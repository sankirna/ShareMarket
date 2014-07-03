using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareMarket.Web.Areas.Admin.Filters;

namespace ShareMarket.Web.Areas.Admin.Controllers
{
    [AuthorizedUse(IsCheck = true)]
    public class BaseController : Controller
    {

    }
}
