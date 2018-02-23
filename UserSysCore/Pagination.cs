using System;
using System.Collections.Generic;
using System.Text;

namespace UserSysCore
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class Pagination
    {
        public Pagination()
        {
            PageIndex = 0;
            PageSize = 20;
        }
        /// <summary>
        /// 当前页，索引从0开始。
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNum { get { return PageIndex + 1; } }
        int _pageSize = 0;
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                if (_pageSize <= 0)
                {
                    _pageSize = 20;
                }
            }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                int num = RecordCount / PageSize;
                if (RecordCount % PageSize > 0)
                {
                    num++;
                }
                return num;
            }
        }
        /// <summary>
        /// 总数据量
        /// </summary>
        public int RecordCount { get; set; }
        public string OrderBy { get; set; }
        public string OrderByDescending { get; set; }
    }
}
