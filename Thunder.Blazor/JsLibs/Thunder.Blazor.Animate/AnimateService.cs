/* Ceated by Ya Lin. 2019/7/22 16:35:13 */

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Services;

namespace Thunder.Blazor.Services
{
    public partial class AnimateService
    {
        private Dictionary<string, (AnimateData data,Task task)> Data = new Dictionary<string, (AnimateData, Task)>();

        public AnimateService(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        public AnimateService()
        {
        }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        /// <summary>
        /// 执行动画
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">动画参数</param>
        /// <param name="callback">动画执行完后回调</param>
        /// <returns></returns>
        public async Task Start(AnimateData data, Action callback=null)
        {
            await Reset(data);
            Console.WriteLine($"开始动画。{data.AnimateType.ToString()}");

            var jscall = new JsAction
            {
                Action = () =>
                {
                    if (data.resetOnEnd)
                    {
                        Console.WriteLine($"回调清理动画。{data.AnimateType.ToString()}");
                        Reset(data);
                    }
                    callback?.Invoke();
                }
            };
            var cb = jscall.ToObjectRef();
            //if (callback == null)
            //{
            //    cb = null;
            //}
            var task= JsRuntime.InvokeAsync<object>("ThunderBlazor.Animate.Start", new object[] { data, cb });
            Data.Add(data.id, (data,task));
        }

        /// <summary>
        /// 重置动画
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task Reset(AnimateData data, bool forceRemove = false)
        {
            if (Data.ContainsKey(data.id))
            {
                var ani = Data[data.id];
                if (ani.task.Status != TaskStatus.RanToCompletion)
                {
                    Console.WriteLine($"等待动画结束。{ani.data.AnimateType.ToString()}");
                    ani.task.Wait();
                }
            }
            if (Data.ContainsKey(data.id)||forceRemove)
            {
                var ani = Data[data.id];
                await JsRuntime.InvokeAsync<object>("ThunderBlazor.Animate.Reset", ani.data);
                Data.Remove(data.id);
                Console.WriteLine($"完成动画清理。{ani.data.AnimateType.ToString()}");
            }
        }

    }

    /// <summary>
    /// 动画参数
    /// </summary>
    public class AnimateData
    {
        /// <summary>
        /// 目标Dom Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 动画参数 CSS
        /// </summary>
        public string[] animateClass => GetAnimate();
        /// <summary>
        /// 动画类型
        /// </summary>
        public AnimateType AnimateType { get; set; } = AnimateType.fadeIn;
        /// <summary>
        /// 持续时间
        /// </summary>
        public Delay AnimateDelay { get; set; } = Delay.delay1s;
        /// <summary>
        /// 动画速度
        /// </summary>
        public Speed AnimateSpeed { get; set; } = Speed.normal;
        /// <summary>
        /// 动画结束以后重置
        /// </summary>
        public bool resetOnEnd { get; set; } = false;
        /// <summary>
        /// 动画类型
        /// </summary>
        private string type => AnimateType.ToString();
        /// <summary>
        /// 持续时间
        /// </summary>
        private string delay => AnimateDelay.ToDescriptionString();
        /// <summary>
        /// 动画速度
        /// </summary>
        private string speed => AnimateSpeed.ToDescriptionString();
        private string[] GetAnimate()
        {
            var result = new List<string>();
            if (!string.IsNullOrWhiteSpace(type)) result.Add(type);
            if (!string.IsNullOrWhiteSpace(delay)) result.Add(delay);
            if (!string.IsNullOrWhiteSpace(speed)) result.Add(speed);
            if (result.Count > 0) result.Add("animated");
            return result.ToArray();
        }

        public string ToString()
        {
            return string.Join(" ", animateClass);
        }
    }

    public enum AnimateType
    {
        bounce,
        flash,
        pulse,
        rubberBand,
        shake,
        headShake,
        swing,
        tada,
        wobble,
        jello,
        bounceIn,
        bounceInDown,
        bounceInLeft,
        bounceInRight,
        bounceInUp,
        bounceOut,
        bounceOutDown,
        bounceOutLeft,
        bounceOutRight,
        bounceOutUp,
        fadeIn,
        fadeInDown,
        fadeInDownBig,
        fadeInLeft,
        fadeInLeftBig,
        fadeInRight,
        fadeInRightBig,
        fadeInUp,
        fadeInUpBig,
        fadeOut,
        fadeOutDown,
        fadeOutDownBig,
        fadeOutLeft,
        fadeOutLeftBig,
        fadeOutRight,
        fadeOutRightBig,
        fadeOutUp,
        fadeOutUpBig,
        flipInX,
        flipInY,
        flipOutX,
        flipOutY,
        lightSpeedIn,
        lightSpeedOut,
        rotateIn,
        rotateInDownLeft,
        rotateInDownRight,
        rotateInUpLeft,
        rotateInUpRight,
        rotateOut,
        rotateOutDownLeft,
        rotateOutDownRight,
        rotateOutUpLeft,
        rotateOutUpRight,
        hinge,
        jackInTheBox,
        rollIn,
        rollOut,
        zoomIn,
        zoomInDown,
        zoomInLeft,
        zoomInRight,
        zoomInUp,
        zoomOut,
        zoomOutDown,
        zoomOutLeft,
        zoomOutRight,
        zoomOutUp,
        slideInDown,
        slideInLeft,
        slideInRight,
        slideInUp,
        slideOutDown,
        slideOutLeft,
        slideOutRight,
        slideOutUp,
        heartBeat
    }

    public enum Delay
    {
        [Description("")]
        delay1s = 1,
        [Description("delay-2s")]
        delay2s=2,   //2s
        [Description("delay-3s")]
        delay3s=3,   //3s
        [Description("delay-4s")]
        delay4s=4,   //4s
        [Description("delay-5s")]
        delay5s=5,    //5s
        keepalways=9999
    }

    public enum Speed
    {
        [Description("")]
        normal = 1000,
        [Description("slow")]
        slow = 2000,       //2s
        [Description("slower")]
        slower = 3000,    //3s
        [Description("fast")]
        fast = 800,       //800ms
        [Description("faster")]
        faster = 500,    //500ms    
    }
}
