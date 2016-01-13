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
    public class DinnerBookingService : AbstractService<DinnerBooking, DinnerBookingDTO>
    {
        private new IDinnerBookingRepository repo;

        public DinnerBookingService(IDinnerBookingRepository repo, IValidation<DinnerBooking> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(DinnerBookingDTO dto, int id, Client currentClient)
        {
            orig = repo.GetDinnerBookingById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(DinnerBookingDTO dto)
        {
            if (dto.MealBookings != null)
                repo.includes.Add("MealBookings");
            if (dto.Booking != null)
                repo.includes.Add("Booking");
        }

        protected override void SetDefaultValues(DinnerBooking entity)
        {
            entity.PriceHT = entity.PriceHT ?? 0M;
            entity.PriceTTC = entity.PriceTTC ?? entity.PriceHT;
        }

        
        public IMealBookingRepository MealRepository { get { return GetService<IMealBookingRepository>(); } private set { } }

        
        public IAbstractService<MealBooking, MealBookingDTO> MealBookingService { get { return GetService<IAbstractService<MealBooking, MealBookingDTO>>(); } private set { } }

        
        public IAbstractService<Booking, BookingDTO> BookingService { get { return GetService<IAbstractService<Booking, BookingDTO>>(); } private set { } }

        protected override DinnerBooking DoPostPutDto(Client currentClient, DinnerBookingDTO dto, DinnerBooking entity, string path, object param)
        {
            if (entity == null)
                entity = new DinnerBooking();
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
            if (dto.MealBookings != null)
            {
                MealRepository.DeleteRange(entity.MealBookings.Where(d => !dto.MealBookings.Any(x => x.Id == d.Id)));
                dto.MealBookings.ForEach(mealBooking =>
                {
                    if (entity.MealBookings.Count != 0 && mealBooking.Id != 0 &&
                    entity.MealBookings.Find(p => p.Id == mealBooking.Id) != null)
                        return;
                    MealBooking toAdd = MealBookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, mealBooking, currentClient, path);

                    if (toAdd != null)
                        entity.MealBookings.Add(toAdd);
                });
            }
            return entity;
        }

        public virtual DinnerBookingDTO Compute(Client currentClient, int id, object param)
        {
            Decimal priceHT = 0;
            Decimal priceTTC = 0;
            IEnumerable<MealBooking> mealBookings = null;

            ProcessDTOPostPut(null, id, currentClient);
            ValidateOrig();
            repo.includes.Add("PeopleCategory");
            repo.includes.Add("Meal");
            mealBookings = repo.GetAllMealByDinnerBooking(id, currentClient.Id);
            foreach (MealBooking cur in mealBookings)
            {
                repo.includes.Add("Tax");
                MealPrice priceForMeal = repo.GetMealPriceByPeopleCategoryId(cur.PeopleCategory.Id, (int)cur.Meal.Id, currentClient.Id);
                if (priceForMeal == null)
                {
                    validationDictionnary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<DinnerBooking>(), "MealPrice"), GenericError.CANNOT_BE_NULL_OR_EMPTY);
                    throw new ManahostValidationException(validationDictionnary);
                }
                Decimal tmpPrice = (Decimal)priceForMeal.PriceHT * (int)cur.NumberOfPeople;

                priceHT += tmpPrice;
                if (priceForMeal.Tax != null)
                {
                    priceTTC += ComputePrice.ComputePriceFromPercentOrAmount(tmpPrice, (EValueType)priceForMeal.Tax.ValueType,
                        (EValueType)priceForMeal.Tax.ValueType == EValueType.AMOUNT ? (Decimal)priceForMeal.Tax.Price * (int)cur.NumberOfPeople : (Decimal)priceForMeal.Tax.Price);
                }
                else
                    priceTTC += tmpPrice;
            }
            orig.PriceHT = priceHT;
            orig.PriceTTC = priceTTC;
            repo.Update(orig);
            repo.Save();
            return GetMapper.Map<DinnerBooking, DinnerBookingDTO>(orig);
        }
    }
}