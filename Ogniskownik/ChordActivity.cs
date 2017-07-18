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
using Android.Support.V7.App;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using System.Reflection;
using MyToolbar = Android.Support.V7.Widget.Toolbar;
using Java.Lang;

namespace Ogniskownik
{
    [Activity(Label = "ChordActivity",Theme ="@style/MyDrawerTheme")]
    public class ChordActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Chord);
            var toolbar = FindViewById<MyToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            GridView gv = FindViewById<GridView>(Resource.Id.chordGridView);
            string key = Intent.GetStringExtra("chord");
            Title=key;
            var adapter = new MyGridAdapter2(this, GetChordsInfo(key));

            gv.Adapter=adapter;
            gv.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
             {
                /*FragmentManager fm = getSupportFragmentManager();
                ChordFragmentDialog editNameDialogFragment = new ChordFragmentDialog();
                editNameDialogFragment.setRes((GridItem)gv.getItemAtPosition(position));
                editNameDialogFragment.show(fm, "fragment_edit_name");*/
             };
        }
        private List<GridItem> GetChordsInfo(string key)
        {
            var list = new List<GridItem>();
            var assembly = typeof(ChordActivity).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Ogniskownik.chords.xml");
            XPathDocument xmlPathDoc = new XPathDocument(stream);
            var mNavigator = xmlPathDoc.CreateNavigator();

            XPathNodeIterator nodes = mNavigator.Select("//chords_array[@name=\"" + key.ToLower() + "\"]/chord");
            while (nodes.MoveNext())
            {
                GridItem item = new GridItem()
                {
                    Name = nodes.Current.GetAttribute("name", ""),
                    Resource = nodes.Current.GetAttribute("resource", "")
                };
                list.Add(item);
            }

            return list;
        }
    }
    public class GridItem
    {
        public string Resource { get; set; }
        public string Name { get; set; }
        public int GetImage(Context context)=>context.Resources.GetIdentifier(Resource, "drawable", context.PackageName);
    }
    class MyGridAdapter2:BaseAdapter<string>
    {
        private Context mContext;
        private List<GridItem> mImages;
        public MyGridAdapter2(Context context, List<GridItem> images)
        {
            mContext = context;
            mImages = images;
        }

        public override string this[int position] => mImages[position].Name;
        public override int Count => mImages.Count;
        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.ChordItem, null);
                ImageView imageView = convertView.FindViewById<ImageView>(Resource.Id.imageView);
                GridItem item = mImages[position];
                var img = item.GetImage(mContext);
                imageView.SetImageResource(img);
                TextView textView = convertView.FindViewById<TextView>(Resource.Id.description);
                textView.Text=item.Name;
            }
            return convertView;
        }
    }
}