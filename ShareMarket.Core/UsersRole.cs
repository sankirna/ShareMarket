using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMarket.Core
{
    [Table("webpages_UsersInRoles")]
    public class UsersRole
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("UserProfile")]
        public int UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
