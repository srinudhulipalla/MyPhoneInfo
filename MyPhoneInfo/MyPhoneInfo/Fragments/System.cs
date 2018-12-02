using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MyPhoneInfo.ViewModel;
using Xamarin.Essentials;

namespace MyPhoneInfo.Fragments
{
    public class System : Fragment
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

            List<ListItemModel> items = GetSystemInfo();
            listView.Adapter = new ListViewAdapter(Activity, items);

            // to update the latest info to UI
            TimerCallback timerCallback = new TimerCallback(RunInBackground);

            TimerState state = new TimerState();
            state.ListView = listView;

            Timer timer = new Timer(timerCallback, state, 1000, 1000);
            state.Timer = timer;

            return view;
        }

        private void RunInBackground(object objState)
        {
            TimerState state = (TimerState)objState;

            if (this.Activity == null)
            {
                state.Timer.Dispose();
                state.Timer = null;
                return;
            }

            ListView listView = state.ListView;
            ListViewAdapter obj = listView.Adapter as ListViewAdapter;

            this.Activity.RunOnUiThread(() =>
            {
                ActivityManager memInfo1 = (ActivityManager)Context.GetSystemService(Context.ActivityService);
                ActivityManager.MemoryInfo memInfo = new ActivityManager.MemoryInfo();
                memInfo1.GetMemoryInfo(memInfo);

                obj[0].Value = $"{DeviceInfo.Manufacturer} {DeviceInfo.Name}" + " " + ConvertToSize(memInfo.AvailMem, 2);
                obj.NotifyDataSetChanged();
            });
        }

        List<ListItemModel> GetSystemInfo()
        {
            List<ListItemModel> items = new List<ListItemModel>();
            items.Add(new ListItemModel() { Name = "Phone", Value = $"{DeviceInfo.Manufacturer} {DeviceInfo.Name}" });
            items.Add(new ListItemModel() { Name = "Manufacturer", Value = DeviceInfo.Manufacturer });
            items.Add(new ListItemModel() { Name = "Model", Value = DeviceInfo.Model });
            items.Add(new ListItemModel() { Name = "Brand", Value = Build.Brand });
            items.Add(new ListItemModel() { Name = "Board", Value = Build.Board });
            items.Add(new ListItemModel() { Name = "Hardware", Value = Build.Hardware });
            items.Add(new ListItemModel() { Name = "Serial No.", Value = Build.Serial });
            items.Add(new ListItemModel() { Name = "Screen Resolution", Value = $"{DeviceDisplay.ScreenMetrics.Width} x {DeviceDisplay.ScreenMetrics.Height}" });
            items.Add(new ListItemModel() { Name = "Device Type", Value = $"{DeviceInfo.Platform} {DeviceInfo.Idiom}" });
            items.Add(new ListItemModel() { Name = $"{DeviceInfo.Platform} Version", Value = DeviceInfo.VersionString });

           

            ActivityManager.MemoryInfo memInfo = new ActivityManager.MemoryInfo();

            ActivityManager memInfo1 = (ActivityManager)Context.GetSystemService(Context.ActivityService);

            memInfo1.GetMemoryInfo(memInfo); 

            items.Add(new ListItemModel() { Name = $"Total RAM", Value = ConvertToSize(memInfo.TotalMem, 2) });
            items.Add(new ListItemModel() { Name = $"Avilable RAM", Value = ConvertToSize(memInfo.AvailMem, 2) });

            StatFs stat = new StatFs(Android.OS.Environment.DataDirectory.Path);

            items.Add(new ListItemModel() { Name = $"Total Internal Storage", Value = ConvertToSize(stat.TotalBytes, 2) });
            items.Add(new ListItemModel() { Name = $"Avilable Available Internal Storage", Value = ConvertToSize(stat.AvailableBytes, 2) });

            return items;
        }

        string ConvertToSize(long value, int decimalPlaces = 0)
        {
            long OneKb = 1024;
            long OneMb = OneKb * 1024;
            long OneGb = OneMb * 1024;
            long OneTb = OneGb * 1024;

            var asTb = Math.Round((double)value / OneTb, decimalPlaces);
            var asGb = Math.Round((double)value / OneGb, decimalPlaces);
            var asMb = Math.Round((double)value / OneMb, decimalPlaces);
            var asKb = Math.Round((double)value / OneKb, decimalPlaces);

            string chosenValue = asTb > 1 ? string.Format("{0}Tb", asTb)
                : asGb > 1 ? string.Format("{0} GB", asGb)
                : asMb > 1 ? string.Format("{0} MB", asMb)
                : asKb > 1 ? string.Format("{0} KB", asKb)
                : string.Format("{0} B", Math.Round((double)value, decimalPlaces));

            return chosenValue;
        }

    }
}