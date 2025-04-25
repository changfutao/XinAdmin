using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Dto;
using Xin.Model;
using Xin.Service.User.Dto;

namespace Xin.Service.User
{
    public interface IUserService
    {
        /// <summary>
        /// 根据id获取用户
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task<UserEntity> GetAsync(long id);
        /// <summary>
        /// 根据用户名获取用户
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        Task<UserEntity> GetUserByNameAsync(string name);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> GetPageAsync(PageInput<UserInput> input);
        /// <summary>
        /// 用户新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> AddAsync(UserAddInput input);
        /// <summary>
        /// 用户编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> EditAsync(UserEditInput input);
        /// <summary>
        /// 用户删除
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns></returns>
        Task<IResultOutput> DeleteAsync(long[] ids);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> ChangePwdAsync(ChangePwdDto input);
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        Task<IResultOutput> GetAllUser();
    }
}
