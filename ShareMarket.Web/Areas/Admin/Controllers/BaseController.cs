using ShareMarket.Web.Areas.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShareMarket.Web.Areas.Admin.Controllers
{
    [AuthorizedUseAttribute(IsCheck = true)]
    public class BaseController : Controller
    {

    }
}
