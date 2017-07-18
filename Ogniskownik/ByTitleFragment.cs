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
    public class ByTitleFragment : Android.Support.V4.App.Fragment
    {
        private List<Song> mSongs;
        private SingleArrayAdapter2 mAdapter;
        private Switch mSwitch;
        private string mAuthor;

        public string Author{
            set
            {
                    XmlDataHelper helper = new XmlDataHelper();
                    if (String.IsNullOrEmpty(value))
                    {
                        mSwitch.Visibility = ViewStates.Gone;
                        mSwitch.Checked = false;
                        mSongs.Clear();
                        mSongs.AddRange(helper.GetSongs());
                    }
                    else
                    {
                        mSwitch.Visibility = ViewStates.Visible;
                        mSwitch.Checked = true;
                        mSongs.Clear();
                        mSongs.AddRange(helper.GetSongs(value));
                        mSwitch.Text = "Wykonawca: "+value;
                    }
                    mAuthor = value;
                    mAdapter.NotifyDataSetChanged();
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
            mSwitch = view.FindViewById<Switch>(Resource.Id.switch1);
            mSwitch.CheckedChange += delegate (object sender, CompoundButton.CheckedChangeEventArgs e) {
                if (!e.IsChecked)
                    Author = null;
            };
            mSongs = new List<Song>();
            ListView listView = view.FindViewById<ListView>(Resource.Id.listView);
            mAdapter = new SingleArrayAdapter2(this.Activity, mSongs);
            listView.Adapter = mAdapter;
            if (savedInstanceState == null)
                Author = null;
            else
                Author = savedInstanceState.GetString("author");
            listView.ItemClick+= (object sender, AdapterView.ItemClickEventArgs e) => {
                var activity = new Intent(this.Activity, typeof(SongActivity));
                Song selected = mAdapter[e.Position];
                string info = "";
           
                IDataHelper helper = new XmlDataHelper();
                info=helper.GetSongInfo(selected);
                
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
        private Context mContext;
        private List<Song> mdata;

        public SingleArrayAdapter2(Context context, List<Song> data)
        {
            mdata = data;
            mContext = context;
        }
        public override Song this[int position]=>mdata[position];
        public override int Count=>mdata.Count;
        public override long GetItemId(int position)=>position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
                convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.SimpleItem2, null, false);
            var textView1 = convertView.FindViewById<TextView>(Resource.Id.text1);
            var textView2 = convertView.FindViewById<TextView>(Resource.Id.text2);
            textView1.Text = mdata[position].Title;
            textView2.Text = mdata[position].Author;

            var forecastImage = convertView.FindViewById<ImageView>(Resource.Id.image);
            forecastImage.SetColorFilter(new Color(ContextCompat.GetColor(mContext,Resource.Color.Text)));
            return convertView;
        }
    }
}