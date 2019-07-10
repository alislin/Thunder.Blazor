using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Thunder.Blazor.Components
{
    public class TNavItemInfo:TContext
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public string Text { get; set; }
        public Action Action { get; set; }
        public string Icon { get; set; }
        public int Count { get; set; }
        public object Data { get; set; }
        public bool Acitved { get; set; }

        public override Type ContextType => throw new NotImplementedException();

        //public override Type ContextType => typeof(TView);
    }


}
