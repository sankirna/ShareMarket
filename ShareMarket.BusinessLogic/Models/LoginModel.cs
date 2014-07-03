using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMarket.BusinessLogic.Models
{
    public class LoginModel
    {
        #region "Member(s)"

        [Required(ErrorMessage = "Please enter user name")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        #endregion
    }
}
