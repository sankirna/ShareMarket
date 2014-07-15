using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using ShareMarket.BusinessLogic.Libs;
using ShareMarket.Core;

namespace ShareMarket.Web.Areas.Admin.Controllers
{
    public class SettingController : BaseController
    {
        //
        // GET: /Admin/Setting/

        #region "Private Member(s)"

        private IComponentContext _context;

        #endregion

        #region "Constructor(s)"

        /// <summary>
        /// Public Constructor.
        /// </summary>
        /// <param name="context">IComponentContext</param>
        public SettingController(IComponentContext context)
        {
            _context = context;
        }

        #endregion

        #region "Public Member(s)"

        public ActionResult List()
        {
            using (SettingLib settingLib = _context.Resolve<SettingLib>())
            {
                List<Setting> settings = settingLib.GetList().ToList();
                return View(settings);
            }
        }

        public ActionResult AddEditView(int id)
        {
            using (SettingLib settingLib = _context.Resolve<SettingLib>())
            {
                Setting setting = settingLib.GetEntityById(id);
                return View(setting);
            }

        }

        [HttpPost]
        public ActionResult AddEditView(Setting model)
        {
            using (SettingLib settingLib = _context.Resolve<SettingLib>())
            {
                model = settingLib.AddUpdateEnity(model);
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

        public ActionResult Delete(int id)
        {
            using (SettingLib settingLib = _context.Resolve<SettingLib>())
            {
                settingLib.Delete(id);
                return RedirectToAction("Index");
            }
        }


        #endregion


    }
}
