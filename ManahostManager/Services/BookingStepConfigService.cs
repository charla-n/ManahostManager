using ManahostManager.App_Start;
using ManahostManager.Controllers;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class BookingStepConfigService : AbstractService<BookingStepConfig, BookingStepConfigDTO>
    {
        private new IBookingStepConfigRepository repo;

        
        public IBookingStepRepository BookingStepRepository { get { return GetService<IBookingStepRepository>(); } private set { } }

        
        public IAbstractService<BookingStep, BookingStepDTO> BookingStepService { get { return GetService<IAbstractService<BookingStep, BookingStepDTO>>(); } private set { } }

        public BookingStepConfigService(IBookingStepConfigRepository repo, IValidation<BookingStepConfig> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(BookingStepConfigDTO dto, int id, Client currentClient)
        {
            orig = repo.GetBookingStepConfigById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(BookingStepConfigDTO dto)
        {
            if (dto.BookingSteps != null)
                repo.includes.Add("BookingSteps");
        }

        protected override BookingStepConfig DoPostPutDto(Client currentClient, BookingStepConfigDTO dto, BookingStepConfig entity, string path, object param)
        {
            if (entity == null)
                entity = new BookingStepConfig();
            GetMapper.Map<BookingStepConfigDTO, BookingStepConfig>(dto, entity);
            if (dto.BookingSteps != null)
            {
                BookingStepRepository.DeleteRange(entity.BookingSteps.Where(d => !dto.BookingSteps.Any(x => x.Id == d.Id)));
                dto.BookingSteps.ForEach(bookingStep =>
                {
                    if (entity.BookingSteps.Count != 0 && bookingStep.Id != 0 &&
                    entity.BookingSteps.Find(p => p.Id == bookingStep.Id) != null)
                        return;
                    BookingStep toAdd = BookingStepService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, bookingStep, currentClient, path);

                    if (toAdd != null)
                        entity.BookingSteps.Add(toAdd);
                });
            }
            return entity;
        }
    }
}