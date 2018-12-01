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
                obj[0].Value = DeviceInfo.Model + " " + new Random().Next(1, 100);
                obj.NotifyDataSetChanged();
            });
        }

        List<ListItemModel> GetSystemInfo()
        {
            List<ListItemModel> mItems = new List<ListItemModel>();
            mItems.Add(new ListItemModel() { Name = "Model", Value = DeviceInfo.Model });
            mItems.Add(new ListItemModel() { Name = "Manufacturer", Value = DeviceInfo.Manufacturer });
            mItems.Add(new ListItemModel() { Name = "Name", Value = DeviceInfo.Name });

            return mItems;
        }

    }
}