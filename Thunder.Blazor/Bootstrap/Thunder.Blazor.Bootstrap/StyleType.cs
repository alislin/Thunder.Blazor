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
        none,
        primary,
        secondary,
        success,
        danger,
        warning,
        info,
        light,
        dark,
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
}
