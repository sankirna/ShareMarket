using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using MobileSurvey.Utils.Utilities;

namespace ShareMarket.Utility.Utilities
{
    public static class EnumUtil
    {
        /// <summary>
        /// Get IEnumerable List of string
        /// </summary>
        /// <typeparam name="T"> T is Enum</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetValues<T>()
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Get Enum Description Attribute
        /// </summary>
        /// <param name="en">Enum Type</param>
        /// <returns></returns>
        public static string GetEnumDescription(System.Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return en.ToString();
        }

        /// <summary>
        /// Parse string to enum by specifying the enum type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ParseEnum<T>(string value)
        {
            try
            {
                return (T)System.Enum.Parse(typeof(T), value, true);
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(GlobalUtil));
                return (T)System.Enum.Parse(typeof(T), value, true);
            }
        }

        /// <summary>
        /// Return the list of string from the enum tyep passed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<string> GetStringListByEnumType<T>()
        {
            return System.Enum.GetNames(typeof(T)).ToList();
        }

    }
}
