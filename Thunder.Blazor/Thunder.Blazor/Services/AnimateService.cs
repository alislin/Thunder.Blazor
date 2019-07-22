/* Ceated by Ya Lin. 2019/7/22 16:35:13 */

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Thunder.Standard.Lib.Extension;

namespace Thunder.Blazor.Services
{
    public class AnimateService
    {
        public AnimateService(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        public async Task Start<T>(AnimateData data, Action<T> callback=null)
        {
            var jscall = new JsAction<T>
            {
                Action = callback
            };
            await JsRuntime.InvokeAsync<object>("thunder.animateCSS", new object[] { data, jscall.ToObjectRef() });
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
        /// 动画类型
        /// </summary>
        public string type  => AnimateType.ToString();
        /// <summary>
        /// 持续时间
        /// </summary>
        public string delay => AnimateDelay.ToDescriptionString();
        /// <summary>
        /// 动画速度
        /// </summary>
        public string speed => AnimateSpeed.ToString();
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
        normal=1000,
        slow=2000,       //2s
        slower=3000,    //3s
        fast=800,       //800ms
        faster=500,    //500ms    
    }
}
