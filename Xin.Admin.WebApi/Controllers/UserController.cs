using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xin.Infrastructure;
using Xin.Infrastructure.Dto;
using Xin.Infrastructure.Model;
using Xin.Model;
using Xin.Service.User;
using Xin.Service.User.Dto;

namespace Xin.Admin.WebApi.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [ApiExplorerSettings(GroupName = "用户管理")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// 通过Id获取用户
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IResultOutput> Get(long id)
        {
            var user = await _userService.GetAsync(id);
            if (user == null)
            {
                return ResultOutput.NotOk("用户不存在");
            }

            var userDto = user.Adapt<UserDto>();
            return ResultOutput.Ok(userDto);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResultOutput> GetPage(PageInput<UserInput> input)
        {
            return await _userService.GetPageAsync(input);
        }

        /// <summary>
        /// 用户增加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResultOutput> Add(UserAddInput input)
        {
            return await _userService.AddAsync(input);
        }

        /// <summary>
        /// 用户删除
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResultOutput> Delete(long[] ids)
        {
            return await _userService.DeleteAsync(ids);
        }
        /// <summary>
        /// 用户修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResultOutput> Edit(UserEditInput input)
        {
            return await _userService.EditAsync(input);
        }

    }
}
