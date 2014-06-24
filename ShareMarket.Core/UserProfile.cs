using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareMarket.Core.Enums;

namespace ShareMarket.Core
{
    [Table("UserProfile")]
    public class UserProfile : BaseEntityId
    {
        public string UserName { get; set; }

        public UserType UserType { get; set; }

        public virtual Membership Membership { get; set; }

        public virtual ICollection<Roles> Roles { get; set; }
    }
}
