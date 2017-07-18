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
using MyToolbar = Android.Support.V7.Widget.Toolbar;
using MyDrawer = Android.Support.V7.App.ActionBarDrawerToggle;
using MyDrawerLayout = Android.Support.V4.Widget.DrawerLayout;
using Android.Support.V7.App;

namespace Ogniskownik
{
    public class DrawerActivity : AppCompatActivity
    {
        private MyNavigationDrawer mDrawer;
        private MyDrawerLayout mDrawerLayout;
        private static int position=0;

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            mDrawer.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
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
            OnDrawer();
            mDrawer.SyncState();
        }
     
        protected void OnDrawer()
        {
            var toolbar = FindViewById<MyToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            mDrawerLayout = FindViewById<MyDrawerLayout>(Resource.Id.drawer_layout);
            mDrawer = new MyNavigationDrawer(this, mDrawerLayout, Resource.String.Menu, Resource.String.LibraryName);

            mDrawerLayout.AddDrawerListener(mDrawer);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            mDrawer.SyncState();

            string[] drawerItems = { "Biblioteka", "Śpiewniki", "Akordy" };
            var listDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
            listDrawer.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, drawerItems);
            listDrawer.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                if (e.Position != position)
                {
                    position = e.Position;
                    if (e.Position == 0)
                        StartActivity(typeof(MainActivity));
                    else if (e.Position == 2)
                        StartActivity(typeof(ChordBookActivity));
                }
                mDrawerLayout.CloseDrawer((int)GravityFlags.Left);
            };

        }
    }
    public class MyNavigationDrawer : MyDrawer
    {
        private int mOpenRes;
        private int mCloseRes;
        private AppCompatActivity mActivity;

        public MyNavigationDrawer(AppCompatActivity activity, MyDrawerLayout layout, int openRes, int closeRes) : base(activity, layout, openRes, closeRes)
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
            base.OnDrawerSlide(drawerView, slideOffset);
        }
    }
}