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
    public class MealService : AbstractService<Meal, MealDTO>
    {
        private new IMealRepository repo;

        public MealService(IMealRepository repo, IValidation<Meal> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        protected override void SetDefaultValues(Meal entity)
        {
            entity.RefHide = entity.RefHide ?? true;
        }

        public override void ProcessDTOPostPut(MealDTO dto, int id, Client currentClient)
        {
            orig = repo.GetMealById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(MealDTO dto)
        {
            if (dto.BillItemCategory != null)
                repo.includes.Add("BillItemCategory");
            if (dto.Documents != null)
                repo.includes.Add("Documents");
            if (dto.MealCategory != null)
                repo.includes.Add("MealCategory");
            if (dto.MealPrices != null)
                repo.includes.Add("MealPrices");
        }

        
        public IAbstractService<BillItemCategory, BillItemCategoryDTO> BillitemCategoryService { get { return GetService<IAbstractService<BillItemCategory, BillItemCategoryDTO>>(); } private set { } }
        
        public IAdditionnalDocumentMethod DocumentService { get { return GetService<IAdditionnalDocumentMethod>(); } private set { } }
        
        public IAbstractService<MealCategory, MealCategoryDTO> MealCategoryService { get { return GetService<IAbstractService<MealCategory, MealCategoryDTO>>(); } private set { } }
        
        public IAbstractService<MealPrice, MealPriceDTO> MealPriceService { get { return GetService<IAbstractService<MealPrice, MealPriceDTO>>(); } private set { } }

        
        public IDocumentRepository DocumentRepository { get { return GetService<IDocumentRepository>(); } private set { } }
        
        public IMealPriceRepository MealPrice { get { return GetService<IMealPriceRepository>(); } private set { } }
        
        public IHomeConfigRepository HomeConfigRepository { get { return GetService<IHomeConfigRepository>(); } private set { } }

        protected override Meal DoPostPutDto(Client currentClient, MealDTO dto, Meal entity, string path, object param)
        {

            if (entity == null)
                entity = new Meal();
            else
            {
                if (dto.RefHide == null)
                    dto.RefHide = entity.RefHide;
            }
            GetMapper.Map(dto, entity);
            if (dto.BillItemCategory != null)
                entity.BillItemCategory = BillitemCategoryService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.BillItemCategory, currentClient, path);
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
            if (dto.MealCategory != null)
                entity.MealCategory = MealCategoryService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.MealCategory, currentClient, path);
            if (dto.MealPrices != null)
            {
                MealPrice.DeleteRange(entity.MealPrices.Where(d => !dto.MealPrices.Any(x => x.Id == d.Id)));
                dto.MealPrices.ForEach(mealPrice =>
        {
            if (entity.MealPrices.Count != 0 && mealPrice.Id != 0 &&
            entity.MealPrices.Find(p => p.Id == mealPrice.Id) != null)
                return;
            MealPrice toAdd = MealPriceService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, mealPrice, currentClient, path);

            if (toAdd != null)
                entity.MealPrices.Add(toAdd);
        });
            }
            return entity;
        }

        protected override void DoPost(Client currentClient, Meal entity, object param)
        {
            HomeConfigRepository.includes.Add("DefaultBillItemCategoryMeal");
            var config = HomeConfigRepository.GetHomeConfigById(entity.HomeId, currentClient.Id);
            if (config != null && config.DefaultBillItemCategoryMeal != null && entity.BillItemCategory == null)
                entity.BillItemCategory = config.DefaultBillItemCategoryMeal;
        }
    }
}