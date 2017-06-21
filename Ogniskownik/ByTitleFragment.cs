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

namespace Ogniskownik
{
    public class ByTitleFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.ListViewLayout, container, false);

            List<Song> songs;
            using (StreamReader sr = new StreamReader(this.Activity.Assets.Open("songs.xml")))
            {
                XmlDataHelper helper = new XmlDataHelper(sr);
                songs = helper.getSongs();
            }
            ListView listView = view.FindViewById<ListView>(Resource.Id.listView);
            var adapter = new SingleArrayAdapter2(this.Activity, songs);
            listView.Adapter = adapter;
            return view;
        }
    }
    public class SingleArrayAdapter2 : BaseAdapter<Song>
    {
        private Context mcontext;
        private List<Song> mdata;

        public SingleArrayAdapter2(Context context, List<Song> data)
        {
            mdata = data;
            mcontext = context;
        }
        public override Song this[int position]
        {
            get
            {
                return mdata[position];
            }
        }
        public override int Count
        {
            get
            {
                return mdata.Count;
            }
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.From(mcontext).Inflate(Resource.Layout.SimpleItem2, null, false);
            }
            var textView1 = convertView.FindViewById<TextView>(Resource.Id.text1);
            var textView2 = convertView.FindViewById<TextView>(Resource.Id.text2);
            textView1.Text = mdata[position].Title;
            textView2.Text = mdata[position].Author;

            var forecastImage = convertView.FindViewById<ImageView>(Resource.Id.image);          
            forecastImage.SetColorFilter(Color.White);
            return convertView;
        }
    }
}