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
    public class ListViewAdapter : BaseAdapter
    {
        private Activity currentActivity;

        public ListViewAdapter(Activity currentActivity, List<ListItemModel> items)
        {
            this.currentActivity = currentActivity;
            this.Items = items;
        }

        public List<ListItemModel> Items
        {
            get;
            set;
        }

        public ListItemModel this[int position]
        {
            get
            {
                return this.Items[position];
            }
        }

        public override int Count
        {
            get
            {
                return Items.Count;
            }
        }

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
            View view = convertView;

            if (view == null)
            {
                view = currentActivity.LayoutInflater.Inflate(Resource.Layout.list_item_main, null);
            }

            view.FindViewById<TextView>(Resource.Id.txtName).Text = Items[position].Name;
            view.FindViewById<TextView>(Resource.Id.txtValue).Text = Items[position].Value;

            return view;
        }
    }
}