/* Ceated by Ya Lin. 2019/8/7 15:14:06 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapDropdownBase : TBlockContextBase<StrapDropdownMenuItem>
    {
        /// <summary>
        /// 默认菜单操作，独立的下拉按钮
        /// </summary>
        [Parameter] public bool EnableDefaultMenu { get; set; }
        [Parameter] public StyleType Style { get; set; }
        [Parameter] public bool Outline { get; set; }
        [Parameter] public SizeType Size { get; set; }
        public string DropMenuCss => GetDropMenuCss();
        public string DropCss => GetDropCss();
        protected bool EnableDefaultMenuFlag => EnableDefaultMenu && (DataContext?.ChildNodes?.Count ?? 0) > 0;

        private string GetDropMenuCss()
        {
            return CssBuild.New.Add("dropdown-menu")
                .Add("show",IsOpen)
                .Build()
                .CssString;
        }

        private string GetDropCss()
        {
            return CssBuild.New.Add("dropdown")
                .Add("show",IsOpen)
                .Build()
                .CssString;
        }

        protected override void StyleBuild(CssBuild cssBuilder)
        {
            //cssBuilder.Add("dropdown");
        }

        public override void UpdateDataContext()
        {
            DataContext.EnableDefaultMenu = EnableDefaultMenu;
            base.UpdateDataContext();
        }

        public override void LoadDataContext()
        {
            EnableDefaultMenu = DataContext.EnableDefaultMenu;
            base.LoadDataContext();
        }
    }

    public class StrapDropdownMenuItem : TNode<StrapDropdownMenuItem>
    {
        /// <summary>
        /// 默认菜单操作，独立的下拉按钮
        /// </summary>
        public bool EnableDefaultMenu { get; set; }
        public StyleType Style { get; set; }
        public bool Outline { get; set; }
        public SizeType Size { get; set; }
    }
}
