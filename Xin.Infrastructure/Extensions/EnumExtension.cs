using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Dto;

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
        
        /// <summary>
        /// 枚举的选项输出
        /// </summary>
        /// <param name="type">枚举Type</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<OptionOutput> ToOptionViewModels(this Type type)
        {
            if (type.BaseType != typeof(Enum)) throw new Exception("type must be Enum");

            return Enum.GetValues(type).Cast<Enum>().Select(a => new OptionOutput
            {
                Label = a.ToDescription(),
                Value = a
            });
        }
    }
}
