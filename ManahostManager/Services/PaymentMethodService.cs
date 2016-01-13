using ManahostManager.App_Start;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManahostManager.Services
{
    public class PaymentMethodService : AbstractService<PaymentMethod, PaymentMethodDTO>
    {
        private new IPaymentMethodRepository repo;

        public IAbstractService<Bill, BillDTO> BillService { get { return GetService<IAbstractService<Bill, BillDTO>>(); } private set { } }
        public IAbstractService<PaymentType, PaymentTypeDTO> PaymentTypeService { get { return GetService<IAbstractService<PaymentType, PaymentTypeDTO>>(); } private set { } }

    public PaymentMethodService(IValidation<PaymentMethod> validation, IPaymentMethodRepository repo) : base(validation, repo)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(PaymentMethodDTO dto, int id, Client currentClient)
        {
            orig = repo.GetPaymentMethodById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(PaymentMethodDTO dto)
        {
            if (dto.PaymentType != null)
                repo.includes.Add("PaymentType");
            if (dto.Bill != null)
                repo.includes.Add("Bill");
        }

        protected override PaymentMethod DoPostPutDto(Client currentClient, PaymentMethodDTO dto, PaymentMethod entity, string path, object param)
        {
            if (entity == null)
                entity = new PaymentMethod();
            GetMapper.Map(dto, entity);
            if (dto.Bill != null && dto.Bill.Id != 0 && dto.Bill.Id != null)
                entity.Bill = BillService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Bill, currentClient, path);
            if (dto.PaymentType != null)
                entity.PaymentType = PaymentTypeService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.PaymentType, currentClient, path);
            return entity;
        }
    }
}