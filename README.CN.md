# Thunder.Blazor 
[![Build Status](https://dev.azure.com/aideePub/Thunder.Blazor/_apis/build/status/alislin.Thunder.Blazor?branchName=master)](https://dev.azure.com/aideePub/Thunder.Blazor/_build/latest?definitionId=4&branchName=master)

| 组件                     | 版本                                                                                 |
| - | - |
| Thunder.Blazor           | ![Nuget (with prereleases)](https://img.shields.io/nuget/v/thunder.blazor)           |
| Thunder.Blazor.Animate   | ![Nuget (with prereleases)](https://img.shields.io/nuget/v/thunder.blazor.animate)   |
| Thunder.Blazor.Noty      | ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/thunder.blazor.noty)   |
| Thunder.Blazor.Bootstrap | ![Nuget (with prereleases)](https://img.shields.io/nuget/v/thunder.blazor.bootstrap) |

Thunder Blazor 框架为快速开发。包含的基础类，基本交互逻辑。将业务逻辑和界面交互分离开。作为一个基础框架使用。

## 安装
根据需要自行选择需要安装的组件。
```
Install-Package Thunder.Blazor
Install-Package Thunder.Blazor.Animate  
Install-Package Thunder.Blazor.Noty     
Install-Package Thunder.Blazor.Bootstrap
```

## 包含对象
### Thunder.Blazor
---
### Thunder.Blazor.Animate
在 `startup.cs` 文件中配置
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddAnimateScoped();
}
```
在需要调用的页面或者组件，将需要启用动画的组件设置 `id` 。  
> 页面继承 `TComponent` 后，可以使用 `NewId()` 生成随机 `id` 名称  
> `@inherits Thunder.Blazor.Components.TComponent` 
```
<h1 id="@titleId">Hello, world!</h1>
@code{
    [Inject] protected AnimateService animate { get; set; }

    protected string titleId { get; set; }

    protected override void OnInit()
    {
        // 设置 Id
        titleId = NewId();
        base.OnInit();
    }

    private void test(){
        animate.Start(new AnimateData { AnimateType = AnimateType.tada, id = titleId, resetOnEnd = true });
    }
}
```
---
### Thunder.Blazor.Noty
> 默认只包含了 `bootstrap-v4.min.css` 。如果需要其他主题，可自行在 `index.htm` 文件的 `head` 中添加对应的主题 css

在 `startup.cs` 文件中配置
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddNotyScoped();
}
```
在需要调用的页面或者组件
```
[Inject] protected NotifyService noty { get; set; }

private void test(){
    noty.Show(new NotyData("这是一条测试的消息。<br> This is a test pop!", NotyType.error));
}
```
---
### Thunder.Blazor.Bootstrap