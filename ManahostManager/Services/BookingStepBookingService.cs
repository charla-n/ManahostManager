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
using System.Net.Mail;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class BookingStepBookingService : AbstractService<BookingStepBooking, BookingStepBookingDTO>
    {
        private new IBookingStepBookingRepository repo;

        
        public IAbstractService<Booking, BookingDTO> BookingService { get { return GetService<IAbstractService<Booking, BookingDTO>>(); } private set { } }

        
        public IAbstractService<BookingStepConfig, BookingStepConfigDTO> BookingStepConfigService { get { return GetService<IAbstractService<BookingStepConfig, BookingStepConfigDTO>>(); } private set { } }

        
        public IAbstractService<BookingStep, BookingStepDTO> BookingStepService { get { return GetService<IAbstractService<BookingStep, BookingStepDTO>>(); } private set { } }

        
        public IBookingStepRepository BookingStepRepository { get { return GetService<IBookingStepRepository>(); } private set { } }

        
        public IBookingStepBookingRepository BookingStepBookingRepository { get { return GetService<IBookingStepBookingRepository>(); } private set { } }

        
        public IHomeConfigRepository HomeConfigRepository { get { return GetService<IHomeConfigRepository>(); } private set { } }

        
        public IDocumentRepository DocumentRepository { get { return GetService<IDocumentRepository>(); } private set { } }

        public BookingStepBookingService(IBookingStepBookingRepository repo, IValidation<BookingStepBooking> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(BookingStepBookingDTO dto, int id, Client currentClient)
        {
            orig = repo.GetBookingStepBookingById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(BookingStepBookingDTO dto)
        {
            if (dto.BookingStepConfig != null)
                repo.includes.Add("BookingStepConfig");
            if (dto.CurrentStep != null)
                repo.includes.Add("CurrentStep");
        }

        protected override void SetDefaultValues(BookingStepBooking entity)
        {
            entity.MailSent = 0;
            entity.DateCurrentStepChanged = DateTime.UtcNow;
        }

        protected override BookingStepBooking DoPostPutDto(Client currentClient, BookingStepBookingDTO dto, BookingStepBooking entity, string path, object param)
        {
            if (entity == null)
                entity = new BookingStepBooking();
            GetMapper.Map<BookingStepBookingDTO, BookingStepBooking>(dto, entity);
            if (dto.Booking != null && dto.Booking.Id != 0)
                entity.Booking = BookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Booking, currentClient, path);
            if (dto.BookingStepConfig != null)
                entity.BookingStepConfig = BookingStepConfigService.PreProcessDTOPostPut(validationDictionnary ,dto.HomeId, dto.BookingStepConfig, currentClient, path);
            if (entity.CurrentStep == null && dto.CurrentStep != null)
                entity.CurrentStep = BookingStepService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.CurrentStep, currentClient, path);
            return entity;
        }

        protected override void DoPost(Client currentClient, BookingStepBooking entity, object param)
        {
            entity.CurrentStep = BookingStepRepository.GetFirstBookingStep(entity.BookingStepConfig.Id, currentClient.Id);
            ValidateBooking(entity);
        }

        protected override void DoPut(Client currentClient, BookingStepBooking entity, object param)
        {
            var origCopy = BookingStepBookingRepository.GetBookingStepBookingById(entity.Id, currentClient.Id);
            if (origCopy.BookingStepConfig.Id != entity.BookingStepConfig.Id)
                entity.CurrentStep = BookingStepRepository.GetFirstBookingStep(entity.BookingStepConfig.Id, currentClient.Id);
            if (origCopy.CurrentStep.Id != entity.CurrentStep.Id)
            {
                entity.MailSent = 0;
                ValidateBooking(entity);
                entity.DateCurrentStepChanged = DateTime.UtcNow;
            }
        }

        private void ValidateBooking(BookingStepBooking entity)
        {
            if (entity.CurrentStep != null && (bool)entity.CurrentStep.BookingValidated)
                entity.Booking.DateValidation = DateTime.UtcNow;
        }

        public virtual MailLog MailSteps(Client currentClient, MailBookingModel model, object param)
        {
            ValidateNull<MailBookingModel>(model);

            MailLog log = new MailLog();
            HomeConfig hc = null;
            Document body = null;
            List<Document> listAttachments = null;
            BookingStepBookingRepository.includes.Add("CurrentStep");
            BookingStepBookingRepository.includes.Add("CurrentStep.Documents");
            BookingStepBookingRepository.includes.Add("CurrentStep.MailTemplate");
            BookingStepBookingRepository.includes.Add("Booking");
            BookingStepBookingRepository.includes.Add("Booking.People");
            BookingStepBooking b = BookingStepBookingRepository.GetBookingStepBookingById(model.BookingId, currentClient.Id);

            if (b == null)
            {
                validationDictionnary.AddModelError(TypeOfName.GetNameFromType<Booking>(), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                throw new ManahostValidationException(validationDictionnary);
            }
            HomeConfigRepository.includes.Add("DefaultMailConfig");
            hc = HomeConfigRepository.GetHomeConfigById(b.HomeId, currentClient.Id);
            if (!((BookingStepBookingValidation)validation).MailStepValidation(validationDictionnary, currentClient, hc, b, model, param))
                throw new ManahostValidationException(validationDictionnary);
            if (b.CurrentStep.MailTemplate == null)
            {
                validationDictionnary.AddModelError("CurrentStep.MailTemplate", GenericError.CANNOT_BE_NULL_OR_EMPTY);
                throw new ManahostValidationException(validationDictionnary);
            }
            body = DocumentRepository.GetDocumentById(b.CurrentStep.MailTemplate.Id, currentClient.Id);
            listAttachments = b.CurrentStep.Documents;
            if (hc.DefaultMailConfig != null)
                SendMailUsingCustomMailAccount(hc.DefaultMailConfig, b, log, model.Password,
                    GetMailBody(body, b.Home), MailUtils.GetAttachments(listAttachments, b.Home));
            else
                SendMailUsingManahostMailAccount(b, log, GetMailBody(body, b.Home), MailUtils.GetAttachments(listAttachments, b.Home));
            repo.Add<MailLog>(log);
            repo.Update(b);
            repo.Save();
            return log;
        }

        private String GetMailBody(Document body, Home currentHome)
        {
            return DocumentUtils.GetStringFromStream(DocumentUtils.GetDocumentStream((bool)body.IsPrivate, (string)body.Url, (string)currentHome.EncryptionPassword));
        }

        private void SendMailUsingCustomMailAccount(MailConfig mailConf, BookingStepBooking b, MailLog log, string password, String body, List<Attachment> attachments)
        {
            SendMail(new InfosMailling(mailConf.Smtp, (int)mailConf.SmtpPort, mailConf.Email, b.Home.Title, password),
                log, b, (bool)mailConf.IsSSL, body, attachments);
        }

        private void SendMailUsingManahostMailAccount(BookingStepBooking b, MailLog log, String body, List<Attachment> attachments)
        {
            SendMail(new InfosMailling(b.Home.Title), log, b, false, body, attachments);
        }

        private void SendMail(InfosMailling mail, MailLog log, BookingStepBooking b, bool ssl, String body, List<Attachment> attachments)
        {
            mail.toPeople.Add(b.Booking.People.Email);
            mail.ssl = ssl;
            mail.prio = System.Net.Mail.MailPriority.High;
            mail.subject = b.CurrentStep.MailSubject;
            //TODO transform mail body template with the template generator
            mail.body = body;
            mail.attachments = attachments;
            Mailling.sendMail(mail, log);

            log.HomeId = b.HomeId;
            b.MailLog = log;
            if (b.MailLog.Successful)
                b.MailSent++;
        }
    }
}