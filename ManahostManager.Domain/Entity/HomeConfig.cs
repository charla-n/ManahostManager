using ManahostManager.Utils;
using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    [Table("HomeConfig")]
    public class HomeConfig : IEntity
    {
        [Key]
        public int Id { get; set; }

        public Boolean AutoSendSatisfactionEmail { get; set; }

        public Decimal? DefaultCaution { get; set; }

        public Boolean DepositNotifEnabled { get; set; }

        public int? DinnerCapacity { get; set; }

        public Boolean EnableDisplayActivities { get; set; }

        public Boolean EnableDisplayMeals { get; set; }

        public Boolean EnableDisplayProducts { get; set; }

        public Boolean EnableDisplayRooms { get; set; }

        public Boolean EnableReferencing { get; set; }

        public EValueType? DefaultValueType { get; set; }

        public Boolean? EnableDinner { get; set; }

        public Boolean FollowStockEnable { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Devise { get; set; }

        public DateTime? DateModification { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Document BookingCanceledMailTemplate { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public MailConfig DefaultMailConfig { get; set; }

        public long? DefaultHourCheckIn { get; set; }

        public long? DefaultHourCheckOut { get; set; }

        public Boolean? HourFormat24 { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public BillItemCategory DefaultBillItemCategoryMeal { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public BillItemCategory DefaultBillItemCategoryProduct { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public BillItemCategory DefaultBillItemCategoryRoom { get; set; }

        [NotMapped]
        public TimeSpan DefaultHourCheckInToTimeSpan
        {
            get
            {
                if (DefaultHourCheckIn != null)
                    return TimeSpan.FromMinutes((double)DefaultHourCheckIn);
                return TimeSpan.Zero;
            }
        }

        [NotMapped]
        public TimeSpan DefaultHourCheckOutToTimeSpan
        {
            get
            {
                if (DefaultHourCheckOut != null)
                    return TimeSpan.FromMinutes((double)DefaultHourCheckOut);
                return TimeSpan.Zero;
            }
        }

        public int? GetFK()
        {
            return Id;
        }

        public void SetDateModification(DateTime? d)
        {
            DateModification = d;
        }
    }
}