using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xin.Infrastructure.Dto;
using Xin.Service.ImageCategory;
using Xin.Service.ImageCategory.Dto;

namespace Xin.Admin.WebApi.Controllers;

public class ImageCategoryController: BaseController
{
    private readonly IImageCategoryService _imageCategoryService;

    public ImageCategoryController(IImageCategoryService imageCategoryService)
    {
        _imageCategoryService = imageCategoryService;
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IResultOutput> GetPage([FromBody]PageInput input)
    {
        return await _imageCategoryService.GetPageAsync(input);
    }
    
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResultOutput> Add(ImageCategoryAddInput input)
    {
        return await _imageCategoryService.AddAsync(input);
    }
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResultOutput> Edit(ImageCategoryEditInput input)
    {
        return await _imageCategoryService.EditAsync(input);
    }
    /// <summary>
    /// 软删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResultOutput> SoftDelete(long[] ids)
    {
        return await _imageCategoryService.SoftDeleteAsync(ids);
    }
}