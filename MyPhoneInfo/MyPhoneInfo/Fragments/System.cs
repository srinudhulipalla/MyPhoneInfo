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
    public class SystemInfo : Fragment
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

            // to update the latest info to UI (available ram)
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
            ListViewAdapter adapter = listView.Adapter as ListViewAdapter;

            this.Activity.RunOnUiThread(() =>
            {
                ActivityManager actManager = (ActivityManager)Context.GetSystemService(Context.ActivityService);
                ActivityManager.MemoryInfo memory = new ActivityManager.MemoryInfo();
                actManager.GetMemoryInfo(memory);

                var itemAvailRAM = adapter.Items.FirstOrDefault(i => i.Id == 130); //Available RAM

                itemAvailRAM.Value = Utils.ToSize(memory.AvailMem, 2);
                adapter.NotifyDataSetChanged();
            });
        }

        List<ListItemModel> GetSystemInfo()
        {
            ActivityManager actManager = (ActivityManager)Context.GetSystemService(Context.ActivityService);
            ActivityManager.MemoryInfo memory = new ActivityManager.MemoryInfo();
            actManager.GetMemoryInfo(memory);
            StatFs statF = new StatFs(Android.OS.Environment.DataDirectory.Path);

            List<ListItemModel> items = new List<ListItemModel>();
            items.Add(new ListItemModel() { Id = 10, Name = "Phone", Value = $"{DeviceInfo.Manufacturer} {DeviceInfo.Name}" });
            items.Add(new ListItemModel() { Id = 20, Name = "Manufacturer", Value = DeviceInfo.Manufacturer });
            items.Add(new ListItemModel() { Id = 30, Name = "Model", Value = DeviceInfo.Model });
            items.Add(new ListItemModel() { Id = 40, Name = "Brand", Value = Build.Brand });
            items.Add(new ListItemModel() { Id = 50, Name = "Board", Value = Build.Board });
            items.Add(new ListItemModel() { Id = 60, Name = "Hardware", Value = Build.Hardware });
            items.Add(new ListItemModel() { Id = 70, Name = "Serial No.", Value = Build.Serial });
            items.Add(new ListItemModel() { Id = 80, Name = "Screen Resolution", Value = $"{DeviceDisplay.ScreenMetrics.Width} x {DeviceDisplay.ScreenMetrics.Height}" });
            items.Add(new ListItemModel() { Id = 90, Name = "Device Type", Value = $"{DeviceInfo.Platform} {DeviceInfo.Idiom}" });
            items.Add(new ListItemModel() { Id = 110, Name = $"{DeviceInfo.Platform} Version", Value = DeviceInfo.VersionString });
            items.Add(new ListItemModel() { Id = 120, Name = $"Total RAM", Value = Utils.ToSize(memory.TotalMem, 2) });
            items.Add(new ListItemModel() { Id = 130, Name = $"Avilable RAM", Value = Utils.ToSize(memory.AvailMem, 2) });
            items.Add(new ListItemModel() { Id = 140, Name = $"Total Internal Storage", Value = Utils.ToSize(statF.TotalBytes, 2) });
            items.Add(new ListItemModel() { Id = 150, Name = $"Avilable Available Internal Storage", Value = Utils.ToSize(statF.AvailableBytes, 2) });

            return items;
        }

    }
}