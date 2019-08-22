/* Ceated by Ya Lin. 2019/7/31 17:28:38 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Thunder.Blazor.Bootstrap
{
    /// <summary>
    /// Style Tag
    /// </summary>
    public enum StyleType
    {
        primary,
        secondary,
        success,
        danger,
        warning,
        info,
        light,
        dark,
        link,
        none,
    }

    public enum ComponentType
    {
        [Description("alert")]
        alert,
        [Description("badge")]
        badge,
        [Description("btn")]
        button,
        [Description("bg")]
        background,
        [Description("list-group-item")]
        list_group_item,
        [Description("text")]
        text
    }

    public enum ActionItemTag
    {
        button,
        a,
        input
    }

    /// <summary>
    /// 外描线
    /// </summary>
    public enum OutlineType
    {
        outline
    }

    /// <summary>
    /// 尺寸
    /// </summary>
    public enum SizeType
    {
        [Description("")]
        normal,
        [Description("lg")]
        large,
        [Description("sm")]
        small
    }

    /// <summary>
    /// 垂直
    /// </summary>
    public enum VerticalType
    {
        vertical
    }

    public enum DropDirectionType
    {
        [Description("")]
        down,
        [Description("dropup")]
        up,
        [Description("dropright")]
        right,
        [Description("dropleft")]
        left
    }

    /// <summary>
    /// 对起模式
    /// </summary>
    public enum AlignmentType
    {
        [Description("")]
        none,
        [Description("left")]
        left,
        [Description("center")]
        center,
        [Description("right")]
        right,
        [Description("start")]
        start,
        [Description("end")]
        end,
    }

}
