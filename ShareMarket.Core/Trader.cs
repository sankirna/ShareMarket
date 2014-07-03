
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareMarket.Core
{
    [Table("Trader")]
    public class Trader : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserProfile UserProfile { get; set; }

        public DateTime BirthDate { get; set; }

        public string ExpInStockMarket { get; set; }

        //[NotMapped]
        //public string StringBirthDate { get; set; }
        //[NotMapped]

        //public string Password { get; set; }
        //[NotMapped]

        //public int ExprInStockMarketMm { get; set; }
        //[NotMapped]

        //public int ExprInStockMarketYy { get; set; }

    }
}
