using ManahostManager.App_Start;
using ManahostManager.Controllers;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class PricePerPersonService : AbstractService<PricePerPerson, PricePerPersonDTO>
    {
        private new IPricePerPersonRepository repo;

        
        public IAbstractService<PeopleCategory, PeopleCategoryDTO> PeopleCategoryService { get { return GetService<IAbstractService<PeopleCategory, PeopleCategoryDTO>>(); } private set { } }
        
        public IAbstractService<Period, PeriodDTO> PeriodService { get { return GetService<IAbstractService<Period, PeriodDTO>>(); } private set { } }
        
        public IAbstractService<Room, RoomDTO> RoomService { get { return GetService<IAbstractService<Room, RoomDTO>>(); } private set { } }
        
        public IAbstractService<Tax, TaxDTO> TaxService { get { return GetService<IAbstractService<Tax, TaxDTO>>(); } private set { } }

        public PricePerPersonService(IPricePerPersonRepository repo, IValidation<PricePerPerson> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(PricePerPersonDTO dto, int id, Client currentClient)
        {
            orig = repo.GetPricePerPersonById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(PricePerPersonDTO dto)
        {
            if (dto.PeopleCategory != null)
                repo.includes.Add("PeopleCategory");
            if (dto.Period != null)
                repo.includes.Add("Period");
            if (dto.Room != null)
                repo.includes.Add("Room");
            if (dto.Tax != null)
                repo.includes.Add("Tax");
            base.PutIncludeProps(dto);
        }

        protected override PricePerPerson DoPostPutDto(Client currentClient, PricePerPersonDTO dto, PricePerPerson entity, string path, object param)
        {
            if (entity == null)
                entity = new PricePerPerson();
            GetMapper.Map(dto, entity);
            if (dto.PeopleCategory != null)
                entity.PeopleCategory = PeopleCategoryService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.PeopleCategory, currentClient, path);
            if (dto.Period != null)
                entity.Period = PeriodService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Period, currentClient, path);
            if (dto.Room != null)
                entity.Room = RoomService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Room, currentClient, path);
            if (dto.Tax != null)
                entity.Tax = TaxService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Tax, currentClient, path);
            return entity;
        }
    }
}