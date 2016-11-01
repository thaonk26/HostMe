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


namespace TravelingApp
{
    class ListViewAdapter : BaseAdapter<Host>
    {
        public List<Host> mItems;
        private Context mContext;
        public ListViewAdapter(Context context, List<Host> items)
        {
            mItems = items;
            mContext = context;
        }
        public override int Count
        {
            get { return mItems.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Host this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if(row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.listview_row, null, false);
            }

            TextView txtHostKey = row.FindViewById<TextView>(Resource.Id.txtHostKey);
            txtHostKey.Text = mItems[position].key;

            TextView txtHostValue = row.FindViewById<TextView>(Resource.Id.txtHostValue);
            txtHostValue.Text = mItems[position].value;

            return row;
        }
    }
}