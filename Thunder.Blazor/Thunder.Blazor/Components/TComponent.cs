﻿using Microsoft.AspNetCore.Components;
using System;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 子组件基类
    /// </summary>
    public class TComponent : ComponentBase, IDisposable
    {
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

    public abstract class TComponentObject<T> : TComponent where T : new()
    {
        [Parameter] protected T Value { get; set; } = new T();
    }

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
    public abstract class TContext
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

        public TContentCustion(Type type)
        {
            Type = type;
        }

        public Type Type { get; set; }
        public override Type ContextType => Type;
    }
}