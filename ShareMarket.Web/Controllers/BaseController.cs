using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;

namespace ShareMarket.Web.Controllers
{
    public class BaseController : Controller
    {
        #region "Private Member(s)"

        protected readonly IComponentContext _context;

        #endregion

        #region "Constructor(s)"

        /// <summary>
        /// Public Constructor.
        /// </summary>
        /// <param name="context">IComponentContext</param>
        public BaseController(IComponentContext context)
        {
            _context = context;
        }

        #endregion



    }
}
