using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Thunder.Blazor.Components;
using Thunder.Standard.Lib.Model;

namespace Thunder.Blazor.Components
{
    public class TSelectBase : TComponent<SelectOptionContext>
    {
        private string selectedValue;
        private SelectOption selectedItem;
        private string selectvalue;
        private bool InitSelected;

        [Parameter] protected EventCallback<string> SelectedValueChanged { get; set; }
        [Parameter] protected EventCallback<SelectOption> SelectedItemChanged { get; set; }
        /// <summary>
        /// 样式名称
        /// </summary>
        [Parameter] protected string ClassName { get; set; }
        /// <summary>
        /// 选择值
        /// </summary>
        [Parameter]
        protected string SelectedValue
        {
            get => selectedValue;
            set
            {
                if (selectedValue != value)
                {
                    selectedValue = value;
                    DataContext.SelectedValue = value;
                    SelectedValueChanged.InvokeAsync(selectedValue);
                    var m = DataContext.Items.FirstOrDefault(x => x.Value == selectedValue);
                    selectedItem = m;
                    DataContext.SelectedItem = m;
                    SelectedItemChanged.InvokeAsync(m);
                }
            }
        }
        /// <summary>
        /// 选择对象
        /// </summary>
        [Parameter]
        protected SelectOption SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
            }
        }

        protected bool HasGroup => (DataContext?.OptionList?.Count ?? 0) > 1;

        protected override void OnInit()
        {
            base.OnInit();
            InitSelected = true;
            selectvalue = DataContext.Items.FirstOrDefault(x => x.Selected).Value;
            //Console.WriteLine($"init {selectvalue}");
        }

        protected override void OnAfterRender()
        {
            if (InitSelected)
            {
                SelectedValue = selectvalue;
                InitSelected = false;
            }
            base.OnAfterRender();
        }
    }

    public class SelectOptionContext : TContext
    {
        public List<SelectOption> Items { get; set; } = new List<SelectOption>();
        public List<IGrouping<string, SelectOption>> OptionList => Items.GroupBy(x => x.Group).ToList();
        public override Type ContextType => typeof(ThunderSelect);
        public string SelectedValue { get; set; }
        public SelectOption SelectedItem { get; set; }
    }
}
