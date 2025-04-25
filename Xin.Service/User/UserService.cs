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
using Xin.Model.ImageWareHouse;
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
            var users = await _fsql.Select<UserEntity, ImageEntity>()
                .WhereIf(input.Filter != null && !string.IsNullOrEmpty(input.Filter.UserName),
                    (a, b) => a.UserName.Contains(input.Filter.UserName))
                .WhereIf(input.Filter != null && input.Filter.Status.HasValue,
                    (a, b) => a.Status == input.Filter.Status.Value)
                .LeftJoin((a, b) => a.AvatorId == b.Id)
                .Count(out long total)
                .Skip((input.CurrentPage - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToListAsync((a, b) => new UserDto
                {
                    Id = a.Id,
                    UserName = a.UserName,
                    NickName = a.NickName,
                    Phonenumber = a.Phonenumber,
                    Sex = a.Sex,
                    Status = a.Status,
                    AvatorId = a.AvatorId,
                    AvatorPath = b.Path
                });
            return ResultOutput.Ok(new PageOutput<UserDto>() { Total = total, List = users });
        }

        /// <summary>
        /// 用户新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> AddAsync(UserAddInput input)
        {
            // 判断用户名唯一
            if (await _fsql.Select<UserEntity>().AnyAsync(a => a.UserName == input.UserName))
            {
                return ResultOutput.NotOk("用户名已存在!");
            }

            var user = input.Adapt<UserEntity>();
            user.Password = "666666";
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
            if (await _fsql.Select<UserEntity>().AnyAsync(a => a.Id != input.Id && a.UserName == input.UserName))
            {
                return ResultOutput.NotOk("用户已存在");
            }

            await _fsql.Update<UserEntity>()
                .Set(a => a.Status, input.Status)
                .Set(a => a.Sex, input.Sex)
                .Set(a => a.NickName, input.NickName)
                .Set(a => a.Remark, input.Remark)
                .Set(a => a.Phonenumber, input.Phonenumber)
                .Set(a => a.AvatorId, input.AvatorId)
                .Where(a => a.Id == input.Id)
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
        
        /// <summary>
        /// 所有用户下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<IResultOutput> GetAllUser()
        {
            var options = await _fsql.Select<UserEntity>()
                                                .Where(a => a.IsDeleted == false)
                                                .ToListAsync(a => new OptionOutput()
                                                {
                                                    Label = a.UserName,
                                                    Value = a.Id
                                                });
            return ResultOutput.Ok(options);
        }
    }
}