using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Infrastructure.Dto
{
    /// <summary>
    /// 返回消息体
    /// </summary>
    public interface IResultOutput
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        bool Success { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        string? Msg { get; set; }
    }
    /// <summary>
    /// 返回消息体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResultOutput<T>: IResultOutput
    {
        /// <summary>
        /// 数据
        /// </summary>
        T? Data { get; set; }
    }
}
