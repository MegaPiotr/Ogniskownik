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

namespace Ogniskownik
{
    [Activity(Label = "Ogniskownik", MainLauncher = true, Icon = "@drawable/icon", Theme ="@style/MyDrawerTheme")]
    public class MainActivity : AppCompatActivity
    {
        private ViewPager mViewPager;
        private MyNavigationDrawer mDrawer;
        private MyDrawerLayout mDrawerLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            var toolbar = FindViewById<MyToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            mDrawerLayout = FindViewById<MyDrawerLayout>(Resource.Id.drawer_layout);
            mDrawer = new MyNavigationDrawer(this, mDrawerLayout, Resource.String.Menu, Resource.String.LibraryName);

            mDrawerLayout.AddDrawerListener(mDrawer);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            mDrawer.SyncState();

            if (bundle == null)
                SupportActionBar.SetTitle(Resource.String.LibraryName);
            else
                OnRecreate(bundle);

            mViewPager = FindViewById<ViewPager>(Resource.Id.pager);
            mViewPager.Adapter = new MyPagerAdapter(SupportFragmentManager);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            mDrawer.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }

        private void OnRecreate(Bundle bundle)
        {
            if(bundle.GetBoolean("IsOpen"))
                SupportActionBar.SetTitle(Resource.String.Menu);
            else
                SupportActionBar.SetTitle(Resource.String.LibraryName);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
                outState.PutBoolean("IsOpen", true);
            else
                outState.PutBoolean("IsOpen", false);
            base.OnSaveInstanceState(outState);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawer.SyncState();
        }
    }
    public class MyPagerAdapter : FragmentPagerAdapter
    {
        public MyPagerAdapter(Android.Support.V4.App.FragmentManager fm):base(fm){}

        public override int Count { get { return 2; }}

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            if (position == 0) return new ByAuthorFragment();
            else return new ByTitleFragment();
        }
    }
    public class MyNavigationDrawer:MyDrawer
    {
        private int mOpenRes;
        private int mCloseRes;
        private AppCompatActivity mActivity;
        public MyNavigationDrawer(AppCompatActivity activity, MyDrawerLayout layout, int openRes, int closeRes):base(activity,layout,openRes,closeRes)
        {
            mOpenRes = openRes;
            mCloseRes = closeRes;
            mActivity = activity;
        }
        public override void OnDrawerOpened(View drawerView)
        {
            mActivity.SupportActionBar.SetTitle(mOpenRes);
            base.OnDrawerOpened(drawerView);
        }
        public override void OnDrawerClosed(View drawerView)
        {
            mActivity.SupportActionBar.SetTitle(mCloseRes);
            base.OnDrawerClosed(drawerView);
        }
        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            base.OnDrawerSlide(drawerView,slideOffset);
        }

    }
}

