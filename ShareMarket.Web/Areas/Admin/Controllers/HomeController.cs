using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using ShareMarket.BusinessLogic.Libs;
using ShareMarket.BusinessLogic.Models;
using ShareMarket.Web.Areas.Admin.Filters;

namespace ShareMarket.Web.Areas.Admin.Controllers
{

    public class HomeController : BaseController
    {


        #region "Private Member(s)"

        private IComponentContext _context;

        #endregion

        #region "Constructor(s)"

        /// <summary>
        /// Public Constructor.
        /// </summary>
        /// <param name="context">IComponentContext</param>
        public HomeController(IComponentContext context)
        {
            _context = context;
        }

        #endregion

        #region "Public Member(s)"

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Return Login.cshtml
        /// </summary>
        /// <returns>View(Login)</returns>
        [AuthorizedUse(IsCheck = false)]
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        /// <summary>
        /// POST method for login
        /// </summary>
        /// <param name="model">Login model</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizedUse(IsCheck = false)]
        public ActionResult Login(LoginModel model)
        {
            using (HomeLib homeLib = _context.Resolve<HomeLib>())
            {
                bool validUser = homeLib.Login(model);
                if (!validUser)
                {
                    ViewBag.Errormsg = "Invalid username or password";
                    return View();
                }

                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}
