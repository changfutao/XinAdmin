using System.ComponentModel.DataAnnotations;

namespace Xin.Service.ImageCategory.Dto;

public class ImageCategoryAddInput
{
    [Required(ErrorMessage = "图片分类名称必填")]
    [MaxLength(ErrorMessage = "图片分类最大长度为50个字符")]
    public string Name { get; set; }
    [Required(ErrorMessage = "图片分类排序必填")]
    public int Sort { get; set; }
}