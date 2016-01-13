using ManahostManager.App_Start;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public interface IAdditionnalDocumentMethod : IAbstractService<Document, DocumentDTO>
    {
        HttpResponseMessage Get(Document entity, HttpRequestMessage Request, object adds, String extensionName = null, Client currentClient = null, int? idHome = null);

        HttpResponseMessage UploadFile(Client currentClient, Document entity, HttpRequestMessage Request, IHomeRepository homeRepo, IDocumentLogRepository logRepo);
    }

    public class DocumentService : AbstractService<Document, DocumentDTO>, IAdditionnalDocumentMethod
    {
        public class ImageFormat
        {
            public ImageFormat(String Name, int Width, int Height)
            {
                this.Name = Name;
                this.Height = Height;
                this.Width = Width;
            }

            public String Name { get; set; }

            public int Height { get; set; }

            public int Width { get; set; }
        }

        static public readonly ImageFormat[] ImagesFormats = new ImageFormat[] {
            new ImageFormat("1920x1080", 1920 , 1080),
            new ImageFormat("1280x720", 1280, 720),
            new ImageFormat("800x600", 800, 600),
            new ImageFormat("thumbnail", 120, 120)
        };

        private class MyMultipartFileStreamProvider : MultipartFileStreamProvider
        {
            public MyMultipartFileStreamProvider(string path)
                : base(path)
            { }

            public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
            {
                string filepath;

                filepath = Guid.NewGuid().ToString() + "_" + headers.ContentDisposition.FileName.Replace("\"", "");
                return filepath;
            }
        }

        private new IDocumentRepository repo;
        public IHomeRepository HomeRepository { get { return GetService<IHomeRepository>(); } private set { } }

        public DocumentService(IDocumentRepository repo, IValidation<Document> validation)
            : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(DocumentDTO dto, int id, Client currentClient)
        {
            orig = repo.GetDocumentById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void DoPost(Client currentClient, Document entity, object param)
        {
            entity.SizeDocument = 0;
            entity.DateUpload = null;
            entity.SetDateModification(null);
            entity.Url = null;
            entity.Hide = false;
        }

        protected override void DoPut(Client currentClient, Document entity, object param)
        {
            entity.SetDateModification(DateTime.Now);
        }

        protected override void DoDelete(Client currentClient, int id, object param)
        {
            if (orig != null && orig.Title != null)
            {
                Boolean isImage = ManahostUploadFileSystem.imageExtension.Any(s => orig.Title.EndsWith(s, StringComparison.OrdinalIgnoreCase));

                if (orig.Url != null)
                    DocumentUtils.DeleteAllFile(orig.Url, isImage);
            }
        }

        /*
        **
        ** Download
        **
        */

        public virtual HttpResponseMessage Get(Document entity, HttpRequestMessage Request, object adds, String extensionName = null, Client currentClient = null, int? idHome = null)
        {
            if (!((DocumentValidation)validation).DownloadValidation(validationDictionnary, currentClient, entity))
                throw new ManahostValidationException(validationDictionnary);
            if (extensionName != null)
            {
                entity.Url = DocumentUtils.GetNewPathFileName(DocumentUtils.GetFullDocumentUrl(entity.Url), extensionName, true);
                entity.Title = Path.GetFileNameWithoutExtension(entity.Title) + "_" + extensionName + Path.GetExtension(entity.Title);
            }
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                Stream result = DocumentUtils.GetDocumentStream((Boolean)entity.IsPrivate, entity.Url, DocumentUtils.GetEncryptionPassword(HomeRepository, currentClient));

                entity.MimeType = DocumentUtils.GetMimeType(entity.Title);
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StreamContent(result);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(entity.MimeType);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = entity.Title
                };
                return response;
            }
            catch (IOException)
            {
                validationDictionnary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "Download"), GenericError.FILE_NOT_FOUND);
                throw new ManahostValidationException(validationDictionnary);
            }
        }

        /*
        **
        ** Upload
        **
        */

        public virtual HttpResponseMessage UploadFile(Client currentClient, Document entity, HttpRequestMessage Request, IHomeRepository homeRepo, IDocumentLogRepository logRepo)
        {
            MyMultipartFileStreamProvider provider;
            Boolean isImage = false;

            ValidateNull(entity);
            if (entity != null && ((IDocumentRepository)repo).GetDocumentById(entity.Id, currentClient.Id) == null)
                validationDictionnary.AddModelError(TypeOfName.GetNameFromType<Document>(), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
            if (!((DocumentValidation)validation).UploadValidationBeforeProvider(validationDictionnary, currentClient, entity, Request))
                throw new ManahostValidationException(validationDictionnary);
            provider = new MyMultipartFileStreamProvider(ManahostUploadFileSystem.GetUploadFolderPath(WebApiApplication.UPLOAD_FOLDER_ROOT, currentClient.Id, (Boolean)entity.IsPrivate));
            try
            {
                IEnumerable<HttpContent> parts = null;
                Task.Factory.StartNew(() => parts = Request.Content.ReadAsMultipartAsync(provider).Result.Contents,
                        CancellationToken.None,
                        TaskCreationOptions.LongRunning, // guarantees separate thread
                        TaskScheduler.Default).Wait();
                MultipartFileData file = provider.FileData.First();

                if (!((DocumentValidation)validation).UploadValidationAfterProvider(validationDictionnary, currentClient, file))
                    throw new ManahostValidationException(validationDictionnary);
                if (isImage = ManahostUploadFileSystem.imageExtension.Any(s => entity.Title.EndsWith(s, StringComparison.OrdinalIgnoreCase)))
                    ImageCompressAndThumbnail(file.LocalFileName);
                if ((Boolean)entity.IsPrivate && currentClient != null)
                    EncryptFileOnUpload(file.LocalFileName, DocumentUtils.GetEncryptionPassword(homeRepo, currentClient), isImage);
                if (!UpdateEntitiesOnUpload(currentClient, entity, file.LocalFileName, isImage, logRepo))
                    throw new ManahostValidationException(validationDictionnary);
            }
            catch (Exception e)
            {
                if (validationDictionnary.IsValid)
                    validationDictionnary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "Upload"), GenericError.INVALID_GIVEN_PARAMETER);
                if (entity.Url != null)
                    DocumentUtils.DeleteAllFile(entity.Url, isImage);
                repo.Delete(entity);
                repo.Save();
                throw new ManahostValidationException(validationDictionnary, e.StackTrace);
            }
            return BuildStringContent.BuildFromRequestOK(Request);
        }

        private bool UpdateEntitiesOnUpload(Client currentClient, Document entity, String pathFile, Boolean isImage, IDocumentLogRepository repoLog)
        {
            FileInfo f = new FileInfo(pathFile);
            repoLog.includes.Add("ResourceConfig");
            DocumentLog logEntity = repoLog.GetDocumentLogById(currentClient.Id);

            entity.SizeDocument = f.Length;
            entity.Url = pathFile.Replace(WebApiApplication.UPLOAD_FOLDER_ROOT, string.Empty);
            entity.DateUpload = DateTime.Now;
            logEntity.CurrentSize += DocumentUtils.GetAllDocumentsSize(f.Length, pathFile, isImage);
            logEntity.DateModification = DateTime.Now;
            if (!((DocumentValidation)validation).CheckSizeUpload(validationDictionnary, logEntity))
                return false;
            repo.Update(entity);
            repo.Save();
            repoLog.Update(logEntity);
            repoLog.Save();
            return true;
        }

        private void EncryptFileOnUpload(String pathFile, String encryptionKey, Boolean isImage)
        {
            AES256.EncryptFile(pathFile, encryptionKey);
            if (isImage)
            {
                foreach (ImageFormat format in ImagesFormats)
                {
                    String pathFileWithExtension = DocumentUtils.GetNewPathFileName(pathFile, format.Name);
                    if (File.Exists(pathFileWithExtension))
                        AES256.EncryptFile(pathFileWithExtension, encryptionKey);
                }
            }
        }

        private void ImageCompressAndThumbnail(String pathImage)
        {
            Image image = Image.FromFile(pathImage);

            foreach (ImageFormat format in ImagesFormats)
            {
                if (image.Height > format.Height || image.Width > format.Width)
                {
                    using (Image newQualityImage = DocumentUtils.ResizeImage(image, format.Width, format.Height))
                        newQualityImage.Save(DocumentUtils.GetNewPathFileName(pathImage, format.Name));
                }
            }
            image.Dispose();
        }
    }
}