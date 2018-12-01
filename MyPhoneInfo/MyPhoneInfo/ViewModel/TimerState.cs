using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyPhoneInfo.ViewModel
{
    public class TimerState
    {
        public int counter { get; set; }
        public Timer Timer { get; set; }
        public ListView ListView { get; set; }
    }
}