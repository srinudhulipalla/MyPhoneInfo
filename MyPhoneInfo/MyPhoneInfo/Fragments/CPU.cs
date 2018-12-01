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

            ListView lstData = (ListView)view.FindViewById(Resource.Id.listMain);

            List<ListItemModel> items = GetCpuInfo();
            lstData.Adapter = new ListViewAdapter(Activity, items);

            return view;
        }

        List<ListItemModel> GetCpuInfo()
        {
            List<ListItemModel> mItems = new List<ListItemModel>();
            mItems.Add(new ListItemModel() { Name = "VersionString", Value = DeviceInfo.VersionString });
            mItems.Add(new ListItemModel() { Name = "Platform", Value = DeviceInfo.Platform });
            mItems.Add(new ListItemModel() { Name = "Idiom", Value = DeviceInfo.Idiom });
            mItems.Add(new ListItemModel() { Name = "DeviceType", Value = DeviceInfo.DeviceType.ToString() });

            return mItems;
        }

    }
}