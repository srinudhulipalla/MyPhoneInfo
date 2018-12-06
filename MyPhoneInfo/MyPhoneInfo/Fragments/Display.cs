using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MyPhoneInfo.ViewModel;
using Xamarin.Essentials;
using Android.Views;
using Javax.Microedition.Khronos.Opengles;
using Android.Opengl;

namespace MyPhoneInfo.Fragments
{
    public class DisplayInfo : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.list_main, container, false);

            ListView listView = (ListView)view.FindViewById(Resource.Id.listMain);

            List<ListItemModel> items = GetDisplayInfo();
            listView.Adapter = new ListViewAdapter(Activity, items);

            return view;
        }

        private List<ListItemModel> GetDisplayInfo()
        {
            DisplayMetrics displayMetrics = Activity.Resources.DisplayMetrics;

            float xInches = displayMetrics.WidthPixels / displayMetrics.Xdpi;
            float yInches = displayMetrics.HeightPixels / displayMetrics.Ydpi;
            double diagonalInches = Math.Sqrt(xInches * xInches + yInches * yInches);

            string dpi = displayMetrics.DensityDpi.ToString();
            int densityDpi = (int)(displayMetrics.Density * 160f);

            float f1 = TypedValue.ApplyDimension(ComplexUnitType.Dip, 1080, displayMetrics);
            float f2 = TypedValue.ApplyDimension(ComplexUnitType.Dip, 2160, displayMetrics);

            Display display = ((IWindowManager)Activity.GetSystemService(Context.WindowService).JavaCast<IWindowManager>()).DefaultDisplay;

            List<ListItemModel> items = new List<ListItemModel>();
            items.Add(new ListItemModel() { Id = 80, Name = "Screen Width", Value = $"{Math.Round(xInches, 2)} inches" });
            items.Add(new ListItemModel() { Id = 80, Name = "Screen Height", Value = $"{Math.Round(yInches, 2)} inches" });
            items.Add(new ListItemModel() { Id = 80, Name = "Screen Size", Value = $"{Math.Round(diagonalInches, 2)} inches" });
            items.Add(new ListItemModel() { Id = 80, Name = "Density Dpi", Value = $"{densityDpi} ({dpi})" });
            items.Add(new ListItemModel() { Id = 80, Name = "Xdpi", Value = $"{Math.Round(displayMetrics.Xdpi, 2)} dpi" });
            items.Add(new ListItemModel() { Id = 80, Name = "Ydpi", Value = $"{Math.Round(displayMetrics.Ydpi, 2)} dpi" });
            items.Add(new ListItemModel() { Id = 80, Name = "Screen Resolution", Value = $"{DeviceDisplay.ScreenMetrics.Width} x {DeviceDisplay.ScreenMetrics.Height}" });
            items.Add(new ListItemModel() { Id = 80, Name = "Refresh Rate", Value = $"{display.RefreshRate} Hz" });






            return items;
        }
    }
}