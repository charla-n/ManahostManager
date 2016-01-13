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
    public class ProductCategoryService : AbstractService<ProductCategory, ProductCategoryDTO>
    {
        private new IProductCategoryRepository repo;

        public ProductCategoryService(IProductCategoryRepository repo, IValidation<ProductCategory> validation) : base(validation, repo)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(ProductCategoryDTO dto, int id, Client currentClient)
        {
            orig = repo.GetProductCategoryById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override ProductCategory DoPostPutDto(Client currentClient, ProductCategoryDTO dto, ProductCategory entity, string path, object param)
        {
            if (entity == null)
                entity = new ProductCategory();
            else
            {
                if (dto.RefHide == null)
                    dto.RefHide = entity.RefHide;
            }
            GetMapper.Map<ProductCategoryDTO, ProductCategory>(dto, entity);
            return entity;
        }

        protected override void SetDefaultValues(ProductCategory entity)
        {
            entity.RefHide = entity.RefHide ?? true;
        }
    }
}