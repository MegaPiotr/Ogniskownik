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
using System.IO;
using Android.Graphics;
using Android.Support.V4.View;
using Android.Support.V4.Content;

namespace Ogniskownik
{
    public class ByAuthorFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if(savedInstanceState!=null)
            {

            }
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Log.Info("MOJE", "CREATE AUT START");
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ListViewLayout, container, false);

            List<string> authors;
            XmlDataHelper helper = new XmlDataHelper();
            authors = helper.GetAuthors();
      
            ListView listView = view.FindViewById<ListView>(Resource.Id.listView);
            var adapter = new SingleArrayAdapter(this.Activity,authors);
            listView.Adapter = adapter;
            listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                ViewPager pager = this.Activity.FindViewById<ViewPager>(Resource.Id.pager);
                MyPagerAdapter myAdapter =(MyPagerAdapter)pager.Adapter;
                pager.SetCurrentItem(1, true);
                myAdapter.SongFilter = authors[e.Position];
            };
            Log.Info("MOJE", "CREATE ACT END");
            return view;
        }
    }
    public class SingleArrayAdapter : BaseAdapter<string>
    {
        private Context mContext;
        private List<string> mData;

        public SingleArrayAdapter(Context context,List<string>data)
        {
            mData = data;
            mContext = context;
        }
        public override string this[int position]=>mData[position];
        public override int Count=>mData.Count;
        public override long GetItemId(int position)=>position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if(convertView==null)
                convertView=LayoutInflater.From(mContext).Inflate(Resource.Layout.SimpleItem1, null, false);
            
            var textView=convertView.FindViewById<TextView>(Resource.Id.text1);
            textView.Text = mData[position];
            var forecastImage = convertView.FindViewById<ImageView>(Resource.Id.image);
            forecastImage.SetColorFilter(new Color(ContextCompat.GetColor(mContext, Resource.Color.Text)));
            return convertView;
        }
    }
}