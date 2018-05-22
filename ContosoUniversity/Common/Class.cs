using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Common
{
    /// <summary>
    /// 分页按钮控件
    /// </summary>
    public static class HtmlHelperPagerExtensions
    {
        /// <summary>
        /// 呈现普通分页按钮
        /// </summary>
        /// <param name="paginationMode">分页按钮显示模式</param>
        /// <param name="html">被扩展的HtmlHelper</param>
        /// <param name="pagingDataSet">数据集</param>
        /// <param name="numericPagingButtonCount">数字分页按钮显示个数</param>
        /// <returns>分页按钮html代码</returns>
        public static MvcHtmlString PagingButton<T>(this HtmlHelper html,IPagedList<T> pagingDataSet, PaginationMode paginationMode = PaginationMode.NumericNextPrevious, int numericPagingButtonCount = 7)
        {
            return PagingButton(html, pagingDataSet, false, null, paginationMode, numericPagingButtonCount);
        }

        /// <summary>
        /// 呈现异步分页按钮
        /// </summary>
        /// <param name="paginationMode">分页按钮显示模式</param>
        /// <param name="html">被扩展的HtmlHelper</param>
        /// <param name="pagingDataSet">数据集</param>
        /// <param name="updateTargetId">异步分页时，被更新的目标元素Id</param>
        /// <param name="numericPagingButtonCount">数字分页按钮显示个数</param>
        /// <returns>分页按钮html代码</returns>
        public static MvcHtmlString AjaxPagingButton<T>(this HtmlHelper html, HaiTunSP.Core.IPagedList<T> pagingDataSet, string updateTargetId, PaginationMode paginationMode = PaginationMode.NumericNextPrevious, int numericPagingButtonCount = 7, string ajaxUrl = null)
        {
            return PagingButton(html, pagingDataSet, true, updateTargetId, paginationMode, numericPagingButtonCount, ajaxUrl);
        }

        /// <summary>
        /// 呈现分页按钮
        /// </summary>
        /// <param name="html">被扩展的HtmlHelper</param>
        /// <param name="pagingDataSet">数据集</param>
        /// <param name="updateTargetId">异步分页时，被更新的目标元素Id</param>
        /// <param name="paginationMode">分页按钮显示模式</param>
        /// <param name="numericPagingButtonCount">数字分页按钮显示个数</param>
        /// <param name="enableAjax">是否使用ajax分页</param>
        /// <returns>分页按钮html代码</returns>
        private static MvcHtmlString PagingButton<T>(this HtmlHelper html, HaiTunSP.Core.IPagedList<T> pagingDataSet, bool enableAjax, string updateTargetId, PaginationMode paginationMode = PaginationMode.NumericNextPrevious, int numericPagingButtonCount = 7, string ajaxUrl = null)
        {
            if (pagingDataSet.TotalCount == 0 || pagingDataSet.PageSize == 0)
                return MvcHtmlString.Empty;

            //计算总页数
            int totalPages = (int)(pagingDataSet.TotalCount / (long)pagingDataSet.PageSize);
            if ((pagingDataSet.TotalCount % pagingDataSet.PageSize) > 0)
                totalPages++;


            //未超过一页时不显示分页按钮
            if (totalPages <= 1)
                return MvcHtmlString.Empty;

            bool showFirst = paginationMode == PaginationMode.NextPreviousFirstLast;

            bool showLast = paginationMode == PaginationMode.NextPreviousFirstLast;

            bool showPrevious = true;
            //if (paginationMode == PaginationMode.NextPrevious || paginationMode == PaginationMode.NextPreviousFirstLast || paginationMode == PaginationMode.NumericNextPrevious)
            //    showPrevious = true;

            bool showNext = true;
            //if (paginationMode == PaginationMode.NextPrevious || paginationMode == PaginationMode.NextPreviousFirstLast || paginationMode == PaginationMode.NumericNextPrevious)
            //    showNext = true;

            bool showNumeric = paginationMode == PaginationMode.NumericNextPrevious;

            //显示多少个数字分页按钮
            //int numericPageButtonCount = 10;

            //对pageIndex进行修正
            if ((pagingDataSet.PageIndex < 1) || (pagingDataSet.PageIndex > totalPages))
                pagingDataSet.PageIndex = 1;

            string pagingContainer = "";
            StringBuilder pagingButtonsHtml = new StringBuilder(pagingContainer);

            //构建 "首页"
            if (showFirst)
            {
                if ((pagingDataSet.PageIndex > 1) && (totalPages > numericPagingButtonCount))
                {
                    pagingButtonsHtml.AppendLine();
                    pagingButtonsHtml.AppendFormat(BuildLink("&lt;&lt;", GetPagingNavigateUrl(html, 1, ajaxUrl), "paginate_button first"));
                }
                else if (paginationMode == PaginationMode.NextPreviousFirstLast)
                {
                    pagingButtonsHtml.AppendLine();
                    pagingButtonsHtml.AppendFormat("<a class=\"paginate_button first\">{0}</a>", "&lt;&lt;");
                }
            }


            //构建 "上一页"
            if (showPrevious)
            {
                pagingButtonsHtml.AppendLine();
                if (pagingDataSet.PageIndex == 1)
                    pagingButtonsHtml.AppendFormat("<a class=\"paginate_button previous\">{0}</a>", "上一页");
                else
                    pagingButtonsHtml.AppendFormat(BuildLink("上一页", GetPagingNavigateUrl(html, pagingDataSet.PageIndex - 1, ajaxUrl), "paginate_button previous"));
            }

            //构建 数字分页部分
            if (showNumeric)
            {
                int startNumericPageIndex;
                if (numericPagingButtonCount > totalPages || pagingDataSet.PageIndex - (numericPagingButtonCount / 2) <= 0)
                    startNumericPageIndex = 1;
                else if (pagingDataSet.PageIndex + (numericPagingButtonCount / 2) > totalPages)
                    startNumericPageIndex = totalPages - numericPagingButtonCount + 1;
                else
                    startNumericPageIndex = pagingDataSet.PageIndex - (numericPagingButtonCount / 2);

                if (startNumericPageIndex < 1)
                    startNumericPageIndex = 1;

                int lastNumericPageIndex = startNumericPageIndex + numericPagingButtonCount - 1;
                if (lastNumericPageIndex > totalPages)
                    lastNumericPageIndex = totalPages;

                pagingButtonsHtml.AppendLine();
                pagingButtonsHtml.Append("<span>");
                if (startNumericPageIndex > 1)
                {
                    for (int i = 1; i < startNumericPageIndex; i++)
                    {
                        pagingButtonsHtml.AppendLine();

                        if (i > 3)
                            break;
                        if (i == 3)
                            pagingButtonsHtml.Append("<a class=\"paginate_button paginate_button_disabled\">...</a>");
                        else
                        {
                            if (pagingDataSet.PageIndex == i)
                            {
                                pagingButtonsHtml.AppendFormat("<a class=\"paginate_button current\">{0}</a>", i);
                            }
                            else
                            {
                                pagingButtonsHtml.AppendFormat(BuildLink(i.ToString(), GetPagingNavigateUrl(html, i, ajaxUrl)));
                            }
                        }
                    }
                }

                for (int i = startNumericPageIndex; i <= lastNumericPageIndex; i++)
                {
                    pagingButtonsHtml.AppendLine();
                    if (pagingDataSet.PageIndex == i)
                        pagingButtonsHtml.AppendFormat("<a class=\"paginate_button current\">{0}</a>", i);
                    else
                        pagingButtonsHtml.AppendFormat(BuildLink(i.ToString(), GetPagingNavigateUrl(html, i, ajaxUrl)));
                }

                if (lastNumericPageIndex < totalPages)
                {
                    int lastStart = lastNumericPageIndex + 1;
                    if (totalPages - lastStart > 2)
                        lastStart = totalPages - 2;

                    for (int i = lastStart; i <= totalPages; i++)
                    {
                        pagingButtonsHtml.AppendLine();
                        if ((i == lastStart) && (totalPages - lastNumericPageIndex > 3))
                        {
                            pagingButtonsHtml.AppendLine();
                            pagingButtonsHtml.Append("<a class=\"paginate_button page-break\">...</a>");
                            continue;
                        }

                        if (pagingDataSet.PageIndex == i)
                            pagingButtonsHtml.AppendFormat("<a class=\"paginate_button current\">{0}</a>", i);
                        else
                            pagingButtonsHtml.AppendFormat(BuildLink(i.ToString(), GetPagingNavigateUrl(html, i, ajaxUrl)));
                    }
                }

            }

            pagingButtonsHtml.Append("</span>");

            if (showNext)
            {
                pagingButtonsHtml.AppendLine();
                if (pagingDataSet.PageIndex == totalPages)
                    pagingButtonsHtml.AppendFormat("<a class=\"paginate_button next\">{0}</a>", "下一页");
                else
                    pagingButtonsHtml.AppendFormat(BuildLink("下一页", GetPagingNavigateUrl(html, pagingDataSet.PageIndex + 1, ajaxUrl), "paginate_button page-next"));
            }

            if (showLast)
            {
                if ((pagingDataSet.PageIndex < totalPages) && (totalPages > numericPagingButtonCount))
                {
                    pagingButtonsHtml.AppendLine();
                    pagingButtonsHtml.AppendFormat(BuildLink("&gt;&gt;", GetPagingNavigateUrl(html, totalPages, ajaxUrl), "paginate_button page-last"));
                }
                else if (paginationMode == PaginationMode.NextPreviousFirstLast)
                {
                    pagingButtonsHtml.AppendLine();
                    pagingButtonsHtml.AppendFormat("<a class=\"paginate_button last\">{0}</a>", "&gt;&gt;");
                }
            }
            //pagingButtonsHtml.Append("</div>");
            return MvcHtmlString.Create(pagingButtonsHtml.ToString());
        }

        /// <summary>
        /// 构建分页按钮的链接
        /// </summary>
        /// <param name="htmlHelper">被扩展的HtmlHelper</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns>分页按钮的url字符串</returns>
        public static string GetPagingNavigateUrl(this HtmlHelper htmlHelper, int pageIndex, string currentUrl = null)
        {
            object pageIndexObj = null;
            if (htmlHelper.ViewContext.RouteData.Values.TryGetValue("pageIndex", out pageIndexObj))
            {
                htmlHelper.ViewContext.RouteData.Values["pageIndex"] = pageIndex;
                return UrlHelper.GenerateUrl(null, null, null, htmlHelper.ViewContext.RouteData.Values, RouteTable.Routes, htmlHelper.ViewContext.RequestContext, true);
            }

            if (string.IsNullOrEmpty(currentUrl))
                currentUrl = HttpUtility.HtmlEncode(htmlHelper.ViewContext.HttpContext.Request.RawUrl);

            if (currentUrl.IndexOf("?") == -1)
            {
                return currentUrl + string.Format("?pageIndex={0}", pageIndex);
            }
            else
            {
                if (currentUrl.IndexOf("pageIndex=", StringComparison.InvariantCultureIgnoreCase) == -1)
                    return currentUrl + string.Format("&pageIndex={0}", pageIndex);
                else
                    return Regex.Replace(currentUrl, @"pageIndex=(\d+\.?\d*|\.\d+)", "pageIndex=" + pageIndex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
        }

        /// <summary>
        /// 生成带Href的链接
        /// </summary>
        private static string BuildLink(string linkText, string url, string cssClassName = "paginate_button")
        {
            return string.Format("<a href=\"{0}\" {1}>{2}</a>", url, string.IsNullOrEmpty(cssClassName) ? string.Empty : string.Format("class=\"{0}\"", cssClassName), linkText);
        }
    }

    /// <summary>
    /// 分页按钮显示模式
    /// </summary>
    public enum PaginationMode
    {
        /// <summary>
        /// 上一页/下一页 模式
        /// </summary>
        NextPrevious,

        /// <summary>
        /// 首页/末页/上一页/下一页 模式
        /// </summary>
        NextPreviousFirstLast,

        /// <summary>
        /// 上一页/下一页 + 数字 模式，例如： 上一页 1 2 3 4 5 下一页
        /// </summary>
        NumericNextPrevious,
    }
}
