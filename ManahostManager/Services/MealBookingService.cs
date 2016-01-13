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
    public class MealBookingService : AbstractService<MealBooking, MealBookingDTO>
    {
        private new IMealBookingRepository repo;

        
        public IAbstractService<Meal, MealDTO> MealService { get { return GetService<IAbstractService<Meal, MealDTO>>(); } private set { } }

        
        public IAbstractService<PeopleCategory, PeopleCategoryDTO> PeopleCategoryService { get { return GetService<IAbstractService<PeopleCategory, PeopleCategoryDTO>>(); } private set { } }

        
        public IAbstractService<DinnerBooking, DinnerBookingDTO> DinnerBookingService { get { return GetService<IAbstractService<DinnerBooking, DinnerBookingDTO>>(); } private set { } }

        public MealBookingService(IMealBookingRepository repo, IValidation<MealBooking> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(MealBookingDTO dto, int id, Client currentClient)
        {
            orig = repo.GetMealBookingById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(MealBookingDTO dto)
        {
            if (dto.PeopleCategory != null)
                repo.includes.Add("PeopleCategory");
            if (dto.Meal != null)
                repo.includes.Add("Meal");
            if (dto.DinnerBooking != null)
                repo.includes.Add("DinnerBooking");
        }


        protected override MealBooking DoPostPutDto(Client currentClient, MealBookingDTO dto, MealBooking entity, string path, object param)
        {
            if (entity == null)
                entity = new MealBooking();
            else
            {
                if (dto.NumberOfPeople == null)
                    dto.NumberOfPeople = entity.NumberOfPeople;
            }
            GetMapper.Map(dto, entity);
            if (dto.Meal != null)
                entity.Meal = MealService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Meal, currentClient, path);
            if (dto.PeopleCategory != null)
                entity.PeopleCategory = PeopleCategoryService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.PeopleCategory, currentClient, path);
            if (dto.DinnerBooking != null)
                entity.DinnerBooking = DinnerBookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.DinnerBooking, currentClient, path);
            return entity;
        }

        protected override void SetDefaultValues(MealBooking entity)
        {
            entity.NumberOfPeople = entity.NumberOfPeople ?? 1;
        }
    }
}