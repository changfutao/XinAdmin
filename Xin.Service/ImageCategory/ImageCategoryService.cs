using Mapster;
using Microsoft.AspNetCore.Mvc;
using Xin.Infrastructure.Dto;
using Xin.Infrastructure.Model;
using Xin.Model.ImageWareHouse;
using Xin.Service.ImageCategory.Dto;

namespace Xin.Service.ImageCategory;

public class ImageCategoryService: IImageCategoryService
{
    private readonly IFreeSql<SqlServerFlag> _fsql;

    public ImageCategoryService(IFreeSql<SqlServerFlag> fsql)
    {
        _fsql = fsql;
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IResultOutput> GetPageAsync(PageInput input)
    {
        var imageCategories =await _fsql.Select<ImageCategoryEntity>()
            .Count(out long total)
            .OrderBy(a => a.Sort)
            .Page(input.CurrentPage, input.PageSize)
            .ToListAsync();
        var list  = imageCategories.Adapt<List<ImageCategoryPageDto>>();
        var pageOutput = new PageOutput<ImageCategoryPageDto>();
        pageOutput.Total = total;
        pageOutput.List = list;
        return ResultOutput.Ok(pageOutput);
    }

    /// <summary>
    /// 新增图片分类
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<IResultOutput> AddAsync(ImageCategoryAddInput input)
    {
        var isExist = await _fsql.Select<ImageCategoryEntity>()
            .AnyAsync(a => a.Name == input.Name);
        if (isExist)
        {
            return ResultOutput.NotOk("图片分类名称已存在!");
        }

        var imageCategory = input.Adapt<ImageCategoryEntity>();
        await _fsql.Insert(imageCategory).ExecuteAffrowsAsync();
        return ResultOutput.Ok();
    }
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<IResultOutput> EditAsync(ImageCategoryEditInput input)
    {
        var isExist = await _fsql.Select<ImageCategoryEntity>()
            .AnyAsync(a => a.Id != input.Id && a.Name == input.Name);
        if (isExist)
        {
            return ResultOutput.NotOk("图片分类名称已存在!");
        }
        var imageCategory = input.Adapt<ImageCategoryEntity>();
        await _fsql.Update<ImageCategoryEntity>()
            .Set(a => a.Name, input.Name)
            .Set(a => a.Sort, input.Sort)
            .Where(a => a.Id == input.Id)
            .ExecuteAffrowsAsync();
            
        return ResultOutput.Ok();
    }
    /// <summary>
    /// 软删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<IResultOutput> SoftDeleteAsync(long[] ids)
    {
        await _fsql.Update<ImageCategoryEntity>()
            .Set(a => a.IsDeleted, true)
            .Where(a => ids.Contains(a.Id))
            .ExecuteAffrowsAsync();
        return ResultOutput.Ok();
    }
}