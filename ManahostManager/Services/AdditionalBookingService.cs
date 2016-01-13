using ManahostManager.App_Start;
using ManahostManager.Controllers;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class AdditionalBookingService : AbstractService<AdditionalBooking, AdditionalBookingDTO>
    {
        private new IAdditionalBookingRepository repo;

        
        public IAbstractService<Booking, BookingDTO> BookingService { get { return GetService<IAbstractService<Booking, BookingDTO>>(); } private set { } }

        
        public IAbstractService<BillItemCategory, BillItemCategoryDTO> BillItemCategoryService { get { return GetService<IAbstractService<BillItemCategory, BillItemCategoryDTO>>(); } private set { } }

        
        public IAbstractService<Tax, TaxDTO> TaxService { get { return GetService<IAbstractService<Tax, TaxDTO>>(); } private set { } }

        public AdditionalBookingService(IAdditionalBookingRepository repo, IValidation<AdditionalBooking> validation)
            : base(validation, repo)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(AdditionalBookingDTO dto, int id, Client currentClient)
        {
            orig = repo.GetAdditionalBookingById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(AdditionalBookingDTO dto)
        {
            if (dto.BillItemCategory != null)
                repo.includes.Add("BillItemCategory");
            if (dto.Tax != null)
                repo.includes.Add("Tax");
            if (dto.Booking != null)
                repo.includes.Add("Booking");
            base.PutIncludeProps(dto);
        }

        protected override void SetDefaultValues(AdditionalBooking entity)
        {
            entity.PriceTTC = entity.PriceTTC ?? entity.PriceHT;
        }

        public virtual AdditionalBookingDTO Compute(Client currentClient, int id, object param)
        {
            repo.includes.Add("Tax");
            ProcessDTOPostPut(null, id, currentClient);
            ValidateOrig();
            orig.SetDateModification(DateTime.UtcNow);
            ComputeTaxPrice(orig);
            repo.Update(orig);
            repo.Save();
            return GetMapper.Map<AdditionalBooking, AdditionalBookingDTO>(orig);
        }

        protected override AdditionalBooking DoPostPutDto(Client currentClient, AdditionalBookingDTO dto, AdditionalBooking entity, string path, object param)
        {
            if (entity == null)
                entity = new AdditionalBooking();
            else
            {
                if (dto.PriceTTC == null)
                    dto.PriceTTC = entity.PriceTTC;
            }
            GetMapper.Map<AdditionalBookingDTO, AdditionalBooking>(dto, entity);
            if (path.StartsWith("AdditionalBookingDTO") && dto.Booking == null)
                validationDictionnary.AddModelError("Booking", GenericError.CANNOT_BE_NULL_OR_EMPTY);
            if (dto.Booking != null && dto.Booking.Id != 0)
                entity.Booking = BookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Booking, currentClient, path);
            if (dto.BillItemCategory != null)
                entity.BillItemCategory = BillItemCategoryService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.BillItemCategory, currentClient, path);
            if (dto.Tax != null)
                entity.Tax = TaxService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Tax, currentClient, path);
            return entity;
        }

        private void ComputeTaxPrice(AdditionalBooking entity)
        {
            if (entity.Tax != null)
            {
                Tax associatedTax = entity.Tax;

                entity.PriceTTC = ComputePrice.ComputePriceFromPercentOrAmount((Decimal)entity.PriceHT, (EValueType)associatedTax.ValueType, (Decimal)associatedTax.Price);
            }
        }
    }
}