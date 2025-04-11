using Microsoft.AspNetCore.Mvc;
using Xin.Infrastructure.Dto;
using Xin.Model.ImageWareHouse;
using Xin.Service.Image;
using Xin.Service.Image.Dto;

namespace Xin.Admin.WebApi.Controllers;

public class ImageController: BaseController
{
    private readonly IImageService _imageService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ImageController(IImageService imageService, IWebHostEnvironment webHostEnvironment)
    {
        _imageService = imageService;
        _webHostEnvironment = webHostEnvironment;
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResultOutput> GetPage(PageInput<ImagePageInput> input)
    {
        return await _imageService.GetPageAsync(input);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResultOutput> Add(ImageAddInput input)
    {
        return await _imageService.AddAsync(input);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResultOutput> Edit(ImageEditInput input)
    {
        return await _imageService.EditAsync(input);
    }

    /// <summary>
    /// 软删除 
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResultOutput> SoftDelete(long[] ids)
    {
        return await _imageService.SoftDeleteAsync(ids);
    }
    /// <summary>
    /// 上传图片
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResultOutput> Upload(IFormFile? file, [FromForm]long imageCategoryId)
    {
        var currentDate = DateTime.Now;
        // wwwroot的根目录地址
        string webRootPath = _webHostEnvironment.WebRootPath;
        string filePath = $@"/Upload/{currentDate:yyyy-MM-dd}/";
        string dirPath = webRootPath + filePath;
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        if (file != null)
        {
            // 文件扩展名
            string fileExtension = Path.GetExtension(file.FileName);
            // 文件大小
            var fileSize = file.Length;
            if (fileSize > 1024 * 1024 * 2)
            {
                return ResultOutput.NotOk("上传文件不能大于2M");
            }
            // 保存文件名称
            var saveName = file.FileName.Substring(0, file.FileName.LastIndexOf(".")) + "_" +
                           currentDate.ToString("HHmmss") + fileExtension;
            string newFilePath = dirPath + saveName;
            // 文件保存
            using (var fs = System.IO.File.Create(newFilePath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            var result = await _imageService.AddAsync(new ImageAddInput
            {
                ImageCategoryId = imageCategoryId,
                Name = file.FileName,
                Path = filePath + saveName,
                Sort = 100
            });
            if (!result.Success)
            {
                return result;
            }

            var resultData = result as ResultOutput<long>;
            return ResultOutput.Ok(new { imgId = resultData.Data, imgPath = filePath + saveName });
        }
        return ResultOutput.NotOk("没有上传文件");
    }
}