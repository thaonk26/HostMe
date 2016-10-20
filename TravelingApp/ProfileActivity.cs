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

            mBtnChangePassword.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction(); //This pulls up the dialog
                dialog_Change_Password changePassword = new dialog_Change_Password();
                changePassword.Show(transaction, "dialog fragment");


            };
        }
    }
}