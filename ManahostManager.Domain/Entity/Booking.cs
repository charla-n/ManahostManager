using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    [Table("Booking")]
    public class Booking : IEntity
    {
        public Booking()
        {
            AdditionalBookings = new List<Entity.AdditionalBooking>();
            BookingDocuments = new List<Entity.BookingDocument>();
            Deposits = new List<Entity.Deposit>();
            DinnerBookings = new List<Entity.DinnerBooking>();
            ProductBookings = new List<Entity.ProductBooking>();
            RoomBookings = new List<Entity.RoomBooking>();
        }

        [Key]
        public int Id { get; set; }

        public String Comment { get; set; }

        public DateTime DateArrival { get; set; }

        public DateTime DateCreation { get; set; }

        public DateTime DateDeparture { get; set; }

        public DateTime? DateDesiredPayment { get; set; }

        public DateTime? DateModification { get; set; }

        public DateTime? DateValidation { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public Boolean IsOnline { get; set; }

        public Boolean IsSatisfactionSended { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public People People { get; set; }

        public int TotalPeople { get; set; }

        public List<AdditionalBooking> AdditionalBookings { get; set; }
        public List<BookingDocument> BookingDocuments { get; set; }
        public BookingStepBooking BookingStepBooking { get; set; }
        public List<Deposit> Deposits { get; set; }
        public List<DinnerBooking> DinnerBookings { get; set; }
        public List<ProductBooking> ProductBookings { get; set; }
        public List<RoomBooking> RoomBookings { get; set; }

        public int? GetFK()
        {
            return HomeId;
        }

        public void SetDateModification(DateTime? d)
        {
            DateModification = d;
        }
    }
}