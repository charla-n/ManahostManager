using ManahostManager.App_Start;
using ManahostManager.Controllers;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Model;
using ManahostManager.Utils;
using ManahostManager.Utils.MailingUtils;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class MailService : AbstractService<MailModel, System.Object>
    {
        private new IMailConfigRepository repo;

        public MailService(ModelStateDictionary validationDictionnary, IMailConfigRepository repo, IValidation<MailModel> validation) : base(validationDictionnary, validation)
        {
            this.repo = repo;
        }

        public virtual MailLogDTO SendMail(Domain.Entity.Client currentClient, MailModel entity, object param)
        {
            List<string> recipients = new List<string>();
            List<Document> documents = new List<Document>();
            MailLog mailLog;

            ValidateNull(entity);
            if (!validation.PreValidatePost(validationDictionnary, currentClient, entity, param, repo, recipients, documents))
                throw new ManahostValidationException(validationDictionnary);
            IHomeRepository homeRepo = ((MailController.AdditionalRepositories)param).HomeRepo;
            MailConfig mailconfig = repo.GetMailConfigById(entity.MailConfigId, currentClient.Id);
            Mailling.SendMailBcc(new InfosMailling(mailconfig.Smtp, (int)mailconfig.SmtpPort, mailconfig.Email, homeRepo.GetHomeById(mailconfig.HomeId, currentClient.Id).Title,
                Encoding.UTF8.GetString(Convert.FromBase64String(entity.Password)))
            {
                body = entity.Body,
                prio = System.Net.Mail.MailPriority.Normal,
                subject = entity.Subject,
                toPeople = recipients,
                ssl = (bool)mailconfig.IsSSL,
                attachments = MailUtils.GetAttachments(documents, mailconfig.Home)
            }, mailLog = new MailLog()
            {
                DateSended = DateTime.UtcNow,
                To = String.Join(",", recipients.ToArray()),
                Successful = true,
                HomeId = mailconfig.HomeId
            });
            repo.Add<MailLog>(mailLog);
            return GetMapper.Map<MailLog, MailLogDTO>(mailLog);
        }
    }
}