using System;
using System.Collections.Generic;

namespace Thunder.Blazor.Components
{
    public class TPage : TComponent
    {
        public  Dictionary<string,TContext> ComponentContexts { get; set; }

        public void RegisterService(string name,TContext context)
        {

        }
    }
}
