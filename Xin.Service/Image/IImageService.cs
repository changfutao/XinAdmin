using Xin.Infrastructure.Dto;
using Xin.Service.Image.Dto;

namespace Xin.Service.Image;

public interface IImageService
{
    Task<IResultOutput> GetPageAsync(PageInput<ImagePageInput> input);
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<IResultOutput> AddAsync(ImageAddInput input);
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<IResultOutput> EditAsync(ImageEditInput input);
    /// <summary>
    /// 软删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<IResultOutput> SoftDeleteAsync(long[] ids);
}