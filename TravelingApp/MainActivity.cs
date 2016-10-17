using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Collections;
using Android.Views;
using System.Threading;

namespace TravelingApp
{
    [Activity(Label = "TravelingApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button mBtnSignUp;
        private ProgressBar mProgressBar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);

            mBtnSignUp.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction(); //This pulls up the dialog
                dialog_SignUp signUpDialog = new dialog_SignUp();
                signUpDialog.Show(transaction, "dialog fragment");

                signUpDialog.mOnSignUpComplete += signUpDialog_mOnSignUpComplete;
            };
        }

        void signUpDialog_mOnSignUpComplete(object sender, OnSignUpEventArgs e)
        {

            mProgressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeARequest);
            thread.Start();
            //send password to db here

        }

        private void ActLikeARequest()
        {
            Thread.Sleep(3000);

            RunOnUiThread(() => { mProgressBar.Visibility = ViewStates.Invisible; });
            //int x = Resource.Animation.slide_right;
        }
    }
}

