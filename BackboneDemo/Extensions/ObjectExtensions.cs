using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Reflection;


namespace System
{
    public static class ObjectExtentions
    {
        /// <summary>
        /// Try to convert the current object into a json object
        /// </summary>
        /// <param name="foo">this</param>
        /// <remarks>If the object cannot be converted then an exception will occur</remarks>
        /// <returns>string that is json</returns>
        public static string ToJson(this object foo)
        {
            try
            {
                var s = new JavaScriptSerializer();
                return s.Serialize(foo);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }








        /// <summary>
        /// All methods for this object
        /// </summary>
        /// <param name="foo">this</param>
        /// <returns>List<MethodInfo>()</returns>
        public static List<MethodInfo> Methods(this object foo)
        {
            return foo.GetType().Methods();
        }

        /// <summary>
        /// All properties for this object
        /// </summary>
        /// <param name="foo">this</param>
        /// <returns>List<PropertyInfo>()</returns>
        public static List<PropertyInfo> Properties(this object foo)
        {
            return foo.GetType().Properties();
        }



        /// <summary>
        /// Convert an object into a decimal
        /// </summary>
        /// <param name="foo">this</param>
        /// <returns>decimal</returns>
        public static decimal ToDecimal(this object foo)
        {
            if (foo.IsNull())
                return 0;
            else
                return Convert.ToDecimal(foo);
        }

        /// <summary>
        /// Convert an object into a DateTime
        /// </summary>
        /// <param name="foo">this</param>
        /// <returns>DateTime Object</returns>
        public static DateTime ToDateTime(this object foo)
        {
            if (foo.IsNull())
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(foo);
        }


        /// <summary>
        /// Convert an object to a boolean
        /// </summary>
        /// <param name="foo"></param>
        /// <returns></returns>
        public static bool ToBool(this object foo)
        {
            if (foo.IsNull())
                return false;
            else
            {
                var tmp = new[] { "on" }.Contains(foo) ? true : foo;
                return Convert.ToBoolean(tmp);
            }
        }

        /// <summary>
        /// Check to determine if the object is null
        /// </summary>
        /// <param name="foo"></param>
        /// <returns></returns>
        public static bool IsNull(this object foo)
        {
            return foo == null || foo == DBNull.Value;
        }

        /// <summary>
        /// Check to determine if the object is NOT null
        /// </summary>
        /// <param name="foo"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object foo)
        {
            return !foo.IsNull();
        }


    }
}
