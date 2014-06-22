using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareMarket.Core
{
     public class Student : BaseEntity
    {
        public Student() { }

        public int StudentId { get; set; }
     //[Required]
        public string StudentName { get; set; }

       // [Required]
        public virtual StudentAddress StudentAddress { get; set; }

    }

     public class StudentAddress
     {
         [Key, ForeignKey("Student")]
         public int StudentId { get; set; }

         public string Address1 { get; set; }
         public string Address2 { get; set; }
         public string City { get; set; }
         public int Zipcode { get; set; }
         public string State { get; set; }
         public string Country { get; set; }

         public virtual Student Student { get; set; }
     }
}
