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
using System.IO;
using Android.Support.V7.App;
using MyToolbar = Android.Support.V7.Widget.Toolbar;

namespace Ogniskownik
{
    [Activity(Label = "SongActivity", Theme = "@style/MyDrawerTheme")]
    public class SongActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Song);
            var toolbar = FindViewById<MyToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var songList = FindViewById<ListView>(Resource.Id.songlist);
            string text = Intent.GetStringExtra("SongInfo");
            songList.Adapter = new SongArrayAdapter(this, text);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(false);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                this.OnBackPressed();

            return base.OnOptionsItemSelected(item);
        }
    }
    public class MyListItem
    {
        public string text;
        public string chords;
    }
    public class SongArrayAdapter : BaseAdapter<MyListItem>
    {
        public bool ChordVisibility { get; set; }
        private Context mContext;
        private List<MyListItem> mData;
        private string mText;

        public SongArrayAdapter(Context context, string text)
        {
            mContext = context;
            mText = text.Trim();
            mData = new List<MyListItem>();
            using (StringReader reader = new StringReader(mText))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] data=line.Split('|');
                    MyListItem item = new MyListItem() { text = data[0] };
                    if (data.Length > 1)
                        item.chords = data[1];
                    mData.Add(item);
                    line = reader.ReadLine();
                }
            }
        }
        public override MyListItem this[int position]=>mData[position];
        public override int Count=>mData.Count;
        public override long GetItemId(int position)=>position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
                convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.SongItem, null, false);
            var textView1 = convertView.FindViewById<TextView>(Resource.Id.text1);
            var textView2 = convertView.FindViewById<TextView>(Resource.Id.text2);
            textView1.Text = mData[position].text;
            textView2.Text = mData[position].chords;
            return convertView;
        }
    }
}