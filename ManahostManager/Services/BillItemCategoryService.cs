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
    public class BillItemCategoryService : AbstractService<BillItemCategory, BillItemCategoryDTO>
    {
        private new IBillItemCategoryRepository repo;

        public BillItemCategoryService(IBillItemCategoryRepository repo, IValidation<BillItemCategory> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(BillItemCategoryDTO dto, int id, Client currentClient)
        {
            orig = repo.GetBillItemCategoryById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override BillItemCategory DoPostPutDto(Client currentClient, BillItemCategoryDTO dto, BillItemCategory entity, string path, object param)
        {
            if (entity == null)
                entity = new BillItemCategory();
            GetMapper.Map(dto, entity);
            return entity;
        }
    }
}