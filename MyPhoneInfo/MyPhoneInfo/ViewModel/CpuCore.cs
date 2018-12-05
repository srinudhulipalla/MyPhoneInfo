using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyPhoneInfo.ViewModel
{
    public class CpuCore
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public long FrequencyKHz { get; set; }

        public string FormattedName
        {
            get
            {
                if (this.Name == null || this.Name.Length <= 3) return this.Name;
                return $"{this.Name.Substring(0, 3).ToUpper()} {this.Name.Substring(3)}";
            } 
        }

        public long FrequencyMHz
        {
            get
            {
                if (this.FrequencyKHz == 0) return this.FrequencyKHz;
                return this.FrequencyKHz / 1000;
            }
        }
    }
}