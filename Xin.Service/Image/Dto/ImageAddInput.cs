using System.ComponentModel.DataAnnotations;

namespace Xin.Service.Image.Dto;

public class ImageAddInput
{
    [Required(ErrorMessage = "图片名称必填")]
    public string Name { get; set; }
    [Required(ErrorMessage = "图片排序必填")]
    public int Sort { get; set; }
    /// <summary>
    /// 图片分类Id
    /// </summary>
    [Required(ErrorMessage ="图片分类Id")]
    public long ImageCategoryId { get; set; }
    /// <summary>
    /// 图片地址
    /// </summary>
    public string? Path { get; set; }
}