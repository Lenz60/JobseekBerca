using System;
using System.Linq;
using System.Reflection;

namespace JobseekBerca.Helper
{
    public class Whitespace
    {
        public static bool HasNullOrEmptyStringProperties<T>(T model, out string propertyName)
        {
            propertyName = null;
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.PropertyType == typeof(string))
                {
                    var value = (string)property.GetValue(model);
                    if (string.IsNullOrEmpty(value))
                    {
                        propertyName = property.Name;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
