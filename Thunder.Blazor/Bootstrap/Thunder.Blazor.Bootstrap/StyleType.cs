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
        list_group_item
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
}
