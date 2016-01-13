using ManahostManager.App_Start;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class MealCategoryService : AbstractService<MealCategory, MealCategoryDTO>
    {
        private new IMealCategoryRepository repo;

        public MealCategoryService(IMealCategoryRepository repo, IValidation<MealCategory> validation) : base(validation, repo)
        {
            this.repo = repo;
        }

        protected override void SetDefaultValues(MealCategory entity)
        {
            entity.RefHide = entity.RefHide ?? true;
        }

        protected override MealCategory DoPostPutDto(Client currentClient, MealCategoryDTO dto, MealCategory entity, string path, object param)
        {
            if (entity == null)
                entity = new MealCategory();
            else
            {
                if (dto.RefHide == null)
                    dto.RefHide = entity.RefHide;
            }
            GetMapper.Map(dto, entity);
            return entity;
        }

        public override void ProcessDTOPostPut(MealCategoryDTO dto, int id, Client currentClient)
        {
            orig = repo.GetMealCategoryById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }
    }
}