using System;
using System.Collections.Generic;

namespace ShareMarket.Utility.Utilities
{
    public static class StringUtil
    {
        /// <summary>
        /// Convert to string List to comma separated values joint separator
        /// </summary>
        /// <param name="stringList">List of string  </param>
        /// <param name="jointSeparator">which joint List of String</param>
        /// <returns></returns> 
        public static string ToCommaSeparated(this List<string> stringList, string jointSeparator)
        {
            try
            {
                return ToCommaSeparated(stringList.ToArray(), jointSeparator);
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(StringUtil));
            }
            return string.Empty;
        }

        /// <summary>
        /// Convert to string List to comma separated values
        /// </summary>
        /// <param name="stringList">array of string  </param>
        /// <param name="jointSeparator">which joint array of String</param>
        /// <returns></returns>
        public static string ToCommaSeparated(this string[] stringList, string jointSeparator)
        {
            try
            {
                if (stringList != null)
                    return string.Join(jointSeparator, stringList);
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(StringUtil));
            }
            return null;
        }

        /// <summary>
        /// Convert comma separated string to string array
        /// </summary>
        /// <param name="commaSeparatedString">Separator with string</param>
        /// <param name="character">character to be separator</param>
        /// <returns></returns>
        public static string[] ToSplit(this string commaSeparatedString, char character)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(commaSeparatedString))
                    return commaSeparatedString.Split(character);
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(StringUtil));
            }
            return null;
        }

        /// <summary>
        /// Convert int to String
        /// </summary>
        /// <param name="value">Pass string value</param>
        /// <returns></returns>
        public static string ToStringFromInt(this int value)
        {
            try
            {
                return Convert.ToString(value);
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(StringUtil));
            }
            return null;
        }

        /// <summary>
        /// Convert String to int
        /// </summary>
        /// <param name="value">Pass string value</param>
        /// <returns></returns>
        public static int ToIntFromString(this string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(StringUtil));
            }
            return 0;
        }

        /// <summary>
        /// Convert Null to String
        /// </summary>
        /// <param name="value">Pass object value</param>
        /// <returns></returns>
        public static string ToStringFromObject(this object value)
        {
            try
            {
                return Convert.ToString(value);
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(StringUtil));
            }
            return string.Empty;
        }
    }
}
