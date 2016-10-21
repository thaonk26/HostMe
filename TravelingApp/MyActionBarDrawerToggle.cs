using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SupportActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.Widget;

namespace TravelingApp
{
    public class MyActionBarDrawerToggle : SupportActionBarDrawerToggle
    {
        private AppCompatActivity mHostActivity;
        private int mOpenedResource;
        private int mClosedResource;

        public MyActionBarDrawerToggle (AppCompatActivity host, DrawerLayout drawerLayout, int openedResource, int closedResource)
            :base (host, drawerLayout, openedResource, closedResource)
        {
            mHostActivity = host;
            mOpenedResource = openedResource;
            mClosedResource = closedResource;
        }

        public override void OnDrawerOpened(View drawerView)
        {
            base.OnDrawerOpened(drawerView);
        }

        public override void OnDrawerClosed(View drawerView)
        {
            base.OnDrawerClosed(drawerView);
        }

        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            base.OnDrawerSlide(drawerView, slideOffset);
        }
    }
}