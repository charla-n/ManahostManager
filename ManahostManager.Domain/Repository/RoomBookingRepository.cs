using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System;
using System.Collections.Generic;

namespace ManahostManager.Domain.Repository
{
    public interface IRoomBookingRepository : IAbstractRepository<RoomBooking>
    {
        RoomBooking GetRoomBookingById(int id, int clientId);

        RoomBooking FindRoomBookingForDates(int roomId, int roomBookingId, DateTime from, DateTime to, int clientId);

        PricePerPerson GetPricePerPersonForGivenPeriodPeopleCategoryAndRoom(int periodId, int peopleCategoryId, int roomId, int clientId);

        IEnumerable<Period> GetPeriods(DateTime dateBegin, DateTime dateEnd, int clientId);

        IEnumerable<PeopleBooking> GetPeopleBookingFromRoomBooking(int roomBookingId, int clientId);

        PricePerPerson GetPricePerPersonForGivenPeriodAndRoom(int periodId, int roomId, int clientId);
    }

    public class RoomBookingRepository : AbstractRepository<RoomBooking>, IRoomBookingRepository
    {
        public RoomBookingRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public RoomBooking GetRoomBookingById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }

        public RoomBooking FindRoomBookingForDates(int roomId, int roomBookingId, DateTime from, DateTime to, int clientId)
        {
            includes.Add("Room");
            return GetUniq(p => p.Room.Id == roomId && p.Home.ClientId == clientId && p.Id != roomBookingId && (
                p.DateBegin <= from && p.DateEnd >= to || /* | ---- | */
                p.DateBegin <= from && p.DateEnd >= from && p.DateEnd <= to || /* | --|-- */
                p.DateBegin >= from && p.DateBegin <= to && p.DateEnd >= to || /* --|-- | */
                p.DateBegin >= from && p.DateEnd <= to && p.DateBegin <= to && p.DateEnd >= from) /* -|--|- */);
        }

        public PricePerPerson GetPricePerPersonForGivenPeriodPeopleCategoryAndRoom(int periodId, int peopleCategoryId, int roomId, int clientId)
        {
            includes.Add("PeopleCategory");
            includes.Add("Period");
            includes.Add("Room");
            return GetUniq<PricePerPerson>(p => p.Home.ClientId == clientId && p.PeopleCategory.Id == peopleCategoryId && p.Period.Id == periodId && p.Room.Id == roomId);
        }

        public PricePerPerson GetPricePerPersonForGivenPeriodAndRoom(int periodId, int roomId, int clientId)
        {
            includes.Add("PeopleCategory");
            includes.Add("Period");
            includes.Add("Room");
            return GetUniq<PricePerPerson>(p => p.Home.ClientId == clientId && p.PeopleCategory == null && p.Period.Id == periodId && p.Room.Id == roomId);
        }

        public IEnumerable<Period> GetPeriods(DateTime dateBegin, DateTime dateEnd, int clientId)
        {
            return GetList<Period>(p => p.Home.ClientId == clientId && (
                p.Begin <= dateBegin && p.End >= dateEnd || /* | ---- | */
                p.Begin <= dateBegin && p.End >= dateBegin && p.End <= dateEnd || /* | --|-- */
                p.Begin >= dateBegin && p.Begin <= dateEnd && p.End >= dateEnd || /* --|-- | */
                p.Begin >= dateBegin && p.End <= dateEnd && p.Begin <= dateEnd && p.End >= dateBegin) /* -|--|- */);
        }

        public IEnumerable<PeopleBooking> GetPeopleBookingFromRoomBooking(int roomBookingId, int clientId)
        {
            includes.Add("RoomBooking");
            return GetList<PeopleBooking>(p => p.RoomBooking.Id == roomBookingId && p.Home.ClientId == clientId);
        }
    }
}