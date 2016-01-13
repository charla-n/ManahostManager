using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManahostManager.Domain.Repository
{
    public interface IDepositRepository : IAbstractRepository<Deposit>
    {
        Deposit GetDepositById(int id, int clientId);

        Deposit GetDepositByBookingId(int id, int bookingId, int clientId);

        Decimal GetCurrentPriceOfTheBooking(int bookingId, int clientId);
    }

    public class DepositRepository : AbstractRepository<Deposit>, IDepositRepository
    {
        public DepositRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public Deposit GetDepositById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }

        public Deposit GetDepositByBookingId(int id, int bookingId, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Booking.Id == bookingId && p.Home.ClientId == clientId);
        }

        public decimal GetCurrentPriceOfTheBooking(int bookingId, int clientId)
        {
            includes.Add("Booking");
            IEnumerable<AdditionalBooking> listAdditionalBooking = GetList<AdditionalBooking>(p => p.Booking.Id == bookingId && p.Home.ClientId == clientId);
            includes.Add("Booking");
            IEnumerable<DinnerBooking> listDinnerBooking = GetList<DinnerBooking>(p => p.Booking.Id == bookingId && p.Home.ClientId == clientId);
            includes.Add("Booking");
            IEnumerable<ProductBooking> listProductBooking = GetList<ProductBooking>(p => p.Booking.Id == bookingId && p.Home.ClientId == clientId);
            includes.Add("Booking");
            IEnumerable<RoomBooking> listRoomBooking = GetList<RoomBooking>(p => p.Booking.Id == bookingId && p.Home.ClientId == clientId);

            return (Decimal)(listAdditionalBooking.Sum(p => p.PriceTTC) + listDinnerBooking.Sum(p => p.PriceTTC) + listProductBooking.Sum(p => p.PriceTTC) + listRoomBooking.Sum(p => p.PriceTTC));
        }
    }
}