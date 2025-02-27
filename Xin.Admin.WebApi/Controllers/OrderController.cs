using Microsoft.AspNetCore.Mvc;
using Xin.Infrastructure.Dto;
using Xin.Service.Order.Dto;

namespace Xin.Admin.WebApi.Controllers;

public class OrderController : BaseController
{
    /// <summary>
    /// 首页信息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IResultOutput GetIndexInfos()
    {
        IndexInfoDto dto = new IndexInfoDto();
        List<TitleInfo> titleInfos = new List<TitleInfo>()
        {
            new TitleInfo { Name = "支付订单", Type = "年", Num = 49, DetailName = "总支付订单", DetailData = "49"},
            new TitleInfo { Name = "订单量", Type = "周", Num = 49, DetailName = "转化率", DetailData = "60%"},
            new TitleInfo { Name = "销售额", Type = "年", Num = 4, DetailName = "总销售额", DetailData = "3.54"},
            new TitleInfo { Name = "支付订单", Type = "年", Num = 17, DetailName = "总用户", DetailData = "17人"},
        };
        dto.TitleInfos = titleInfos;
        return ResultOutput.Ok(dto);
    }
}