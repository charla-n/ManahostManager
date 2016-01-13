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
    public class PaymentTypeService : AbstractService<PaymentType, PaymentTypeDTO>
    {
        private new IPaymentTypeRepository repo;

        public PaymentTypeService(IPaymentTypeRepository repo, IValidation<PaymentType> validation) : base(validation, repo)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(PaymentTypeDTO dto, int id, Client currentClient)
        {
            orig = repo.GetPaymentTypeById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override PaymentType DoPostPutDto(Client currentClient, PaymentTypeDTO dto, PaymentType entity, string path, object param)
        {
            if (entity == null)
                entity = new PaymentType();
            GetMapper.Map(dto, entity);
            return entity;
        }
    }
}