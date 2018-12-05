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

        public string ClockName
        {
            get
            {
                if (this.Name == null) return this.Name;
                string id = this.Name.ToLower().Replace("cpu", string.Empty);

                int clockId = 0;

                if (int.TryParse(id, out clockId))
                {
                    return $"Core {clockId + 1}";
                }
                else
                {
                    return this.Name;
                }
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