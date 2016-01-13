using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManahostManager.Domain.Entity
{
    // Only administrator
    // Set the price of manahost

    [Table("PriceManahost")]
    public class PriceManahost
    {
        [Key]
        public int Id { get; set; }

        public int NumberRoomsFrom { get; set; }

        public int NumberRoomsTo { get; set; }

        public Decimal Price { get; set; }
    }
}