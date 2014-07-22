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
    public class CmsPageController : BaseController
    {
        //
        // GET: /Admin/CmsPage/

        #region "Private Member(s)"

        private IComponentContext _context;

        #endregion

        #region "Constructor(s)"

        /// <summary>
        /// Public Constructor.
        /// </summary>
        /// <param name="context">IComponentContext</param>
        public CmsPageController(IComponentContext context)
        {
            _context = context;
        }

        #endregion

        #region "Public Member(s)"

        public ActionResult List()
        {
            using (CmsPageLib cmsPageLib = _context.Resolve<CmsPageLib>())
            {
                List<CmsPage> cmsPages = cmsPageLib.GetList().ToList();
                return View(cmsPages);
            }
        }

        public ActionResult AddEditView(int id)
        {
            using (CmsPageLib cmsPageLib = _context.Resolve<CmsPageLib>())
            {
                CmsPage cmsPage = cmsPageLib.GetEntityById(id);
                return View(cmsPage);
            }

        }

        [HttpPost]
        public ActionResult AddEditView(CmsPage model)
        {
            using (CmsPageLib cmsPageLib = _context.Resolve<CmsPageLib>())
            {
                model = cmsPageLib.AddUpdateEnity(model);
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
            using (CmsPageLib cmsPageLib = _context.Resolve<CmsPageLib>())
            {
                cmsPageLib.Delete(id);
                return RedirectToAction("Index");
            }
        }


        #endregion


    }
}
