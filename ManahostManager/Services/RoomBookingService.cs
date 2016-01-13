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
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class RoomBookingService : AbstractService<RoomBooking, RoomBookingDTO>
    {
        private const int MAX_PEOPLEBOOKING_LENGTH = 30;

        private new IRoomBookingRepository repo;

        private Decimal finalPriceHT;
        private Decimal finalPriceTTC;


        
        public IPeopleBookingRepository PeopleBookingRepository { get { return GetService<IPeopleBookingRepository>(); } private set { } }

        
        public ISupplementRoomBookingRepository SupplementRoomBookingRepository { get { return GetService<ISupplementRoomBookingRepository>(); } private set { } }

        
        public IAbstractService<Booking, BookingDTO> BookingService { get { return GetService<IAbstractService<Booking, BookingDTO>>(); } private set { } }

        
        public IAbstractService<Room, RoomDTO> RoomService { get { return GetService<IAbstractService<Room, RoomDTO>>(); } private set { } }

        
        public IAbstractService<PeopleBooking, PeopleBookingDTO> PeopleBookingService { get { return GetService<IAbstractService<PeopleBooking, PeopleBookingDTO>>(); } private set { } }
        public IAbstractService<SupplementRoomBooking, SupplementRoomBookingDTO> SupplementRoomBookingService { get { return GetService<IAbstractService<SupplementRoomBooking, SupplementRoomBookingDTO>>(); } private set { } }

        public RoomBookingService(IRoomBookingRepository repo, IValidation<RoomBooking> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(RoomBookingDTO dto, int id, Client currentClient)
        {
            orig = repo.GetRoomBookingById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(RoomBookingDTO dto)
        {
            if (dto.Booking != null)
                repo.includes.Add("Booking");
            if (dto.Room != null)
                repo.includes.Add("Room");
            if (dto.PeopleBookings != null)
                repo.includes.Add("PeopleBookings");
            if (dto.SupplementRoomBookings != null)
                repo.includes.Add("SupplementRoomBookings");
        }

        protected override void SetDefaultValues(RoomBooking entity)
        {
            entity.PriceHT = entity.PriceHT ?? 0M;
            entity.PriceTTC = entity.PriceTTC ?? entity.PriceHT;
            ComputeNbNights(entity);
        }


        protected override RoomBooking DoPostPutDto(Client currentClient, RoomBookingDTO dto, RoomBooking entity, string path, object param)
        {
            if (entity == null)
                entity = new RoomBooking();
            else
            {
                if (dto.PriceHT == null)
                    dto.PriceHT = entity.PriceHT;
                if (dto.PriceTTC == null)
                    dto.PriceTTC = entity.PriceTTC;
            }
            GetMapper.Map(dto, entity);
            if (dto.Booking != null && dto.Booking.Id != 0)
                entity.Booking = BookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Booking, currentClient, path);
            if (dto.Room != null)
                entity.Room = RoomService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Room, currentClient, path);
            if (dto.PeopleBookings != null)
            {
                PeopleBookingRepository.DeleteRange(entity.PeopleBookings.Where(d => !dto.PeopleBookings.Any(x => x.Id == d.Id)));
                dto.PeopleBookings.ForEach(peopleBooking =>
                {
                    if (entity.PeopleBookings.Count != 0 && peopleBooking.Id != 0 &&
                    entity.PeopleBookings.Find(p => p.Id == peopleBooking.Id) != null)
                        return;
                    PeopleBooking toAdd = PeopleBookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, peopleBooking, currentClient, path);

                    if (toAdd != null)
                        entity.PeopleBookings.Add(toAdd);
                });
            }
            if (dto.SupplementRoomBookings != null)
            {
                SupplementRoomBookingRepository.DeleteRange(entity.SupplementRoomBookings.Where(d => !dto.SupplementRoomBookings.Any(x => x.Id == d.Id)));
                dto.SupplementRoomBookings.ForEach(supplementRoomBooking =>
                {
                    if (entity.SupplementRoomBookings.Count != 0 && supplementRoomBooking.Id != 0 &&
                    entity.SupplementRoomBookings.Find(p => p.Id == supplementRoomBooking.Id) != null)
                        return;
                    SupplementRoomBooking toAdd = SupplementRoomBookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, supplementRoomBooking, currentClient, path);

                    if (toAdd != null)
                        entity.SupplementRoomBookings.Add(toAdd);
                });
            }
            return entity;
        }

        protected override void DoPostAfterSave(Client currentClient, RoomBooking entity, object param)
        {
            if (entity.DateBegin == null || entity.DateEnd == null)
            {
                if (entity.DateBegin == null)
                    entity.DateBegin = entity.Booking.DateArrival;
                if (entity.DateEnd == null)
                    entity.DateEnd = entity.Booking.DateDeparture;
                ComputeNbNights(entity);
                repo.Update(entity);
                repo.Save();
            }
        }

        protected override void DoPut(Client currentClient, RoomBooking entity, object param)
        {
            ComputeNbNights(entity);
        }

        private void ComputeNbNights(RoomBooking entity)
        {
            if (entity.DateEnd != null && entity.DateBegin != null)
                entity.NbNights = (int)(((DateTime)entity.DateEnd).Date - ((DateTime)entity.DateBegin).Date).TotalDays;
        }

        public virtual RoomBookingDTO Compute(Client currentClient, int id, object param)
        {
            repo = (IRoomBookingRepository)base.repo;
            List<PeopleBooking> listPeopleBooking = null;
            List<Period> listPeriod = null;

            repo.includes.Add("Booking");
            repo.includes.Add("Room");
            ProcessDTOPostPut(null, id, currentClient);
            ValidateOrig();
            repo.includes.Add("PeopleCategory");
            listPeopleBooking = repo.GetPeopleBookingFromRoomBooking(orig.Id, currentClient.Id).ToList();
            listPeriod = repo.GetPeriods((DateTime)orig.Booking.DateArrival, (DateTime)orig.Booking.DateDeparture, currentClient.Id).ToList();

            finalPriceHT = 0;
            finalPriceTTC = 0;
            if (listPeopleBooking.Count == 0)
                ComputeUniqPrice(listPeriod, currentClient);
            else
                ComputePricePerPerson(listPeopleBooking, listPeriod, currentClient);
            if (!validationDictionnary.IsValid)
                throw new ManahostValidationException(validationDictionnary);
            orig.PriceHT = finalPriceHT;
            orig.PriceTTC = finalPriceTTC;
            repo.Update(orig);
            repo.Save();
            return GetMapper.Map<RoomBooking, RoomBookingDTO>(orig);
        }

        private void ComputeUniqPrice(List<Period> listPeriod, Client currentClient)
        {
            UpdatePrices(listPeriod, (DateTime)orig.DateBegin, (DateTime)orig.DateEnd, currentClient, 1);
        }

        private void UpdatePrices(List<Period> listPeriod, DateTime begin, DateTime end, Client currentClient, int coeff, int? peopleCategoryId = null)
        {
            DateTime dateNights = begin;
            PricePerPerson ppp = null;

            for (; dateNights.Date < end.Date; dateNights = dateNights.AddDays(1))
            {
                Period currentPeriod = PeriodUtils.GetCorrecPeriod(listPeriod.Where(p => dateNights >= p.Begin && dateNights <= p.End).ToList(), dateNights);
                if (currentPeriod == null)
                {
                    validationDictionnary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "Period"), GenericError.CANNOT_BE_NULL_OR_EMPTY);
                    return;
                }
                repo.includes.Add("Tax");
                if (peopleCategoryId == null)
                    ppp = repo.GetPricePerPersonForGivenPeriodAndRoom(currentPeriod.Id, orig.Room.Id, currentClient.Id);
                else
                    ppp = repo.GetPricePerPersonForGivenPeriodPeopleCategoryAndRoom(currentPeriod.Id, (int)peopleCategoryId, orig.Room.Id, currentClient.Id);
                if (ppp == null)
                {
                    validationDictionnary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "PricePerPerson"), GenericError.CANNOT_BE_NULL_OR_EMPTY);
                    return;
                }
                Decimal tmpTTC = (Decimal)(ppp.PriceHT) * coeff;
                if (ppp.Tax != null)
                    tmpTTC = ComputePrice.ComputePriceFromPercentOrAmount(tmpTTC, (EValueType)ppp.Tax.ValueType,
                        (EValueType)ppp.Tax.ValueType == EValueType.AMOUNT ? (Decimal)ppp.Tax.Price * coeff : (Decimal)ppp.Tax.Price);
                finalPriceHT += (Decimal)(ppp.PriceHT) * coeff;
                finalPriceTTC += tmpTTC;
            }
        }

        private void ComputePricePerPerson(List<PeopleBooking> listPeopleBooking, List<Period> listPeriod, Client currentClient)
        {
            if (listPeopleBooking.Count > MAX_PEOPLEBOOKING_LENGTH)
            {
                validationDictionnary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "PeopleBooking"), GenericError.DOES_NOT_MEET_REQUIREMENTS);
                return;
            }
            foreach (PeopleBooking cur in listPeopleBooking)
            {
                UpdatePrices(listPeriod, (DateTime)cur.DateBegin, (DateTime)cur.DateEnd, currentClient, cur.NumberOfPeople, cur.PeopleCategory.Id);
            }
        }
    }
}