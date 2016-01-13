using ManahostManager.App_Start;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Services;
using System;
using System.Drawing;
using System.IO;

namespace ManahostManager.Utils
{
    public class DocumentUtils
    {
        // pathFile : Send the Absolute URL
        public static long GetAllDocumentsSize(long initialLenght, String pathFile, Boolean isImage)
        {
            long ret = initialLenght;

            if (isImage)
            {
                foreach (DocumentService.ImageFormat format in DocumentService.ImagesFormats)
                {
                    String pathFileWithExtension = GetNewPathFileName(pathFile, format.Name);
                    if (File.Exists(pathFileWithExtension))
                    {
                        FileInfo tmpInfo = new FileInfo(pathFileWithExtension);

                        ret += tmpInfo.Length;
                    }
                }
            }
            return ret;
        }

        // entityUrl : Send the entity URL
        public static void DeleteAllFile(String entityUrl, Boolean isImage)
        {
            String path = GetFullDocumentUrl(entityUrl);

            if (File.Exists(path))
                File.Delete(path);
            if (isImage)
            {
                foreach (DocumentService.ImageFormat format in DocumentService.ImagesFormats)
                {
                    String pathToDel = GetNewPathFileName(path, format.Name);

                    if (File.Exists(pathToDel))
                        File.Delete(pathToDel);
                }
            }
        }

        public static String GetStringFromStream(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        // Url : Send the entity URL
        public static Stream GetDocumentStream(Boolean isPrivate, String Url, String encryptionKey)
        {
            Stream result;
            String fullUrl;

            fullUrl = GetFullDocumentUrl(Url);
            if (isPrivate)
                result = AES256.DecryptFile(fullUrl, encryptionKey);
            else
                result = File.OpenRead(fullUrl);
            result.Position = 0;
            return result;
        }

        // Create the Absolute URL from the entity Url
        public static String GetFullDocumentUrl(String Url)
        {
            return WebApiApplication.UPLOAD_FOLDER_ROOT + Url;
        }

        // pathImage : Send the Absolute URL
        public static String GetNewPathFileName(String pathImage, String endFile, Boolean fullPath = false)
        {
            FileInfo info = new FileInfo(pathImage);
            String path = info.DirectoryName + @"\" + Path.GetFileNameWithoutExtension(pathImage) + "_" + endFile + Path.GetExtension(pathImage);

            if (fullPath)
                path = path.Replace(WebApiApplication.UPLOAD_FOLDER_ROOT, String.Empty);
            return path;
        }

        public static String GetEncryptionPassword(IHomeRepository homeRepo, Client currentClient)
        {
            if (currentClient != null)
            {
                Home home = homeRepo.GetHomeById((int)currentClient.DefaultHomeId, currentClient.Id);

                return home.EncryptionPassword;
            }
            return null;
        }

        public static String GetMimeType(String titleFile)
        {
            String extension = Path.GetExtension(titleFile).ToLower();
            String mimeType = "multipart/form-data";

            switch (extension)
            {
                case ".png":
                    mimeType = "image/png";
                    break;

                case ".jpg":
                case ".jpeg":
                    mimeType = "image/png";
                    break;

                case ".pdf":
                    mimeType = "application/pdf";
                    break;
            }
            return mimeType;
        }

        public static Image ResizeImage(Image image, int desWidth, int desHeight)
        {
            int x, y, w, h;

            if (image.Height > image.Width)
            {
                w = (image.Width * desHeight) / image.Height;
                h = desHeight;
                x = (desWidth - w) / 2;
                y = 0;
            }
            else
            {
                w = desWidth;
                h = (image.Height * desWidth) / image.Width;
                x = 0;
                y = (desHeight - h) / 2;
            }

            var bmp = new Bitmap(desWidth, desHeight);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(image, x, y, w, h);
            }
            return bmp;
        }
    }
}