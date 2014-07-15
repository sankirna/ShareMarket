using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using ShareMarket.Core;
using ShareMarket.DataAccess.Repository;
using ShareMarket.Utility.Utilities;

namespace ShareMarket.BusinessLogic.Libs
{
    public class SettingLib : IDisposable
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
        public SettingLib(IComponentContext context)
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
        public IQueryable<Setting> GetList()
        {
            try
            {
                IQueryable<Setting> entities = _context.Resolve<IDataRepository<Setting>>()
                    .GetAll();
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
        public Setting GetEntityById(int Id)
        {
            Setting setting;
            try
            {
                using (IDataRepository<Setting> settingContext = _context.Resolve<IDataRepository<Setting>>())
                {
                    if (Id > 0)
                    {
                        setting = settingContext.FirstOrDefault(x => x.Id == Id);
                        if (setting == null)
                        {
                            throw new Exception("Id not found");
                        }
                        return setting;
                    }
                }
                setting = new Setting();

                return setting;
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
        /// <returns>true/false</returns>
        public Setting AddUpdateEnity(Setting model)
        {
            try
            {   // Check company is not null
                if (model != null)
                {
                    using (IDataRepository<Setting> settingContext = _context.Resolve<IDataRepository<Setting>>())
                    {

                        if (model.Id == 0)
                        {
                            settingContext.Add(model);
                        }
                        else
                        {
                            settingContext.Update(model);
                        }

                        settingContext.SaveChanges();

                        return model;
                    }

                }
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }

            model.ErrorList.Add("some error  O");

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
                    using (IDataRepository<Setting> settingContext = _context.Resolve<IDataRepository<Setting>>())
                    {
                        settingContext.Delete(x => x.Id == id);
                        settingContext.SaveChanges();
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
