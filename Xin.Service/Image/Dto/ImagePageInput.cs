using System.ComponentModel.DataAnnotations;

namespace Xin.Service.Image.Dto;

public class ImagePageInput
{
    [Required(ErrorMessage ="图片分类Id不为空")]
    /// <summary>
    /// 图片分类Id
    /// </summary>
    public long ImageCategoryId { get; set; }
    /// <summary>
    /// 图片名称
    /// </summary>
    public string? Name { get; set; }
}