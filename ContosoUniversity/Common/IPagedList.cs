using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Common
{
    /// <summary>
    /// 分页列表接口
    /// </summary>
    public interface IPagedList<T> : IList<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        int PageIndex { get; set; }
        /// <summary>
        /// 每页显示数据量
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 总数据量
        /// </summary>
        int TotalCount { get; }
        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPages { get; }
        /// <summary>
        /// 有上一页
        /// </summary>
        bool HasPreviousPage { get; }
        /// <summary>
        /// 有下一页
        /// </summary>
        bool HasNextPage { get; }
    }
}
