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
    public class TraderLib : IDisposable
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
        public TraderLib(IComponentContext context)
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
        public IQueryable<Trader> GetList()
        {
            try
            {
                IQueryable<Trader> entities = _context.Resolve<IDataRepository<Trader>>()
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
        public TraderModel GetEntityById(int Id)
        {
            Trader trader;
            try
            {
                using (IDataRepository<Trader> traderContext = _context.Resolve<IDataRepository<Trader>>())
                {
                    if (Id > 0)
                    {
                        trader = traderContext.FirstOrDefault(x => x.Id == Id);
                        if (trader == null)
                        {
                            throw new Exception("Id not found");
                        }
                        return Mapper.Map<Trader, TraderModel>(trader);
                    }
                }
                trader = new Trader();
                trader.ExpInStockMarket = "0_0";
                trader.UserProfile = new UserProfile();
                return Mapper.Map<Trader, TraderModel>(trader);
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
        public TraderModel AddUpdateEnity(TraderModel model)
        {
            Trader trader = Mapper.Map<TraderModel, Trader>(model);
            using (UserLib userLib = _context.Resolve<UserLib>())
            {
                // TODO ReamingSet
                // Check Validation
                bool isUseExists = trader.UserId <= 0
                    ? userLib.IsUserExists(trader.UserProfile.UserName)
                    : userLib.IsEditUserExists(trader.UserProfile.UserName, trader.UserId);
                if (isUseExists)
                {
                    trader.ErrorList.Add("User Name already exists");
                    return Mapper.Map<Trader, TraderModel>(trader);
                }


                using (IDataRepository<UserProfile> userProfileContext = _context.Resolve<IDataRepository<UserProfile>>())
                using (IDataRepository<Trader> traderContext = _context.Resolve<IDataRepository<Trader>>())
                {

                    //Create new User
                    if (trader.UserId == 0)
                    {
                        bool isUserCreate = userLib.CreateUser(trader.UserProfile,
                            trader.UserProfile.Password,
                            RoleType.Trader, UserType.Trader);
                        UserProfile userProfile = GlobalLib.GetUserByName(trader.UserProfile.UserName);

                        trader.UserId = userProfile.Id;
                        trader.UserProfile = null;
                        trader.CreatedByUserId = trader.UpdatedByUserId = userProfile.Id;
                        traderContext.Add(trader);
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(trader.UserProfile.Password))
                        {
                            userLib.UpdatePassword(trader.UserProfile.UserName, trader.UserProfile.Password);
                        }

                        userProfileContext.Update(trader.UserProfile);
                        trader.UserProfile = null;
                        traderContext.Update(trader);

                    }
                    traderContext.SaveChanges();
                }

            }
            return Mapper.Map<Trader, TraderModel>(trader);
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
                    using (IDataRepository<Trader> traderContext = _context.Resolve<IDataRepository<Trader>>())
                    {
                        traderContext.Delete(id);
                        traderContext.SaveChanges();
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
