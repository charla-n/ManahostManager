using ManahostManager.App_Start;
using ManahostManager.Controllers;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class BookingStepService : AbstractService<BookingStep, BookingStepDTO>
    {
        private new IBookingStepRepository repo;

        
        public IDocumentRepository DocumentRepository { get { return GetService<IDocumentRepository>(); } private set { } }

        
        public IAdditionnalDocumentMethod DocumentService { get { return GetService<IAdditionnalDocumentMethod>(); } private set { } }

        
        public IAbstractService<BookingStepConfig, BookingStepConfigDTO> BookingStepConfigService { get { return GetService<IAbstractService<BookingStepConfig, BookingStepConfigDTO>>(); } private set { } }

        public BookingStepService(IBookingStepRepository repo, IValidation<BookingStep> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(BookingStepDTO dto, int id, Client currentClient)
        {
            repo.GetBookingStepById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(BookingStepDTO dto)
        {
            if (dto.BookingStepIdNext != null)
                repo.includes.Add("BookingStepNext");
            if (dto.BookingStepIdPrevious != null)
                repo.includes.Add("BookingStepPrevious");
            if (dto.MailTemplate != null)
                repo.includes.Add("MailTemplate");
            if (dto.Documents != null)
                repo.includes.Add("Documents");
            if (dto.BookingStepConfig != null)
                repo.includes.Add("BookingStepConfig");
        }

        protected override BookingStep DoPostPutDto(Client currentClient, BookingStepDTO dto, BookingStep entity, string path, object param)
        {
            if (entity == null)
                entity = new BookingStep();
            GetMapper.Map(dto, entity);
            if (path.StartsWith("BookingStepDTO") && dto.BookingStepConfig == null)
            {
                validationDictionnary.AddModelError("Booking", GenericError.CANNOT_BE_NULL_OR_EMPTY);
                throw new ManahostValidationException(validationDictionnary);
            }
            if (dto.BookingStepConfig != null)
                entity.BookingStepConfig = BookingStepConfigService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.BookingStepConfig, currentClient, path);
            if (dto.BookingStepIdNext != null && dto.BookingStepIdNext != 0)
                entity.BookingStepNext = PreProcessDTOPostPut(dto.HomeId, new BookingStepDTO() { Id = (int)dto.BookingStepIdNext }, currentClient, path, param);
            if (dto.BookingStepIdPrevious != null && dto.BookingStepIdPrevious != 0)
                entity.BookingStepPrevious = PreProcessDTOPostPut(dto.HomeId, new BookingStepDTO() { Id = (int)dto.BookingStepIdPrevious }, currentClient, path, param);
            if (dto.MailTemplate != null)
                entity.MailTemplate = DocumentService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.MailTemplate, currentClient, path);
            if (dto.Documents != null)
            {
                DocumentRepository.DeleteRange(entity.Documents.Where(d => !dto.Documents.Any(x => x.Id == d.Id)));
                dto.Documents.ForEach(document =>
                {
                    if (entity.Documents.Count != 0 && document.Id != 0 &&
                    entity.Documents.Find(p => p.Id == document.Id) != null)
                        return;
                    Document toAdd = DocumentService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, document, currentClient, path);

                    if (toAdd != null)
                        entity.Documents.Add(toAdd);
                });
            }
            UpdateBookingStepPreviousOnPost(entity);
            return entity;
        }

        private void UpdateBookingStepPreviousOnPost(BookingStep entity)
        {
            if (entity.BookingStepPrevious != null && entity.BookingStepPrevious.BookingValidated)
                entity.BookingValidated = true;
        }
    }
}