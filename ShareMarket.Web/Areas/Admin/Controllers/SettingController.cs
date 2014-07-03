using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;

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

        #endregion


    }
}
