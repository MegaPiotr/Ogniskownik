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

namespace Ogniskownik
{
    public class ByAuthorFragment : Fragment
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

            List<Song> authors;
            using (StreamReader sr = new StreamReader(this.Activity.Assets.Open("songs.xml")))
            {
                XmlDataHelper helper = new XmlDataHelper(sr);
                authors = helper.getSongs();
            }
            ListView listView = view.FindViewById<ListView>(Resource.Id.listView);
            //ArrayAdapter adapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleListItem1,authors);
            //listView.Adapter = adapter;
            return view;
        }
    }
}