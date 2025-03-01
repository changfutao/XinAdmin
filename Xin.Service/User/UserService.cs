using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Cache;
using Xin.Infrastructure.Consts;
using Xin.Infrastructure.Dto;
using Xin.Infrastructure.Model;
using Xin.Model;
using Xin.Service.Menu.Dto;
using Xin.Service.User.Dto;

namespace Xin.Service.User
{
    public class UserService : IUserService
    {
        private readonly IFreeSql<SqlServerFlag> _fsql;
        private readonly ICache _cache;

        public UserService(IFreeSql<SqlServerFlag> fsql, ICache cache)
        {
            _fsql = fsql;
            _cache = cache;
        }

        /// <summary>
        /// 根据id获取用户
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public Task<UserEntity> GetAsync(long id)
        {
            return _fsql.Select<UserEntity>()
                  .Where(a => a.Id == id)
                  .FirstAsync();
        }

        /// <summary>
        /// 根据用户名获取用户
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        public Task<UserEntity> GetUserByNameAsync(string name)
        {
            return _fsql.Select<UserEntity>()
                        .Where(a => a.UserName == name)
                        .FirstAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> GetPageAsync(PageInput<UserInput> input)
        {
            var users = await _fsql.Select<UserEntity>()
                  .WhereIf(!string.IsNullOrEmpty(input.Filter.UserName), a => a.UserName.Contains(input.Filter.UserName))
                  .WhereIf(input.Filter.Status.HasValue, a => a.Status == input.Filter.Status.Value)
                  .Count(out long total)
                  .Skip((input.CurrentPage - 1) * input.PageSize)
                  .Take(input.PageSize)
                  .ToListAsync();
            var list = users.Adapt<List<UserDto>>();
            return ResultOutput.Ok(new PageOutput<UserDto>() { Total = total, List = list });
        }

        /// <summary>
        /// 用户新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> AddAsync(UserAddInput input)
        {
            var user = input.Adapt<UserEntity>();
            await _fsql.Insert(user).ExecuteAffrowsAsync();
            return ResultOutput.Ok();
        }

        /// <summary>
        /// 用户删除
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns></returns>
        public async Task<IResultOutput> DeleteAsync(long[] ids)
        {
            await _fsql.Update<UserEntity>()
                        .Set(a => a.IsDeleted, true)
                        .Where(a => ids.Contains(a.Id))
                        .ExecuteAffrowsAsync();
            return ResultOutput.Ok();
        }
        /// <summary>
        /// 用户修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> EditAsync(UserEditInput input)
        {
            var user = await _fsql.Select<UserEntity>()
                                            .Where(a => a.Id == input.Id)
                                            .FirstAsync();
            if (user == null)
            {
                return ResultOutput.NotOk("用户不存在");
            }
            await _fsql.Update<UserEntity>()
                .Set(a => a.Status, input.Status)
                .Set(a => a.Sex, input.Sex)
                .Set(a => a.NickName, input.NickName)
                .Set(a => a.Remark, input.Remark)
                .Set(a => a.Phonenumber, input.PhoneNumber)
                .ExecuteAffrowsAsync();
            return ResultOutput.Ok();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> ChangePwdAsync(ChangePwdDto input)
        {
            var user = await _fsql.Select<UserEntity>()
                  .Where(a => a.Id == input.Id)
                  .FirstAsync();
            if (user == null)
            {
                return ResultOutput.NotOk("用户不存在");
            }
            if (!user.Password.Equals(input.OriginPwd))
            {
                return ResultOutput.NotOk("原密码错误");
            }
            await _fsql.Update<UserEntity>()
                .Set(a => a.Password, input.NewPwd)
                .Where(a => a.Id == input.Id)
                .ExecuteAffrowsAsync();
            return ResultOutput.Ok();
        }
    }
}
