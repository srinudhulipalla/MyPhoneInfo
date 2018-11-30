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
using Xamarin.Essentials;

namespace MyPhoneInfo.Fragments
{
    public class CPU : Fragment
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
            lstData.Adapter = new GrdAdapter1(Activity);

            return view;
        }
    }

    public class GrdAdapter1 : BaseAdapter
    {
        private Activity curvedActivity;
        private List<fragdata> mItems;

        public GrdAdapter1(Activity curvedActivity)
        {
            this.curvedActivity = curvedActivity;
            mItems = new List<fragdata>();
            mItems.Add(new fragdata() { fname = "VersionString", lname = DeviceInfo.VersionString });
            mItems.Add(new fragdata() { fname = "Platform", lname = DeviceInfo.Platform });
            mItems.Add(new fragdata() { fname = "Idiom", lname = DeviceInfo.Idiom });
            mItems.Add(new fragdata() { fname = "DeviceType", lname = DeviceInfo.DeviceType.ToString() });
        }

        //public override fragdata this[int position] => throw new NotImplementedException();

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