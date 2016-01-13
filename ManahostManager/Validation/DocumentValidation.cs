using ManahostManager.Controllers;
using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace ManahostManager.Validation
{
    public class DocumentValidation : AbstractValidation<Document>
    {
        static private readonly String[] extensionFilter = ManahostUploadFileSystem.imageExtension.Concat(ManahostUploadFileSystem.otherExtension).ToArray();

        public DocumentValidation()
        {
        }

        /*
        **  Entity Validations
        */

        protected override bool CommonValidation(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, Document entity, object param, params object[] additionalObjects)
        {
            if (entity.Title != null && !extensionFilter.Any(s => entity.Title.EndsWith(s, StringComparison.OrdinalIgnoreCase)))
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "Title"), GenericError.DOES_NOT_MEET_REQUIREMENTS);
            return validationDictionary.IsValid;
        }

        protected override bool ValidatePut(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, Document entity, object param, params object[] additionalObjects)
        {
            NullCheckValidation.NullValidation(TypeOfName.GetNameFromType<Document>(), new Dictionary<String, Object>()
                {
                    {"Hide", entity.Hide}
                }, validationDictionary);
            return CommonValidation(validationDictionary, currentClient, entity, param, additionalObjects);
        }

        /*
        **  Upload Validation
        */

        public bool UploadValidationBeforeProvider(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, Document entity, HttpRequestMessage Request)
        {
            if (entity == null)
                validationDictionary.AddModelError(TypeOfName.GetNameFromType<Document>(), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
            else
            {
                if (entity.Url != null)
                    validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "Url"), GenericError.ALREADY_EXISTS);
                if (!Request.Content.IsMimeMultipartContent())
                    validationDictionary.AddModelError(TypeOfName.GetNameFromType<Document>(), GenericError.WRONG_DATA);
            }
            return validationDictionary.IsValid;
        }

        public bool UploadValidationAfterProvider(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, MultipartFileData file)
        {
            if (file == null || String.IsNullOrEmpty(file.Headers.ContentDisposition.FileName))
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "File"), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
            return validationDictionary.IsValid;
        }

        public bool CheckSizeUpload(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, DocumentLog log)
        {
            if (log == null)
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "DocumentLog"), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
            else if (log.CurrentSize > log.ResourceConfig.LimitBase)
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "LimitBase"), GenericError.UPLOAD_LIMIT);
            return validationDictionary.IsValid;
        }

        /*
        ** Download Validation
        */

        public bool DownloadValidation(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, Document entity)
        {
            String absoluteUrl = entity != null ? DocumentUtils.GetFullDocumentUrl(entity.Url) : null;

            if (entity == null || (((Boolean)entity.IsPrivate) && currentClient == null))
                validationDictionary.AddModelError(TypeOfName.GetNameFromType<Document>(), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
            else
            {
                if (!File.Exists(absoluteUrl))
                    validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Document>(), "Url"), GenericError.FILE_NOT_FOUND);
            }
            return validationDictionary.IsValid;
        }
    }
}