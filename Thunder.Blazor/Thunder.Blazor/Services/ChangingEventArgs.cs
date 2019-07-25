using System;

namespace Thunder.Blazor.Services
{
    public class ChangingEventArgs : ChangedEventArgs
    {
        public bool Cancel { get; set; }
    }
}
