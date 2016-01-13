using ManahostManager.App_Start;
using ManahostManager.Controllers;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class PeopleBookingService : AbstractService<PeopleBooking, PeopleBookingDTO>
    {
        private new IPeopleBookingRepository repo;

        public IAbstractService<PeopleCategory, PeopleCategoryDTO> PeopleCategoryService { get { return GetService<IAbstractService<PeopleCategory, PeopleCategoryDTO>>(); } private set { } }

        public IAbstractService<RoomBooking, RoomBookingDTO> RoomBookingService { get { return GetService<IAbstractService<RoomBooking, RoomBookingDTO>>(); } private set { } }

        public PeopleBookingService(IPeopleBookingRepository repo, IValidation<PeopleBooking> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(PeopleBookingDTO dto, int id, Client currentClient)
        {
            orig = repo.GetPeopleBookingById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override PeopleBooking DoPostPutDto(Client currentClient, PeopleBookingDTO dto, PeopleBooking entity, string path, object param)
        {
            if (entity == null)
                entity = new PeopleBooking();
            GetMapper.Map(dto, entity);
            if (dto.PeopleCategory != null)
                entity.PeopleCategory = PeopleCategoryService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.PeopleCategory, currentClient, path);
            if (dto.RoomBooking != null)
                entity.RoomBooking = RoomBookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.RoomBooking, currentClient, path);
            return entity;
        }

        protected override void DoPostAfterSave(Client currentClient, PeopleBooking entity, object param)
        {
            IncludeCommonProps();
            ProcessDTOPostPut(null, entity.Id, currentClient);
            Booking b = orig.RoomBooking.Booking;

            b.TotalPeople += orig.NumberOfPeople;
            if (orig.DateBegin == null || orig.DateEnd == null)
            {
                if (orig.DateBegin == null)
                    orig.DateBegin = orig.RoomBooking.DateBegin;
                if (orig.DateEnd == null)
                    orig.DateEnd = orig.RoomBooking.DateEnd;
                repo.Update(orig);
            }
            repo.Update<Booking>(b);
            repo.Save();
        }

        private void IncludeCommonProps()
        {
            repo.includes.Add("RoomBooking.Booking");
        }

        protected override void DeleteIncludeProps()
        {
            IncludeCommonProps();
        }

        protected override void PutIncludeProps(PeopleBookingDTO dto)
        {
            if (dto.RoomBooking != null)
                repo.includes.Add("RoomBooking");
            if (dto.PeopleCategory != null)
                repo.includes.Add("PeopleCategory");
            IncludeCommonProps();
        }

        protected override void DoPut(Client currentClient, PeopleBooking entity, object param)
        {
            PeopleBooking beforeSave = repo.GetPeopleBookingById(entity.Id, currentClient.Id);

            entity.RoomBooking.Booking.TotalPeople += entity.NumberOfPeople - beforeSave.NumberOfPeople;
            repo.Update<Booking>(entity.RoomBooking.Booking);
            base.DoPut(currentClient, entity, param);
        }

        protected override void DoDelete(Client currentClient, int id, object param)
        {
            Booking b = orig.RoomBooking.Booking;

            b.TotalPeople -= orig.NumberOfPeople;
            repo.Update<Booking>(b);
        }
    }
}