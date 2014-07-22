using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using ShareMarket.BusinessLogic.Helpers;
using ShareMarket.Core;
using ShareMarket.Core.Enums;
using ShareMarket.BusinessLogic.Models;
using ShareMarket.DataAccess.Repository;
using ShareMarket.Utility.Enum;
using ShareMarket.Utility.Utilities;

namespace ShareMarket.BusinessLogic.Libs
{
    public class CmsPageLib : IDisposable
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
        public CmsPageLib(IComponentContext context)
        {
            _context = context;
        }

        #endregion

        #region "Public Member(s)"

        /// <summary>
        /// Get Hotel List based on client id
        /// </summary>
        /// <param name="clientId">Logging user client Id</param>
        /// <returns>List of Hotel model </returns>
        public IQueryable<CmsPage> GetList()
        {
            try
            {
                IQueryable<CmsPage> entities = _context.Resolve<IDataRepository<CmsPage>>()
                                                    .Fetch(u => (u.IsDeleted == null || u.IsDeleted == false));
                return entities;
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public CmsPage GetEntityById(int Id)
        {
            CmsPage cmsPage;
            try
            {
                using (IDataRepository<CmsPage> cmsPageContext = _context.Resolve<IDataRepository<CmsPage>>())
                {
                    if (Id > 0)
                    {
                        cmsPage = cmsPageContext.FirstOrDefault(x => x.Id == Id);
                        if (cmsPage == null)
                        {
                            throw new Exception("Id not found");
                        }
                        return cmsPage;
                    }
                }
                cmsPage = new CmsPage();
                return cmsPage;
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
            return null;
        }

        /// <summary>
        /// Create New User Web Security
        /// </summary>
        /// <param name="model"></param>
        /// <param name="model"></param>
        /// <returns>true/false</returns>
        public CmsPage AddUpdateEnity(CmsPage model)
        {
            using (UserLib userLib = _context.Resolve<UserLib>())
            {
                // TODO ReamingSet
                // Check Validation


                using (IDataRepository<CmsPage> cmsPageContext = _context.Resolve<IDataRepository<CmsPage>>())
                {

                    //Create new User
                    if (model.Id == 0)
                    {
                        cmsPageContext.Add(model);
                    }
                    else
                    {
                        cmsPageContext.Update(model);

                    }
                    cmsPageContext.SaveChanges();
                }

            }
            return model;
        }

        /// <summary>
        /// Delete Hotel  by hotel Id
        /// </summary>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns>true/false</returns>
        public bool Delete(int id)
        {
            try
            {   // Check customer is not null
                if (id != 0)
                {
                    using (IDataRepository<CmsPage> cmsPageContext = _context.Resolve<IDataRepository<CmsPage>>())
                    {
                        cmsPageContext.Delete(id);
                        cmsPageContext.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }

            return false;
        }

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
