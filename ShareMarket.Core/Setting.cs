using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMarket.Core
{
    [Table("Setting")]
    public class Setting : BaseEntityId
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
