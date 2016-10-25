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
using Android.Support.Design.Widget;

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
        private NavigationView mNavView;
        private AdapterView mHomeItem;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ProfilePage);

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);



            SetSupportActionBar(mToolbar);

            mBtnSavedItems = FindViewById<Button>(Resource.Id.btnSavedItems);
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

            mNavView = FindViewById<NavigationView>(Resource.Id.nav_view);
            mHomeItem = FindViewById<AdapterView>(Resource.Id.nav_home);
            mNavView.NavigationItemSelected += MNavView_NavigationItemSelected;

            if(bundle != null)
            {
                if(bundle.GetString("DrawerState") == "Opened")
                {
                    SupportActionBar.SetTitle(Resource.String.openDrawer);
                }
                else
                {
                    SupportActionBar.SetTitle(Resource.String.closeDrawer);
                }
            }
            else
            {
                //first time activity is ran
                SupportActionBar.SetTitle(Resource.String.closeDrawer);
            }
        }

        private void MNavView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_home):
                    Toast.MakeText(this, "Profile!", ToastLength.Short).Show();
                    Intent intent = new Intent(this, typeof(ProfileActivity));
                    StartActivity(intent);
                    break;
                case (Resource.Id.nav_search):
                    Toast.MakeText(this, "Search!", ToastLength.Short).Show();
                    Intent intentSearch = new Intent(this, typeof(SearchActivity));
                    StartActivity(intentSearch);
                    break;
                case (Resource.Id.nav_convert):
                    Toast.MakeText(this, "Convert!", ToastLength.Short).Show();
                    Intent intentConvert = new Intent(this, typeof(MoneyConvertActivity));
                    StartActivity(intentConvert);
                    break;

            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
            {
                outState.PutString("DrawerState", "Opened");
            }
            else
            {
                outState.PutString("DrawerState", "Closed");
            }
            base.OnSaveInstanceState(outState);
        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            mDrawerToggle.OnOptionsItemSelected(item);  //allows to be opened or closed
            return base.OnOptionsItemSelected(item);
        }

    }
}