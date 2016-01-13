using ManahostManager.Controllers;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Model;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManahostManager.Validation
{
    public class MailValidation : AbstractValidation<MailModel>
    {
        protected override bool ValidatePost(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, MailModel entity, object param, params object[] additionalObjects)
        {
            IMailConfigRepository repo = (IMailConfigRepository)additionalObjects[0];
            List<string> recipients = (List<string>)additionalObjects[1];
            List<Document> attachments = (List<Document>)additionalObjects[2];
            MailController.AdditionalRepositories addRepo = (MailController.AdditionalRepositories)param;

            NullCheckValidation.NullValidation(TypeOfName.GetNameFromType<MailModel>(), new Dictionary<String, Object>()
                {
                    {"Body", entity.Body},
                    {"Subject", entity.Subject},
                    {"Password", entity.Password}
                }, validationDictionary);
            if (repo.GetMailConfigById(entity.MailConfigId, currentClient.Id) == null)
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MailModel>(), "MailConfigId"), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
            if (entity.To.Count == 0)
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MailModel>(), "To"), GenericError.CANNOT_BE_NULL_OR_EMPTY);
            try
            {
                Encoding.UTF8.GetString(Convert.FromBase64String(entity.Password));
            }
            catch (Exception)
            {
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MailModel>(), "Password"), GenericError.DOES_NOT_MEET_REQUIREMENTS);
            }
            foreach (int cur in entity.To)
            {
                People p = addRepo.PeopleRepo.GetPeopleById(cur, currentClient.Id);
                if (p == null)
                {
                    validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MailModel>(), "To"), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                    return false;
                }
                if (p.AcceptMailing == true && p.Email != null)
                    recipients.Add(p.Email);
            }
            if (entity.Attachments != null)
            {
                foreach (int cur in entity.Attachments)
                {
                    Document d = addRepo.DocumentRepo.GetDocumentById(cur, currentClient.Id);
                    if (d == null)
                    {
                        validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MailModel>(), "Attachments"), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                        return false;
                    }
                    attachments.Add(d);
                }
            }
            return validationDictionary.IsValid;
        }

        protected override bool CommonValidation(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, MailModel entity, object param, params object[] additionalObjects)
        {
            throw new NotImplementedException();
        }
    }
}