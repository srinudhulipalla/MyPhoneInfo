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

namespace MyPhoneInfo
{
    public class Utils
    {
        public static string ToSize(long value, int decimalPlaces = 0)
        {
            long kb = 1024;
            long mb = kb * 1024;
            long gb = mb * 1024;
            long tb = gb * 1024;

            var tbVal = Math.Round((double)value / tb, decimalPlaces);
            var gbVal = Math.Round((double)value / gb, decimalPlaces);
            var mbVal = Math.Round((double)value / mb, decimalPlaces);
            var kbVal = Math.Round((double)value / kb, decimalPlaces);

            string size = tbVal > 1 ? string.Format("{0} TB", tbVal)
                                : gbVal > 1 ? string.Format("{0} GB", gbVal)
                                : mbVal > 1 ? string.Format("{0} MB", mbVal)
                                : kbVal > 1 ? string.Format("{0} KB", kbVal)
                                : string.Format("{0} B", Math.Round((double)value, decimalPlaces));

            return size;
        }
    }
}