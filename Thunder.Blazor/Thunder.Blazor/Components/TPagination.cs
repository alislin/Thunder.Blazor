/* Ceated by Ya Lin. 2019/7/31 16:26:44 */

using System;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 翻页组件基础类
    /// </summary>
    public class TPagination<TModel>:TComponent<TModel> where TModel:TPaginationItem,new()
    {
        protected void GotoPage(int index)
        {
            DataContext.Goto?.Invoke(index);
        }
    }

    public class TPagination : TPagination<TPaginationItem>
    {
    }

    public class TPaginationItem : TContext
    {
        /// <summary>
        /// 翻页Action
        /// </summary>
        public Action<int> Goto { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 总计页码
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// 最大显示页码
        /// </summary>
        public int ShowMax { get; set; } = 10;
        /// <summary>
        /// 总是显示前后页
        /// </summary>
        public bool AlwaysShowNextPrev { get; set; } = true;

        /// <summary>
        /// 最小页码
        /// </summary>
        public int MinPage => getMin();
        /// <summary>
        /// 最大页码
        /// </summary>
        public int MaxPage => getMax();
        public bool ShowLeft => Index > 0 && IsEnabled;
        public bool ShowRight => Index < TotalPage - 1 && IsEnabled;

        //public override Type ContextType => typeof(TPagination);

        private int getMin()
        {
            var min = 0;
            min = Index - (ShowMax / 2);
            if (min + ShowMax > TotalPage - 1)
            {
                min = TotalPage - ShowMax - 1;
            }
            min = min < 0 ? 0 : min;
            return min;
        }

        private int getMax()
        {
            var max = MinPage + ShowMax;
            max = max > TotalPage - 1 ? TotalPage - 1 : max;
            return max;
        }
    }

}
