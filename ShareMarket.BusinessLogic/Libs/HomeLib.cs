using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace ShareMarket.BusinessLogic.Libs
{
    public class HomeLib : IDisposable
    {
        #region "Private Member(s)"

        private bool _disposed = false;
        private IComponentContext _context;

        #endregion

        #region "Constructor(s)"

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="context">IComponentContext</param>
        public HomeLib(IComponentContext context)
        {
            _context = context;
        }

        #endregion

        #region "Public Member(s)"

       

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose Logic
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // dispose-only, i.e. non-finalizable logic
                    _context = null;
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
