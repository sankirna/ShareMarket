using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using ShareMarket.BusinessLogic.Libs;
using ShareMarket.BusinessLogic.Models;
using ShareMarket.Core;

namespace ShareMarket.Web.Controllers
{
    public class HomeController : Controller
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

        public ActionResult Index()
        {
            //TraderModel model = new TraderModel();
            //model.StringBirthDate = "06/06/1988";
            //model.ExprInStockMarketMm = 02;
            //model.ExprInStockMarketYy = 1;
            //model.UserProfile = new UserProfile();
            //model.UserProfile.UserName = "Sankirna rana" + Guid.NewGuid();
            //model.UserProfile.Password = "Test#123";
            //model.BirthDate = DateTime.Now;
            //using (TraderLib traderLib = _context.Resolve<TraderLib>())
            //{
            //   TraderModel tm= traderLib.GetEntityById(1);
            //    traderLib.AddUpdateEnity(model);
            //}

            return View();
        }

        public ActionResult AddEditView(int id)
        {
            using (TraderLib traderLib = _context.Resolve<TraderLib>())
            {
                TraderModel tm = traderLib.GetEntityById(id);
                return View(tm);
            }

        }

        [HttpPost]
        public ActionResult AddEditView(TraderModel model)
        {
            using (TraderLib traderLib = _context.Resolve<TraderLib>())
            {
                model = traderLib.AddUpdateEnity(model);
                if (model.ErrorList.Count <= 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
        }


    }
}
