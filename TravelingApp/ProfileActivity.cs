using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.Widget;

namespace TravelingApp
{
    [Activity(Label = "ProfileActivity", Theme ="@style/MyTheme")]
    class ProfileActivity : AppCompatActivity
    {
        private Button mBtnSavedItems;
        private Button mBtnChangePassword;

        private SupportToolbar mToolbar;
        private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ProfilePage);

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);

            SetSupportActionBar(mToolbar);

            //mBtnSavedItems = FindViewById<Button>(Resource.Id.btnSavedItems);
            mBtnChangePassword = FindViewById<Button>(Resource.Id.btnChangePassword);

            mBtnChangePassword.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction(); //This pulls up the dialog
                dialog_Change_Password changePassword = new dialog_Change_Password();
                changePassword.Show(transaction, "dialog fragment");


            };

            mDrawerToggle = new MyActionBarDrawerToggle(
                this,                           //Host Activity
                mDrawerLayout,                  //DrawerLayout
                Resource.String.openDrawer,     //Opened Message
                Resource.String.closeDrawer     //Closed Message
                );
            mDrawerLayout.SetDrawerListener(mDrawerToggle);  //gives drawer toggle permission to call the methods
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            mDrawerToggle.SyncState();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            mDrawerToggle.OnOptionsItemSelected(item);  //allows to be opened or closed
            return base.OnOptionsItemSelected(item);
        }
    }
}