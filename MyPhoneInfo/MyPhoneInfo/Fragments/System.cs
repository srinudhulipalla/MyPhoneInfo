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

            ListView lstData = (ListView)view.FindViewById(Resource.Id.listView1);

            lstData.Adapter = new GrdAdapter(Activity);

            TimerExampleState s = new TimerExampleState();
            s.v = lstData;

            TimerCallback tc = new TimerCallback(BackgroundAsync);

            Timer t = new Timer(tc, s, 1000, 1000);
            s.tmr = t;

            return view;
        }

        private void BackgroundAsync(object state)
        {
            TimerExampleState s = (TimerExampleState)state;
            ListView v = s.v;

            GrdAdapter obj = v.Adapter as GrdAdapter;

            if (this.Activity == null)
            {
                s.tmr.Dispose();
                s.tmr = null;
                return;
            }

            this.Activity.RunOnUiThread(() =>
            {
                obj[0].lname = DeviceInfo.Model + " " + new Random().Next(1, 100);
                obj.NotifyDataSetChanged();
            });
        }
    }

    class TimerExampleState
    {
        public int counter = 0;
        public Timer tmr;
        public ListView v;
    }

    public class fragdata
    {
        public string fname { get; set; }
        public string lname { get; set; }
    }

    public class GrdAdapter : BaseAdapter
    {
        private Activity curvedActivity;
        private List<fragdata> mItems;

        public GrdAdapter(Activity curvedActivity)
        {
            this.curvedActivity = curvedActivity;
            mItems = new List<fragdata>();
            mItems.Add(new fragdata() { fname = "Model", lname = DeviceInfo.Model });
            mItems.Add(new fragdata() { fname = "Manufacturer", lname = DeviceInfo.Manufacturer });
            mItems.Add(new fragdata() { fname = "Name", lname = DeviceInfo.Name });
        }

        //public override fragdata this[int position] => throw new NotImplementedException();

        public fragdata this[int position]
        {
            get
            {
                return this.mItems[position];
            }
        }

        public override int Count => mItems.Count;

        // public override int Count => ;  

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View v = convertView;
            if (v == null)
            {
                v = curvedActivity.LayoutInflater.Inflate(Resource.Layout.list_item_main, null);
            }
            v.FindViewById<TextView>(Resource.Id.textView1).Text = mItems[position].fname;
            v.FindViewById<TextView>(Resource.Id.textView2).Text = mItems[position].lname;
            return v;
        }
    }

}