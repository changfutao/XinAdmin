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
    /// <typeparam name="T"></typeparam>
    public class ResultOutput<T>: IResultOutput<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; } = true;
        /// <summary>
        /// 消息
        /// </summary>
        public string? Msg { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public T? Data { get; set; }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public ResultOutput<T> Ok(T data, string? msg = null)
        {
            Data = data;
            Msg = msg;
            return this;
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ResultOutput<T> NotOk(string? msg = null, T data = default(T))
        {
            Success = false;
            Msg = msg;
            Data = data;
            return this;
        }
    }

    public static class ResultOutput
    {
        /// <summary>
        /// 成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static IResultOutput Ok<T>(T data = default(T), string msg = null)
        {
            return new ResultOutput<T>().Ok(data, msg);
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <returns></returns>
        public static IResultOutput Ok()
        {
            return Ok<string>();
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static IResultOutput NotOk(string msg = null)
        {
            return new ResultOutput<string>().NotOk(msg);
        }
    }
}
