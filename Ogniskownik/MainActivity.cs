using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Collections.Generic;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Support.V7.App;
using MyToolbar = Android.Support.V7.Widget.Toolbar;
using MyDrawer = Android.Support.V7.App.ActionBarDrawerToggle;
using MyDrawerLayout = Android.Support.V4.Widget.DrawerLayout;
using Android.Util;
using Java.Lang;

namespace Ogniskownik
{
    [Activity(Label = "Ogniskownik", MainLauncher = true, Icon = "@drawable/icon", Theme ="@style/MyDrawerTheme")]
    public class MainActivity : DrawerActivity
    {
        private ViewPager mViewPager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            //OnDrawer();

            /*if (bundle == null)
                SupportActionBar.SetTitle(Resource.String.LibraryName);
            else
                OnRecreate(bundle);*/

            mViewPager = FindViewById<ViewPager>(Resource.Id.pager);
            mViewPager.Adapter = new MyPagerAdapter(SupportFragmentManager);
        }

        private void OnRecreate(Bundle bundle)
        {
            if (bundle.GetBoolean("IsOpen"))
                SupportActionBar.SetTitle(Resource.String.Menu);
            else
                SupportActionBar.SetTitle(Resource.String.LibraryName);
        }
    }
    public class MyPagerAdapter : FragmentPagerAdapter
    {
        public string SongFilter
        {
            set
            {
                var fragment = (ByTitleFragment)mFragments[1];
                fragment.Author = value;
            }
        }
        private Android.Support.V4.App.Fragment[] mFragments;

        public MyPagerAdapter(Android.Support.V4.App.FragmentManager fm):base(fm)
        {
            mFragments = new Android.Support.V4.App.Fragment[2];
        }

        public override int Count=>2;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            switch(position)
            {
                case 0:
                    return new ByAuthorFragment();
                default:
                    return new ByTitleFragment();
            }
        }
        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            var inst= base.InstantiateItem(container, position);
  
            switch (position)
            {
                case 0:
                    mFragments[0] = (ByAuthorFragment)inst;
                    break;
                case 1:
                    mFragments[1] = (ByTitleFragment)inst;
                    break;
            }
            return inst;
        }
    }
    
}

