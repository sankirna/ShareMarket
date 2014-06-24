using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareMarket.Core
{
    public class BaseEntityId
    {
        [Key]
        public int Id { get; set; }
    }

    public class BaseEntity : BaseEntityId
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }




        //[ForeignKey("UserProfile")]
        //public int UpdatedByUserId { get; set; }
        // [ForeignKey("UpdatedByUserId")]
        //public virtual UserProfile UpdatedByUser { get; set; }


    }
}
