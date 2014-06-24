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
    public class UsersRole : BaseEntityId
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserProfile UserProfile { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Roles Roles { get; set; }
    }
}
