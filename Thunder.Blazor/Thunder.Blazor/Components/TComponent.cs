using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 子组件基类
    /// </summary>
    public class TComponent : ComponentBase, IDisposable,IThunderObject
    {
        /// <summary>
        /// 对象名称
        /// </summary>
        [Parameter] public string ObjectName { get; set; }

        /// <summary>
        /// 级联参数（父组件传入参数）
        /// </summary>
        [CascadingParameter] protected ComponentParamenter Paramenters { get; set; }
        /// <summary>
        /// 级联参数（传入子组件）
        /// </summary>
        [CascadingParameter] protected ComponentParamenter ChildParamenters { get; set; }
        /// <summary>
        /// 子组件
        /// </summary>
        [Parameter] protected RenderFragment ChildContent { get; set; }     //todo:需要处理子组件队列 List<T>
        /// <summary>
        /// 组件名称
        /// </summary>
        [Parameter] protected string Name { get; set; }
        /// <summary>
        /// 点击回调
        /// </summary>
        [Parameter] protected EventCallback OnClick { get; set; }
        /// <summary>
        /// 关闭回调
        /// </summary>
        [Parameter] protected EventCallback OnClose { get; set; }
        /// <summary>
        /// 是否含有传入参数
        /// </summary>
        protected bool HasParamenters => Paramenters != null;

        public virtual void Dispose()
        {
        }

        /// <summary>
        /// 日志输入
        /// </summary>
        /// <param name="m"></param>
        protected void Log(string m)
        {
            Console.WriteLine(m);
        }
    }

    /// <summary>
    /// 组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TComponentObject<T> : TComponent where T : new()
    {
        [Parameter] protected T Value { get; set; } = new T();
    }

    /// <summary>
    /// 含上下文数据的组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TComponent<T>:TComponent where T : TContext,new()
    {
        [Parameter] protected T Value { get; set; } = new T();
        protected override void OnInit()
        {
            base.OnInit();
            if (HasParamenters)
            {
                try
                {
                    Value = Paramenters.Get<T>();
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// 设置子组件
        /// </summary>
        /// <param name="child">子组件数据</param>
        public void SetChild(TContext child)
        {
            Value.Child = child;
            ChildContent = Value.Child.ContextFragment;
            ChildParamenters = Value.Child.ContextParameters;
        }
    }


    /// <summary>
    /// 组件数据
    /// </summary>
    public abstract class TContext: IThunderObject
    {
        /// <summary>
        /// 组件参数(级联传入)
        /// </summary>
        public ComponentParamenter ContextParameters => GetParamenter();
        /// <summary>
        /// 组件类型
        /// </summary>
        public abstract Type ContextType { get; }
        /// <summary>
        /// 子组件数据
        /// </summary>
        public TContext Child { get; set; }

        /// <summary>
        /// 生成区块
        /// </summary>
        public RenderFragment ContextFragment => new RenderFragment(x => { x.OpenComponent(1, ContextType); x.CloseComponent(); });
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName => this.GetType().FullName;

        /// <summary>
        /// 对象名称
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// 自动生成参数
        /// </summary>
        /// <returns></returns>
        private ComponentParamenter GetParamenter()
        {
            var p = new ComponentParamenter(TypeName, this);
            return p;
        }
    }

    /// <summary>
    /// 定制组件数据（无参数）
    /// </summary>
    public class TContentCustion : TContext
    {
        public TContentCustion()
        {
        }

        /// <summary>
        /// 定制组件
        /// </summary>
        /// <param name="type">对象类型</param>
        public TContentCustion(Type type)
        {
            Type = type;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// 上下文类型
        /// </summary>
        public override Type ContextType => Type;
    }

    /// <summary>
    /// 容器组件
    /// </summary>
    public abstract class TContainer : TContext, IVisual, IBehaver<TContext>
    {
        public string Text { get; set; }

        public bool Visabled { get; set; }
        public bool Actived { get; set; }
        public Action<TContext> Load { get; set; }
        public Action<TContext> Show { get; set; }
        public Action<TContext> Close { get; set; }
        public EventHandler OnLoading{ get; set; }
        public EventHandler OnShowing{ get; set; }
        public EventHandler OnClosing{ get; set; }
        public EventHandler OnLoaded { get; set; }
        public EventHandler OnShowed { get; set; }
        public EventHandler OnClosed { get; set; }

        public string Backgroud { get; set; }
        public string FontColor { get; set; }
        public string Size { get; set; }

    }
}
