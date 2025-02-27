namespace Xin.Service.Order.Dto;
/// <summary>
/// 首页信息
/// </summary>
public class IndexInfoDto
{
    public List<TitleInfo> TitleInfos { get; set; }
}
/// <summary>
/// 标题信息
/// </summary>
public class TitleInfo
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 时间类型
    /// </summary>

    public string Type { get; set; }
    /// <summary>
    /// 数据
    /// </summary>
    public double Num { get; set; }
    /// <summary>
    /// 详情名称
    /// </summary>
    public string DetailName { get; set; }
    /// <summary>
    /// 详情数据
    /// </summary>
    public string DetailData { get; set; }
}

