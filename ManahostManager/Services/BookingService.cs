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
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class BookingService : AbstractService<Booking, BookingDTO>
    {
        private new IBookingRepository repo;

        
        public IHomeConfigRepository HomeConfigRepository { get { return GetService<IHomeConfigRepository>(); } private set { } }

        
        public IAdditionalBookingRepository AdditionalBookingRepository { get { return GetService<IAdditionalBookingRepository>(); } private set { } }
        
        public IDepositRepository DepositRepository { get { return GetService<IDepositRepository>(); } private set { } }

        
        public IDinnerBookingRepository DinnerBookingRepository { get { return GetService<IDinnerBookingRepository>(); } private set { } }

        
        public IRoomBookingRepository RoomBokingRepository { get { return GetService<IRoomBookingRepository>(); } private set { } }

        
        public IProductBookingRepository ProductBookingRepository { get { return GetService<IProductBookingRepository>(); } private set { } }

        
        public IAbstractService<AdditionalBooking, AdditionalBookingDTO> AdditionalBookingService { get { return GetService<IAbstractService<AdditionalBooking, AdditionalBookingDTO>>(); } private set { } }

        
        public IAbstractService<Deposit, DepositDTO> DepositService { get { return GetService<IAbstractService<Deposit, DepositDTO>>(); } private set { } }

        
        public IAbstractService<DinnerBooking, DinnerBookingDTO> DinnerBookingService { get { return GetService<IAbstractService<DinnerBooking, DinnerBookingDTO>>(); } private set { } }

        
        public IAbstractService<ProductBooking, ProductBookingDTO> ProductBookingService { get { return GetService<IAbstractService<ProductBooking, ProductBookingDTO>>(); } private set { } }

        
        public IAbstractService<RoomBooking, RoomBookingDTO> RoomBookingService { get { return GetService<IAbstractService<RoomBooking, RoomBookingDTO>>(); } private set { } }

        
        public IAbstractService<People, PeopleDTO> PeopleService { get { return GetService<IAbstractService<People, PeopleDTO>>(); } private set { } }

        
        public IAbstractService<BookingStepBooking, BookingStepBookingDTO> BookingStepBookingService { get { return GetService<IAbstractService<BookingStepBooking, BookingStepBookingDTO>>(); } private set { } }

        public BookingService(IBookingRepository repo, IValidation<Booking> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(BookingDTO dto, int id, Client currentClient)
        {
            orig = repo.GetBookingById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(BookingDTO dto)
        {
            if (dto.People != null)
                repo.includes.Add("People");
            if (dto.AdditionalBookings != null)
                repo.includes.Add("AdditionalBookings");
            if (dto.Deposits != null)
                repo.includes.Add("Deposits");
            if (dto.DinnerBookings != null)
                repo.includes.Add("DinnerBookings");
            if (dto.ProductBookings != null)
                repo.includes.Add("ProductBookings");
            if (dto.RoomBookings != null)
                repo.includes.Add("RoomBookings");
        }

        //TODO BookingDocument

        protected override Booking DoPostPutDto(Client currentClient, BookingDTO dto, Booking entity, string path, object param)
        {
            if (entity == null)
                entity = new Booking();
            GetMapper.Map<BookingDTO, Booking>(dto, entity);

            HomeConfig hc = HomeConfigRepository.GetHomeConfigById(entity.HomeId, currentClient.Id);

            if (hc != null)
            {
                if (hc.DefaultHourCheckIn != null)
                    entity.DateArrival = ((DateTime)entity.DateArrival).Date.Add(hc.DefaultHourCheckInToTimeSpan);
                if (hc.DefaultHourCheckOut != null)
                    entity.DateDeparture = ((DateTime)entity.DateDeparture).Date.Add(hc.DefaultHourCheckOutToTimeSpan);
            }
            if (dto.People != null)
                entity.People = PeopleService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.People, currentClient, path);
            if (dto.AdditionalBookings != null)
            {
                AdditionalBookingRepository.DeleteRange(entity.AdditionalBookings.Where(d => !dto.AdditionalBookings.Any(x => x.Id == d.Id)));
                dto.AdditionalBookings.ForEach(additionalBooking =>
                {
                    if (entity.AdditionalBookings.Count != 0 && additionalBooking.Id != 0 &&
                    entity.AdditionalBookings.Find(p => p.Id == additionalBooking.Id) != null)
                        return;
                    AdditionalBooking toAdd = AdditionalBookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, additionalBooking, currentClient, path);

                    if (toAdd != null)
                        entity.AdditionalBookings.Add(toAdd);
                });
            }
            if (dto.BookingStepBooking != null)
                entity.BookingStepBooking = BookingStepBookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.BookingStepBooking, currentClient, path);
            if (dto.Deposits != null)
            {
                DepositRepository.DeleteRange(entity.Deposits.Where(d => !dto.Deposits.Any(x => x.Id == d.Id)));
                dto.Deposits.ForEach(deposit =>
                {
                    if (entity.Deposits.Count != 0 && deposit.Id != 0 &&
                    entity.Deposits.Find(p => p.Id == deposit.Id) != null)
                        return;
                    Deposit toAdd = DepositService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, deposit, currentClient, path);

                    if (toAdd != null)
                        entity.Deposits.Add(toAdd);
                });
            }
            if (dto.DinnerBookings != null)
            {
                DinnerBookingRepository.DeleteRange(entity.DinnerBookings.Where(d => !dto.DinnerBookings.Any(x => x.Id == d.Id)));
                dto.DinnerBookings.ForEach(dinnerBooking =>
                {
                    if (entity.DinnerBookings.Count != 0 && dinnerBooking.Id != 0 &&
                    entity.DinnerBookings.Find(p => p.Id == dinnerBooking.Id) != null)
                        return;
                    DinnerBooking toAdd = DinnerBookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dinnerBooking, currentClient, path);

                    if (toAdd != null)
                        entity.DinnerBookings.Add(toAdd);
                });
            }
            if (dto.ProductBookings != null)
            {
                ProductBookingRepository.DeleteRange(entity.ProductBookings.Where(d => !dto.ProductBookings.Any(x => x.Id == d.Id)));
                dto.ProductBookings.ForEach(productBooking =>
                {
                    if (entity.ProductBookings.Count != 0 && productBooking.Id != 0 &&
                    entity.ProductBookings.Find(p => p.Id == productBooking.Id) != null)
                        return;
                    ProductBooking toAdd = ProductBookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, productBooking, currentClient, path);

                    if (toAdd != null)
                        entity.ProductBookings.Add(toAdd);
                });
            }
            if (dto.RoomBookings != null)
            {
                RoomBokingRepository.DeleteRange(entity.RoomBookings.Where(d => !dto.RoomBookings.Any(x => x.Id == d.Id)));
                dto.RoomBookings.ForEach(roomBooking =>
                {
                    if (entity.RoomBookings.Count != 0 && roomBooking.Id != 0 &&
                    entity.RoomBookings.Find(p => p.Id == roomBooking.Id) != null)
                        return;
                    RoomBooking toAdd = RoomBookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, roomBooking, currentClient, path);

                    if (toAdd != null)
                        entity.RoomBookings.Add(toAdd);
                });
            }
            entity.DateCreation = DateTime.UtcNow;
            return entity;
        }

        protected override void SetDefaultValues(Booking entity)
        {
            entity.TotalPeople = 0;
        }

        protected override void DoDelete(Client currentClient, int id, object param)
        {
            ((IBookingRepository)repo).includes.Add("Product");
            IEnumerable<ProductBooking> listProductBooking = ((IBookingRepository)repo).GetProductBookingFromBookingId(id, currentClient.Id);

            foreach (ProductBooking productBooking in listProductBooking)
            {
                productBooking.Product.Stock += productBooking.Quantity;
                if (productBooking.Product.Stock < productBooking.Product.Threshold)
                    productBooking.Product.IsUnderThreshold = true;
                else
                    productBooking.Product.IsUnderThreshold = false;
                repo.Update<Product>(productBooking.Product);
            }
            repo.Save();
        }
    }
}