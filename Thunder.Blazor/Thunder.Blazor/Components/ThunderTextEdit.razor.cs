/* Ceated by Ya Lin. 2019/11/21 13:59:20 */

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunder.Blazor.Components
{
    public class ThunderTextEditBase:TComponent
    {
        [Parameter] public RenderFragment CloseButton { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        [Parameter] public string Text { get; set; }
        /// <summary>
        /// 文本更新事件
        /// </summary>
        [Parameter] public EventCallback<string> TextChanged { get; set; }
        /// <summary>
        /// 用户数据
        /// </summary>
        [Parameter] public object Data { get; set; }
        /// <summary>
        /// 显示行数
        /// </summary>
        [Parameter] public int Row { get; set; } = 2;
        /// <summary>
        /// 是否多行（单行模式 回车 退出，多行模式 Ctrl+回车 退出）
        /// </summary>
        [Parameter] public bool IsMultiLine { get; set; }
        /// <summary>
        /// 双击进入编辑
        /// </summary>
        [Parameter] public bool IsDoubleClickEnter { get; set; } = true;
        /// <summary>
        /// 自动确认
        /// </summary>
        [Parameter] public bool AutoConfirm { get; set; } = true;
        /// <summary>
        /// 占位符
        /// </summary>
        [Parameter] public string PlaceHolder { get; set; }
        /// <summary>
        /// 确认提交
        /// </summary>
        [Parameter] public Action<(string text, object data)> Confirm { get; set; }
        /// <summary>
        /// 退出方法
        /// </summary>
        [Parameter] public Action Exit { get; set; }
        /// <summary>
        /// 编辑模式
        /// </summary>
        [Parameter] public bool EditMode { get; set; }
        /// <summary>
        /// 触摸进入编辑模式时长
        /// </summary>
        [Parameter] public int TouchTimeout { get; set; } = 1000;
        /// <summary>
        /// 文本渲染
        /// </summary>
        [Parameter] public Func<string,string> TextRender { get; set; }

        bool touchFlag { get; set; }
        int maxRow = 30;
        bool needConfirm = true;
        protected int showRow => GetShowRow();

        private int GetShowRow()
        {
            var line = Text?.Count(o => o == '\n') ?? Row;
            var r = IsMultiLine && line > Row ? line + 1 : Row;
            r = r > maxRow ? maxRow : r;

            return r;
        }

        private void OnKeyUp(KeyboardEventArgs key)
        {
            //Log($"IsMultiLine:{IsMultiLine} CtrlKey:{key.CtrlKey} Key:{key.Key}");
            if (key.Key == "Escape")
            {
                ExitMode(null);
            }
            if (IsMultiLine)
            {
                if (!(key.CtrlKey && key.Key == "Enter"))
                {
                    return;
                }
            }
            else
            {
                if (!(!key.CtrlKey && key.Key == "Enter"))
                {
                    return;
                }
            }
            //UpdateValue(null);
            if (!IsMultiLine)
            {
                //去除回车
                Text = Text.Replace("\n", "").Replace("\r", "");
            }
            else
            {
                //去掉最后一个回车
                var temp = Text?.Substring(Text.Length - 1);
                if (temp == "\n")
                {
                    Text = Text.Substring(0, Text.Length - 1);
                }
            }
            TextChanged.InvokeAsync(Text);
            if (needConfirm)
            {
                needConfirm = false;
                Confirm?.Invoke((Text, Data));
            }
            ExitMode(null);
        }

        public void ExitMode(object objvalue)
        {
            if (needConfirm && AutoConfirm)
            {
                needConfirm = false;
                Confirm?.Invoke((Text, Data));
            }
            EditMode = false;
            Exit?.Invoke();
        }

        public void EnterMode(object objvalue)
        {
            //关闭之前开启的编辑窗口
            ComponentService.DoAction("ThunderTextEdit");
            //添加关闭指令
            ComponentService.AddAction("ThunderTextEdit", () => ExitMode(null));
            EditMode = true;
            needConfirm = true;
            //刷新组件
            ComponentService.NeedUpdate();
        }

        private async void TouchCheck(object o)
        {
            Console.WriteLine($"{DateTime.Now} TouchCheck");
            //EditMode = (DateTime.Now - touchTime).TotalSeconds > 3;
            //touchTime = DateTime.MaxValue;

            var s = DateTime.Now;
            touchFlag = true;
            await Task.Run(() =>
            {
                while (touchFlag && (DateTime.Now - s).TotalMilliseconds < TouchTimeout + 500)
                {
                    Task.Delay(100).Wait();
                    if ((DateTime.Now - s).TotalMilliseconds > TouchTimeout)
                    {
                        EditMode = true;
                        Update();
                        break;
                    }
                }
                Console.WriteLine($"{DateTime.Now} Exit TouchCheck {EditMode}");
            }).ConfigureAwait(true);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            //local.HighLight();
        }
    }
}
