using ManahostManager.App_Start;
using ManahostManager.Controllers;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class DepositService : AbstractService<Deposit, DepositDTO>
    {
        private new IDepositRepository repo;

        public IAbstractService<Booking, BookingDTO> BookingService { get { return GetService<IAbstractService<Booking, BookingDTO>>(); } private set { } }

        public DepositService(IDepositRepository repo, IValidation<Deposit> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        protected override void SetDefaultValues(Deposit entity)
        {
            entity.DateCreation = DateTime.UtcNow;
        }

        public override void ProcessDTOPostPut(DepositDTO dto, int id, Client currentClient)
        {
            orig = repo.GetDepositById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(DepositDTO dto)
        {
            if (dto.Booking != null)
                repo.includes.Add("Booking");
        }

        protected override void DoPostAfterSave(Client currentClient, Deposit entity, object param)
        {
            ComputePercentage(currentClient.Id, entity);
        }

        protected override void DoPut(Client currentClient, Deposit entity, object param)
        {
            ComputePercentage(currentClient.Id, entity);
        }

        protected override Deposit DoPostPutDto(Client currentClient, DepositDTO dto, Deposit entity, string path, object param)
        {
            if (entity == null)
                entity = new Deposit();
            GetMapper.Map(dto, entity);
            if (dto.Booking != null && dto.Booking.Id != 0)
                entity.Booking = BookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Booking, currentClient, path);
            return entity;
        }

        private void ComputePercentage(int clientId, Deposit entity)
        {
            if (entity.ValueType == EValueType.PERCENT)
            {
                entity.ComputedPrice = repo.GetCurrentPriceOfTheBooking(entity.Booking.Id, clientId);
            }
        }
    }
}