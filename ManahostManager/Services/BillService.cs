using ManahostManager.App_Start;
using ManahostManager.Controllers;
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
    public class BillService : AbstractService<Bill, BillDTO>
    {
        private new IBillRepository BillRepository;

        public IAbstractService<Booking, BookingDTO> BookingService { get { return GetService<IAbstractService<Booking, BookingDTO>>(); } private set { } }

        public IAbstractService<BillItem, BillItemDTO> BillItemService { get { return GetService< IAbstractService<BillItem, BillItemDTO>> (); } private set { } }

        public IAbstractService<PaymentMethod, PaymentMethodDTO> PaymentMethodService { get { return GetService<IAbstractService<PaymentMethod, PaymentMethodDTO>>(); } private set { } }

        public IAbstractService<Supplier, SupplierDTO> SupplierService { get { return GetService<IAbstractService<Supplier, SupplierDTO>>(); } private set { } }

        public IBillItemRepository BillItemRepository { get { return GetService<IBillItemRepository>(); } private set { } }

        public IPaymentMethodRepository PaymentMethodRepository { get { return GetService<IPaymentMethodRepository>(); } private set { } }

        public BillService(IValidation<Bill> BillValidation, IBillRepository BillRepository) : base(BillValidation, BillRepository)
        {
            this.BillRepository = BillRepository;
        }

        public override void ProcessDTOPostPut(BillDTO dto, int id, Client currentClient)
        {
            orig = BillRepository.GetBillById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(BillDTO dto)
        {
            if (dto.BillItems != null)
                repo.includes.Add("BillItems");
            if (dto.Booking != null)
                repo.includes.Add("Booking");
            if (dto.Document != null)
                repo.includes.Add("Document");
            if (dto.PaymentMethods != null)
                repo.includes.Add("PaymentMethods");
            if (dto.Supplier != null)
                repo.includes.Add("Supplier");
        }

        protected override Bill DoPostPutDto(Client currentClient, BillDTO dto, Bill entity, string path, object param)
        {
            if (entity == null)
                entity = new Bill();
            GetMapper.Map(dto, entity);
            if (dto.Booking != null && dto.Booking.Id != 0)
                entity.Booking = BookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Booking, currentClient, path);
            if (dto.BillItems != null)
            {
                BillItemRepository.DeleteRange(entity.BillItems.Where(d => !dto.BillItems.Any(x => x.Id == d.Id)));
                dto.BillItems.ForEach(bitem =>
                {
                    if (entity.BillItems.Count != 0 && bitem.Id != 0 &&
                        entity.BillItems.Find(p => p.Id == bitem.Id) != null)
                        return;
                    BillItem toAdd = BillItemService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, bitem, currentClient, path);

                    if (toAdd != null)
                        entity.BillItems.Add(toAdd);
                });
            }
            if (dto.PaymentMethods != null)
            {
                PaymentMethodRepository.DeleteRange(entity.PaymentMethods.Where(d => !dto.PaymentMethods.Any(x => x.Id == d.Id)));
                dto.PaymentMethods.ForEach(pm =>
                {
                    if (entity.PaymentMethods.Count != 0 && pm.Id != 0 &&
                        entity.PaymentMethods.Find(p => p.Id == pm.Id) != null)
                        return;
                    PaymentMethod toAdd = PaymentMethodService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, pm, currentClient, path);

                    if (toAdd != null)
                        entity.PaymentMethods.Add(toAdd);
                });
            }
            if (dto.Supplier != null)
                entity.Supplier = SupplierService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Supplier, currentClient, path);
            return entity;
        }

        protected override void DoPost(Client currentClient, Bill entity, object param)
        {
            entity.CreationDate = DateTime.UtcNow;
        }
    }
}