# Thunder.Blazor
![](https://img.shields.io/badge/.NetCore%203.0-SDK%203.0.100--preview7--012821-sucess)

| Components               | Version                                                                              |
| ------------------------ | ------------------------------------------------------------------------------------ |
| Thunder.Blazor           | ![Nuget (with prereleases)](https://img.shields.io/nuget/v/thunder.blazor)           |
| Thunder.Blazor.Animate   | ![Nuget (with prereleases)](https://img.shields.io/nuget/v/thunder.blazor.animate)   |
| Thunder.Blazor.Noty      | ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/thunder.blazor.noty)   |
| Thunder.Blazor.Bootstrap | ![Nuget (with prereleases)](https://img.shields.io/nuget/v/thunder.blazor.bootstrap) |

The Thunder Blazor framework is developed rapidly. Contains the base class, the basic interaction logic. Separate business logic from interface interaction. Used as a basic framework.

## Installation
Select the components you need to install as needed.
```
Install-Package Thunder.Blazor
Install-Package Thunder.Blazor.Animate
Install-Package Thunder.Blazor.Noty
Install-Package Thunder.Blazor.Bootstrap
```

## Include objects
### Thunder.Blazor
---
### Thunder.Blazor.Animate
Configured in the `startup.cs` file
```
Public void ConfigureServices(IServiceCollection services)
{
    services.AddAnimateScoped();
}
```
On the page or component that needs to be called
```
<h1 id="@titleId">Hello, world!</h1>
@code{
    [Inject] protected AnimateService animate { get; set; }

    Protected string titleId { get; set; }

    Protected override void OnInit()
    {
        // set Id
        titleId = NewId();
        base.OnInit();
    }

    Private void test(){
        animate.Start(new AnimateData { AnimateType = AnimateType.tada, id = titleId, resetOnEnd = true });
    }
}
```
---
### Thunder.Blazor.Noty
> By default only `bootstrap-v4.min.css` is included. If you need other topics, you can add the corresponding theme css in `head` of the `index.htm` file.

Configured in the `startup.cs` file
```
Public void ConfigureServices(IServiceCollection services)
{
    services.AddNotyScoped();
}
```
On the page or component that needs to be called
```
[Inject] protected NotifyService noty { get; set; }

Private void test(){
    noty.Show(new NotyData("This is a test message.<br> This is a test pop!", NotyType.error));
}
```
---
### Thunder.Blazor.Bootstrap