using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Business.Extensions
{
    public static class EnumExtension
    {
        public static string ToDescriptionString(this Enum val)
        {
            var field = val.GetType().GetField(val.ToString());

            if (field == null)
                return string.Empty;

            DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}