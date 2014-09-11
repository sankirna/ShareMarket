
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareMarket.Core
{
    [Table("Trader")]
    public class Trader : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserProfile UserProfile { get; set; }

        public DateTime BirthDate { get; set; }

        public string ExpInStockMarket { get; set; }

    }
}
