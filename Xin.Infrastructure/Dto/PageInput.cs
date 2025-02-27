namespace Xin.Infrastructure.Dto
{
    /// <summary>
    /// 分页信息输入
    /// </summary>
    public class PageInput
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; } = 1;
        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// 排序项
        /// </summary>
        public string SortItem { get; set; } = "CreatedTime";
        /// <summary>
        /// 排序类型
        /// </summary>
        public string SortType { get; set; } = "asc";
    }
    /// <summary>
    /// 页面信息输入
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageInput<T>: PageInput
    {
        /// <summary>
        /// 筛选数据
        /// </summary>
        public T? Filter { get; set; }
    }
}
