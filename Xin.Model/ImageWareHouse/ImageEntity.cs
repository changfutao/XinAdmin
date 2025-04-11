using System.ComponentModel;
using FreeSql.DataAnnotations;
using Xin.Infrastructure.Entities;

namespace Xin.Model.ImageWareHouse;
/// <summary>
/// 图片信息
/// </summary>
[Table(Name = "bus_image")]
[Description("图片信息")]
public class ImageEntity: EntityFull
{
    /// <summary>
    /// 图片名称
    /// </summary>
    [Column(Position = 2, IsNullable = false, StringLength = 50)]
    [Description("图片名称")]
    public string Name { get; set; }
    /// <summary>
    /// 图片排序
    /// </summary>
    [Column(Position = 3)]
    [Description("图片排序")]
    public int Sort { get; set; }
    /// <summary>
    /// 图片分类Id
    /// </summary>
    [Column(Position = 4)]
    [Description("图片分类Id")]
    public long ImageCategoryId { get; set; }
    /// <summary>
    /// 图片地址
    /// </summary>
    [Column(Position = 5, StringLength = 100)]
    [Description("图片地址")]
    public string? Path { get; set; }
}