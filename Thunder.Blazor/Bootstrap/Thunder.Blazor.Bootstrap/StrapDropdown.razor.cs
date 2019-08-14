/* Ceated by Ya Lin. 2019/8/7 15:14:06 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Libs;
using Thunder.Blazor.Services;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapDropdownBase : TBlockComponent<StrapDropdownMenuItem>
    {
        [Parameter] public ActionItemTag ActionItemTag { get; set; }
        /// <summary>
        /// 默认菜单操作，独立的下拉按钮
        /// </summary>
        [Parameter] public bool EnableDefaultMenu { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        [Parameter] public StyleType Style { get; set; }
        /// <summary>
        /// 外框线
        /// </summary>
        [Parameter] public bool Outline { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        [Parameter] public SizeType Size { get; set; }
        /// <summary>
        /// 展开方向
        /// </summary>
        [Parameter] public DropDirectionType DropDirection { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        [Parameter] public bool SubItem { get; set; }
        /// <summary>
        /// 禁用按钮样式
        /// </summary>
        [Parameter] public bool DisableButtonStyle { get; set; }
        /// <summary>
        /// 显示角标
        /// </summary>
        [Parameter] public bool ShowBadge { get; set; }

        public string DropMenuCss => GetDropMenuCss();
        public string DropCss => GetDropCss();
        protected bool EnableDefaultMenuFlag => EnableDefaultMenu && (DataContext?.ChildNodes?.Count ?? 0) > 0 && !SubItem;

        private string GetDropMenuCss()
        {
            return CssBuild.New.Add("dropdown-menu")
                .Add("show",IsOpen)
                .Build()
                .CssString;
        }

        private string GetDropCss()
        {
            return CssBuild.New.Add("dropdown",!EnableDefaultMenuFlag)
                .Add("btn-group", EnableDefaultMenuFlag)
                .Add(DropDirection.ToDescriptionString())
                .Add("show",IsOpen)
                .Add(StyleClass)
                .Build()
                .CssString;
        }

        protected string GetToggleCss(bool spit=false)
        {
            return CssBuild.New.Add("dropdown-toggle")
                .Add("dropdown-toggle-split",spit)
                //.Add(CssStyle)
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

        protected override void OnInitialized()
        {
            base.OnInitialized();
            //OnShowing += (o, e) => { ComponentService.BlockContextCloseAction.Add(Close); };
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
