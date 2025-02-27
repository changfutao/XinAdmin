using System.ComponentModel.DataAnnotations;

namespace Xin.Service.Image.Dto;

public class ImageEditInput: ImageAddInput
{
    [Required(ErrorMessage = "Id必填")]
    public long Id { get; set; }
}