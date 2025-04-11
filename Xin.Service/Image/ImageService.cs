using Mapster;
using Xin.Infrastructure.Dto;
using Xin.Infrastructure.Model;
using Xin.Model.ImageWareHouse;
using Xin.Service.Image.Dto;

namespace Xin.Service.Image;

public class ImageService: IImageService
{
    private readonly IFreeSql<SqlServerFlag> _fsql;

    public ImageService(IFreeSql<SqlServerFlag> fsql)
    {
        _fsql = fsql;
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<IResultOutput> GetPageAsync(PageInput<ImagePageInput> input)
    {
       var list = await _fsql.Select<ImageEntity>()
            .Where(a => a.ImageCategoryId == input.Filter.ImageCategoryId)
            .WhereIf(!string.IsNullOrEmpty(input.Filter.Name), a => a.Name.Contains(input.Filter.Name))
            .Count(out long total)
            .OrderBy(a => a.Sort)
            .Page(input.CurrentPage, input.PageSize)
            .ToListAsync();
       var imageDtos = list.Adapt<List<ImageDto>>();
       PageOutput<ImageDto> pageOutput = new PageOutput<ImageDto>();
       pageOutput.Total = total;
       pageOutput.List = imageDtos;
       return ResultOutput.Ok(pageOutput);
    }
    
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<IResultOutput> AddAsync(ImageAddInput input)
    {
        var imageEntity = input.Adapt<ImageEntity>();
        await _fsql.Insert(imageEntity).ExecuteAffrowsAsync();
        return ResultOutput.Ok(imageEntity.Id);
    }
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<IResultOutput> EditAsync(ImageEditInput input)
    {
        var isExist = await _fsql.Select<ImageEntity>().AnyAsync(a => a.Id != input.Id && a.Name == input.Name);
        if (isExist)
        {
            return ResultOutput.NotOk("图片名称已存在");
        }

        await _fsql.Update<ImageEntity>()
            .Set(a => a.Name, input.Name)
            .Set(a => a.Sort, input.Sort)
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
        await _fsql.Update<ImageEntity>()
            .Set(a => a.IsDeleted, true)
            .Where(a => ids.Contains(a.Id))
            .ExecuteAffrowsAsync();
        return ResultOutput.Ok();
    }
}