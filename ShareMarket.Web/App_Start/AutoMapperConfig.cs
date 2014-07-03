
using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using ShareMarket.BusinessLogic.Models;
using ShareMarket.Core;
using ShareMarket.Utility.Constants;

namespace ShareMarket.Web
{
    public class AutoMapperConfig
    {
        /// <summary>
        /// Create Mapping Model To Entity and Entity To Model
        /// </summary>
        public static void Mapping()
        {
            #region Trader

            //Enity
            Mapper.CreateMap<Trader, TraderModel>()
                .AfterMap((s, d) => d.StringBirthDate = s.BirthDate.ToString(CommonConstants.DateToStringFormate))
                .AfterMap((s, d) => d.ExprInStockMarketMm = Convert.ToInt16(s.ExpInStockMarket.Split('_')[0]))
                .AfterMap((s, d) => d.ExprInStockMarketYy = Convert.ToInt16(s.ExpInStockMarket.Split('_')[1]));


            //Model
            Mapper.CreateMap<TraderModel, Trader>()
                   //.BeforeMap(
                   // (s, d) =>
                   //     s.StringBirthDate =
                   //         DateTime.ParseExact(s.StringBirthDate, CommonConstants.BeforeMapStringToDateFormate, CultureInfo.InvariantCulture)
                   //             .ToString(CommonConstants.AfterMapStringToDateFormate))
                //.AfterMap(
                //    (s, d) =>
                //        s.StringBirthDate =
                //            DateTime.ParseExact(s.StringBirthDate, CommonConstants.AfterMapStringToDateFormate, CultureInfo.InvariantCulture)
                //                .ToString(CommonConstants.BeforeMapStringToDateFormate))
                 .AfterMap((s, d) => d.BirthDate = Convert.ToDateTime(s.StringBirthDate))
                .AfterMap(
                    (s, d) =>
                        d.ExpInStockMarket = string.Format("{0}_{1}", s.ExprInStockMarketMm, s.ExprInStockMarketYy));


            #endregion
        }

        /// <summary>
        /// Set Model to Entity and Entity to Model
        /// </summary>
        /// <typeparam name="T1">Model/Entity</typeparam>
        /// <typeparam name="T2">Model/Entity</typeparam>
        protected virtual void ViceVersa<T1, T2>()
        {

            Mapper.CreateMap<T1, T2>();
            Mapper.CreateMap<T2, T1>();
        }
    }
}
