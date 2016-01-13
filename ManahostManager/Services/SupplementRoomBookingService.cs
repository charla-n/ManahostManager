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
    public class SupplementRoomBookingService : AbstractService<SupplementRoomBooking, SupplementRoomBookingDTO>
    {
        private new ISupplementRoomBookingRepository repo;

        public SupplementRoomBookingService(ISupplementRoomBookingRepository repo, IValidation<SupplementRoomBooking> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(SupplementRoomBookingDTO dto, int id, Client currentClient)
        {
            orig = repo.GetSupplementRoomBookingById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(SupplementRoomBookingDTO dto)
        {
            if (dto.RoomSupplement != null)
                repo.includes.Add("RoomSupplement");
            if (dto.RoomBooking != null)
                repo.includes.Add("RoomBooking");
        }

        protected override void SetDefaultValues(SupplementRoomBooking entity)
        {
            entity.PriceHT = entity.PriceHT ?? 0M;
            entity.PriceTTC = entity.PriceTTC ?? entity.PriceHT;
        }

        public virtual SupplementRoomBookingDTO Compute(Client currentClient, int id, object param)
        {
            repo.includes.Add("RoomSupplement.Tax");
            ProcessDTOPostPut(null, id, currentClient);
            ValidateOrig();
            orig.PriceHT = orig.RoomSupplement.PriceHT;
            orig.PriceTTC = orig.PriceHT;
            ComputeTaxPrice(orig.RoomSupplement);
            repo.Update(orig);
            repo.Save();
            return GetMapper.Map<SupplementRoomBooking, SupplementRoomBookingDTO>(orig);
        }

        public IAbstractService<RoomSupplement, RoomSupplementDTO> RoomSupplementService { get { return GetService<IAbstractService<RoomSupplement, RoomSupplementDTO>>(); } private set { } }
        public IAbstractService<RoomBooking, RoomBookingDTO> RoomBookingService { get { return GetService<IAbstractService<RoomBooking, RoomBookingDTO>>(); } private set { } }

        protected override SupplementRoomBooking DoPostPutDto(Client currentClient, SupplementRoomBookingDTO dto, SupplementRoomBooking entity, string path, object param)
        {
            if (entity == null)
                entity = new SupplementRoomBooking();
            else
            {
                if (dto.PriceHT == null)
                    dto.PriceHT = entity.PriceHT;
                if (dto.PriceTTC == null)
                    dto.PriceTTC = entity.PriceTTC;
            }
            GetMapper.Map(dto, entity);
            if (dto.RoomSupplement != null)
                entity.RoomSupplement = RoomSupplementService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.RoomSupplement, currentClient, path);
            if (dto.RoomBooking != null)
                entity.RoomBooking = RoomBookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.RoomBooking, currentClient, path);
            return entity;
        }

        private void ComputeTaxPrice(RoomSupplement entity)
        {
            if (entity.Tax != null)
            {
                Tax associatedTax = entity.Tax;

                orig.PriceTTC = ComputePrice.ComputePriceFromPercentOrAmount((Decimal)entity.PriceHT, (EValueType)associatedTax.ValueType, (Decimal)associatedTax.Price);
            }
        }
    }
}