using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Thunder.Standard.Lib.Model;

namespace Thunder.Blazor.Components
{
    public class TSelectBase : TComponent<SelectOptionContext>
    {
        private SelectOption selectedItem;
        private string selectvalue;

        [Parameter] public EventCallback<string> SelectedValueChanged { get; set; }
        [Parameter] public EventCallback<SelectOption> SelectedItemChanged { get; set; }
        /// <summary>
        /// 样式名称
        /// </summary>
        [Parameter] public string ClassName { get; set; }
        /// <summary>
        /// 选择值
        /// </summary>
        [Parameter]
        public string SelectedValue
        {
            get => selectedItem?.Value;
            set
            {
                if (selectedItem?.Value != value)
                {
                    SetSelectValue(value);
                }
            }
        }
        /// <summary>
        /// 选择对象
        /// </summary>
        [Parameter]
        public SelectOption SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if (selectedItem?.Value != value?.Value)
                {
                    SetSelectValue(value?.Value);
                }
            }
        }

        protected bool HasGroup => (DataContext?.OptionList?.Count ?? 0) > 1;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            selectvalue = DataContext.Items?.FirstOrDefault(x => x.Selected)?.Value;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                SelectedValue = selectvalue;
            }
            base.OnAfterRender(firstRender);
        }

        protected void SetSelectValue(string s)
        {
            DataContext.SelectedValue = s;
            selectedItem = DataContext.SelectedItem;

            SelectedValueChanged.InvokeAsync(selectedItem?.Value);
            SelectedItemChanged.InvokeAsync(selectedItem);
            OnBindChanged.InvokeAsync(selectedItem);
        }
    }

    public class SelectOptionContext : TContext
    {
        private string selectedValue;

        public List<SelectOption> Items { get; set; }
        public List<IGrouping<string, SelectOption>> OptionList => Items.GroupBy(x => x.Group).ToList();
        public override Type ContextType => typeof(ThunderSelect);
        public string SelectedValue { get => GetSelectItem(selectedValue)?.Value; set => selectedValue = value; }
        public SelectOption SelectedItem { get => GetSelectItem(selectedValue); set => selectedValue = value?.Value; }


        protected SelectOption GetSelectItem(string s)
        {
            var result = Items?.FirstOrDefault(x => x.Value == s);
            if (result == null)
            {
                result = Items?.FirstOrDefault();
            }
            return result;
        }
    }
}
