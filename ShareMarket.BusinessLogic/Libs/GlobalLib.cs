using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ShareMarket.Core;
using ShareMarket.DataAccess;
using ShareMarket.Utility.Utilities;

namespace ShareMarket.BusinessLogic.Libs
{
    public class GlobalLib : IDisposable
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
        public GlobalLib(IComponentContext context)
        {
            _context = context;
        }

        #endregion

        #region "Public Member(s)"

        public static string SettingValue(string name)
        {
            try
            {
                using (ShareMarketDbContext shareMarketDbContext = new ShareMarketDbContext())
                {
                    Setting setting = shareMarketDbContext.Settings.FirstOrDefault(x => x.Name == name);
                    if (setting != null) return setting.Value;
                }
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(GlobalLib));
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets all tasks
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Tasks</returns>
        public static List<ScheduleTask> GetAll(bool showHidden = false)
        {
            try
            {
                using (ShareMarketDbContext shareMarketDbContext = new ShareMarketDbContext())
                {
                    var tasks = shareMarketDbContext.ScheduleTasks.ToList();
                    if (!showHidden)
                    {
                        tasks = tasks.Where(t => t.Enabled).ToList();
                    }
                    tasks = tasks.OrderByDescending(t => t.Seconds).ToList();

                    return tasks;
                }
            }
            catch (Exception ex)
            {
                ex.LogError(typeof (GlobalLib));
            }
            return null;
        }



        /// <summary>
        /// Gets a task by its type
        /// </summary>
        /// <param name="type">Task type</param>
        /// <returns>Task</returns>
        public static ScheduleTask GetTaskByType(string type)
        {
            try
            {
                using (ShareMarketDbContext shareMarketDbContext = new ShareMarketDbContext())
                {
                    if (String.IsNullOrWhiteSpace(type))
                        return null;

                    var tasks = shareMarketDbContext.ScheduleTasks.ToList();
                    tasks = tasks.Where(st => st.Type == type).ToList();
                    tasks = tasks.OrderByDescending(t => t.Id).ToList();

                    var task = tasks.FirstOrDefault();
                    return task;
                }
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(GlobalLib));
            }
            return null;
        }


        public static List<QueuedEmail> GetUnsentEmails()
        {
            using (ShareMarketDbContext shareMarketDbContext = new ShareMarketDbContext())
            {
                var maxTries = 5;
                var maxRecords = 10;
                var query = shareMarketDbContext.QueuedEmails.Where(e => e.SentOn == null);
                query = query.Where(e => e.SentTries < maxTries);
                query = query.OrderBy(e => e.CreatedOn);
                query = query.Take(maxRecords);
                return query.ToList();
            }
        }

        public static List<QueuedSms> GetUnsentSms()
        {
            using (ShareMarketDbContext shareMarketDbContext = new ShareMarketDbContext())
            {
                var maxTries = 5;
                var maxRecords = 10;
                var query = shareMarketDbContext.QueuedSmses.Where(e => e.SentOn == null);
                query = query.Where(e => e.SentTries < maxTries);
                query = query.OrderBy(e => e.CreatedOn).Take(maxRecords);
                return query.ToList();
            }
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
