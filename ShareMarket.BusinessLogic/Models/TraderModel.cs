using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareMarket.Core;

namespace ShareMarket.BusinessLogic.Models
{
    public class TraderModel : Trader
    {
        public string StringBirthDate { get; set; }

        public int ExprInStockMarketMm { get; set; }

        public int ExprInStockMarketYy { get; set; }

    }
}
