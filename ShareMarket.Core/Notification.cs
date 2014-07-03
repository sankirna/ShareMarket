using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMarket.Core
{
    public class Notification
    {
        public Notification()
        {
                ErrorList=new List<string>();
        }
        [NotMapped]
        public List<string>  ErrorList { get; set; }
    }
}
