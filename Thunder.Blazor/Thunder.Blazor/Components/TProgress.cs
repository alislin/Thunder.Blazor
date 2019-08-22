/* Ceated by Ya Lin. 2019/7/31 16:26:44 */

using Microsoft.AspNetCore.Components;

namespace Thunder.Blazor.Components
{
    public class TProgress : TComponent<TProgressContext>
    {
        [Parameter] public int Value { get; set; }
        [Parameter] public int Max { get; set; } = 100;
        [Parameter] public int Min { get; set; }
        protected double Percent => GetProgressPercent();

        protected double GetProgressPercent()
        {
            var total = Max - Min;
            var p = Value * 100.0 / total;
            p = p > 100 ? 100 : p;
            p = p < 0 ? 0 : p;
            return p;
        }

        public override void LoadDataContext()
        {
            base.LoadDataContext();
            Value = dataContext.Value;
            Max = dataContext.Max;
            Min = dataContext.Min;
        }

        public override void UpdateDataContext()
        {
            base.UpdateDataContext();
            dataContext.Value = Value;
            dataContext.Min = Min;
            dataContext.Max = Max;
        }
    }

    public class TProgressContext : TContext
    {
        public int Value { get; set; }
        public int Max { get; set; } = 100;
        public int Min { get; set; }
    }
}
