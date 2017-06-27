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

namespace Ogniskownik
{
    public class ByTitleFragment : Android.Support.V4.App.Fragment
    {
        private List<Song> songs;
        private SingleArrayAdapter2 adapter;
        private Switch switch1;
        private string mAuthor;
        public string Author{
            set
            {
                using (StreamReader sr = new StreamReader(this.Activity.Assets.Open("songs.xml")))
                {
                    XmlDataHelper helper = new XmlDataHelper(sr);
                    if (String.IsNullOrEmpty(value))
                    {
                        switch1.Visibility = ViewStates.Gone;
                        switch1.Checked = false;
                        songs.Clear();
                        songs.AddRange(helper.getSongs());
                    }
                    else
                    {
                        switch1.Visibility = ViewStates.Visible;
                        switch1.Checked = true;
                        songs.Clear();
                        songs.AddRange(helper.getSongs(value));
                        switch1.Text = "Wykonawca: "+value;
                    }
                    mAuthor = value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Log.Info("MOJE", "CREATE TIT V START");
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.ListViewLayout, container, false);
            switch1 = view.FindViewById<Switch>(Resource.Id.switch1);
            switch1.CheckedChange += delegate (object sender, CompoundButton.CheckedChangeEventArgs e) {
                if (!e.IsChecked)
                    Author = null;
            };
            songs = new List<Song>();
            ListView listView = view.FindViewById<ListView>(Resource.Id.listView);
            adapter = new SingleArrayAdapter2(this.Activity, songs);
            listView.Adapter = adapter;
            if (savedInstanceState == null)
                Author = null;
            else
                Author = savedInstanceState.GetString("author");
            listView.ItemClick+= (object sender, AdapterView.ItemClickEventArgs e) => {
                var activity = new Intent(this.Activity, typeof(SongActivity));
                Song selected = adapter[e.Position];
                string info = "";
                using (StreamReader reader = new StreamReader(this.Activity.Assets.Open("songs.xml")))
                {
                    IDataHelper helper = new XmlDataHelper(reader);
                    info=helper.getSongInfo(selected);
                }
                activity.PutExtra("SongInfo", info);
                StartActivity(activity);
            };
            
            Log.Info("MOJE", "CREATE TIT V END");
            return view;
        }
        public override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString("author", mAuthor);
            base.OnSaveInstanceState(outState);
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
        public override Song this[int position]{get{return mdata[position];}}
        public override int Count{get{return mdata.Count;}}
        public override long GetItemId(int position){return position;}

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