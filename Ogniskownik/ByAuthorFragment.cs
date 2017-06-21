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
    public class ByAuthorFragment : Android.Support.V4.App.Fragment
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

            List<string> authors;
            using (StreamReader sr = new StreamReader(this.Activity.Assets.Open("songs.xml")))
            {
                XmlDataHelper helper = new XmlDataHelper(sr);
                authors = helper.getAuthors();
            }
            ListView listView = view.FindViewById<ListView>(Resource.Id.listView);
            var adapter = new SingleArrayAdapter(this.Activity,authors);
            listView.Adapter = adapter;
            return view;
        }
    }
    public class SingleArrayAdapter : BaseAdapter<string>
    {
        private Context mcontext;
        private List<string> mdata;

        public SingleArrayAdapter(Context context,List<string>data)
        {
            mdata = data;
            mcontext = context;
        }
        public override string this[int position]
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
            if(convertView==null)
            {
                convertView=LayoutInflater.From(mcontext).Inflate(Resource.Layout.SimpleItem1, null, false);
            }
            var textView=convertView.FindViewById<TextView>(Resource.Id.text1);
            textView.Text = mdata[position];
            var forecastImage = convertView.FindViewById<ImageView>(Resource.Id.image);
            forecastImage.SetColorFilter(Color.White);
            return convertView;
        }
    }
}