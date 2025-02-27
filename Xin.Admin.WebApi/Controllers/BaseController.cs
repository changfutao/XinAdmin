using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Xin.Infrastructure.Attributes;
using Xin.Infrastructure.Dto;

namespace Xin.Admin.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ValidatePermission]
    public abstract class BaseController: ControllerBase
    {
       
    }
}
