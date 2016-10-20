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

namespace TravelingApp
{
    [Activity(Label = "ProfileActivity")]
    class ProfileActivity : Activity
    {
        private Button mBtnSavedItems;
        private Button mBtnChangePassword;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ProfilePage);

            mBtnSavedItems = FindViewById<Button>(Resource.Id.btnSavedItems);
            mBtnChangePassword = FindViewById<Button>(Resource.Id.btnChangePassword);


        }
    }
}