using System.ComponentModel.DataAnnotations;

namespace Xin.Service.ImageCategory.Dto;

public class ImageCategoryEditInput: ImageCategoryAddInput
{
    [Required(ErrorMessage = "Id必填")]
    public long Id { get; set; }
}