using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Infrastructure.Extensions
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExtension
    {
        public static string ToDescription(this Enum @enum)
        {
            FieldInfo? field = @enum.GetType().GetField(@enum.ToString(), BindingFlags.Public | BindingFlags.Instance);
            if (field != null)
            {
                var desc = field.GetCustomAttribute<DescriptionAttribute>();
                if (desc != null)
                {
                    return desc.Description;
                }
            }
            return string.Empty;
        }
    }
}
