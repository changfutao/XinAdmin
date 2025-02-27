
using System.ComponentModel;
using FreeSql.DataAnnotations;
using Xin.Infrastructure.Entities;

namespace Xin.Model.ImageWareHouse;
/// <summary>
/// 图片分类
/// </summary>
[Table(Name = "bus_image_category")]
[Description("图片分类")]
public class ImageCategoryEntity: EntityFull
{
    /// <summary>
    /// 分类名称
    /// </summary>
    [Column(Position = 2, IsNullable = false, StringLength = 50)]
    [Description("分类名称")]
    public string Name { get; set; }
    /// <summary>
    /// 分类排序
    /// </summary>
    [Column(Position = 3)]
    [Description("分类排序")]
    public int Sort { get; set; }
}