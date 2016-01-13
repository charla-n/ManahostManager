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
    public class MealPriceService : AbstractService<MealPrice, MealPriceDTO>
    {
        private new IMealPriceRepository repo;

        
        public IAbstractService<Meal, MealDTO> MealService { get { return GetService<IAbstractService<Meal, MealDTO>>(); } private set { } }
        
        public IAbstractService<PeopleCategory, PeopleCategoryDTO> PeopleCategoryService { get { return GetService<IAbstractService<PeopleCategory, PeopleCategoryDTO>>(); } private set { } }
        
        public IAbstractService<Tax, TaxDTO> TaxService { get { return GetService<IAbstractService<Tax, TaxDTO>>(); } private set { } }

        public MealPriceService(IMealPriceRepository repo, IValidation<MealPrice> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(MealPriceDTO dto, int id, Client currentClient)
        {
            orig = repo.GetMealPriceById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(MealPriceDTO dto)
        {
            if (dto.PeopleCategory != null)
                repo.includes.Add("PeopleCategory");
            if (dto.Tax != null)
                repo.includes.Add("Tax");
            if (dto.Meal != null)
                repo.includes.Add("Meal");
        }

        protected override MealPrice DoPostPutDto(Client currentClient, MealPriceDTO dto, MealPrice entity, string path, object param)
        {
            if (entity == null)
                entity = new MealPrice();
            GetMapper.Map(dto, entity);
            if (dto.Meal != null)
                entity.Meal = MealService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Meal, currentClient, path);
            if (dto.PeopleCategory != null)
                entity.PeopleCategory = PeopleCategoryService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.PeopleCategory, currentClient, path);
            if (dto.Tax != null)
                entity.Tax = TaxService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Tax, currentClient, path);
            return entity;
        }
    }
}