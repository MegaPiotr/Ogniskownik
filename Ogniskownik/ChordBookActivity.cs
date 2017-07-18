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
using Java.Lang;
using MyToolbar = Android.Support.V7.Widget.Toolbar;

namespace Ogniskownik
{
    [Activity(Label = "ChordBook", Theme = "@style/MyDrawerTheme")]
    public class ChordBookActivity : DrawerActivity
    {
        private static string[] mNumbers = new string[] {
            "A","B","H","C","Cis","D","Dis","E","F","Fis","G","Gis"};

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ChordBook);
            //OnDrawer();
            Title = "Akordy";
            var gridView = FindViewById<GridView>(Resource.Id.GridView);
            var adapter = new MyGridAdapter(this, mNumbers);
            gridView.Adapter = adapter;
            gridView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                string name = adapter[args.Position];
                var activity2 = new Intent(this, typeof(ChordActivity));
                activity2.PutExtra("chord", name);
                StartActivity(activity2);
            };
        }
    }
    public class MyGridAdapter:BaseAdapter<string>
    {
        private readonly Context mContext;
        private string[] mNames;

        public MyGridAdapter(Context context, string[] names)
        {
            mContext = context;
            mNames = names;
        }

        public override string this[int position] => mNames[position];
        public override int Count=>mNames.Length;
        
        public override long GetItemId(int position)=>position;
      
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.GridFolderItem, null);
                TextView textView = convertView.FindViewById<TextView>(Resource.Id.grid_item_label);
                textView.Text=mNames[position];
            }
            return convertView;
        }
    }
}