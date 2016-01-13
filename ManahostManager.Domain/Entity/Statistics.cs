using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    public enum StatisticsTypes
    {
        ACCOUNTING,
        BOOKING,
        SATISFACTION
    };

    public class SatisfactionStats
    {
        public List<String> Q { get; set; }

        public List<List<int>> A { get; set; }

        public int T { get; set; }
    }

    public class PeopleCategoryStat
    {
        public String Title { get; set; }

        public int Number { get; set; }
    }

    public class ProductStat
    {
        public String Title { get; set; }

        public int Number { get; set; }
    }

    public class RoomStat
    {
        public String Title { get; set; }

        public List<int> Days { get; set; }
    }

    public class BookingStats
    {
        public int NumberCancelled { get; set; }

        public int Online { get; set; }

        public int TotalNight { get; set; }

        public int TotalRes { get; set; }

        public int TotalPeople { get; set; }

        public List<PeopleCategoryStat> PeopleCategories { get; set; }

        public List<ProductStat> Products { get; set; }

        public int TotalProducts { get; set; }

        public List<RoomStat> Rooms { get; set; }

        public int TotalRooms { get; set; }

        public List<List<int>> Dinners { get; set; }

        public int TotalDinners { get; set; }
    }

    public class RoomAccounting
    {
        public String Title { get; set; }

        public int Income { get; set; }
    }

    public class ProductAccounting
    {
        public String Title { get; set; }

        public int Income { get; set; }
    }

    public class AccountingStat
    {
        public int Income { get; set; }

        public int Outcome { get; set; }

        public List<RoomAccounting> Rooms { get; set; }

        public List<ProductAccounting> Products { get; set; }

        public List<int> Dinners { get; set; }

        public int Other { get; set; }
    }

    [Table("MStatistics")]
    public class MStatistics : IEntity
    {
        [Key]
        public int Id { get; set; }

        public String Data { get; set; }

        public DateTime Date { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public StatisticsTypes Type { get; set; }

        public DateTime? DateModification { get; set; }

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