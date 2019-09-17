// <copyright file="Index.cs" author="linya">
// Create time：       2019/9/17 14:12:26
// </copyright>

using Microsoft.AspNetCore.Components;

namespace ServerDemo.Pages
{
    [Route("/")]
    public class IndexV : ClientDemo.Client.Pages.Index { }

    [Route("/tabs")]
    public class TabsV : ClientDemo.Client.Pages.Tabs { }

}
