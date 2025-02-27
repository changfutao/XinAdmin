using Xin.Infrastructure.Dto;
using Xin.Service.ImageCategory.Dto;

namespace Xin.Service.ImageCategory;

public interface IImageCategoryService
{
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<IResultOutput> GetPageAsync(PageInput input);
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<IResultOutput> AddAsync(ImageCategoryAddInput input);
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<IResultOutput> EditAsync(ImageCategoryEditInput input);
    /// <summary>
    /// 软删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<IResultOutput> SoftDeleteAsync(long[] ids);
}