
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareMarket.Core
{
    [Table("Trader")]
    public class Trader : BaseEntity
    {
        public string Name { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserProfile UserProfile { get; set; }

        public int CreatedByUserId { get; set; }
        [ForeignKey("CreatedByUserId")]
        public virtual UserProfile CreatedByUser { get; set; }
    }
}
