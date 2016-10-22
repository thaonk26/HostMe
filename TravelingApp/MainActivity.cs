using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Collections;
using Android.Views;
using System.Threading;
using System.Net;
using SQLite.Extensions;
using SQLite;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Android.Content;

namespace TravelingApp
{
    [Activity(Label = "TravelingApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button mBtnSignUp;
        private Button mBtnSignIn;
        private ProgressBar mProgressBar;
  

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mBtnSignIn = FindViewById<Button>(Resource.Id.btnSignin);
            mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);


            mBtnSignIn.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction(); //This pulls up the dialog
                dialog_SignIn signInDialog = new dialog_SignIn();
                signInDialog.Show(transaction, "dialog fragment");
                transaction.Commit();
                signInDialog.mOnSignInComplete += SignInDialog_mOnSignInComplete;
            };
            mBtnSignUp.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction(); //This pulls up the dialog
                dialog_SignUp signUpDialog = new dialog_SignUp();
                signUpDialog.Show(transaction, "dialog fragment");
                transaction.Commit();
                signUpDialog.mOnSignUpComplete += signUpDialog_mOnSignUpComplete;

            };
           
        }

        private void SignInDialog_mOnSignInComplete(object sender, OnSignInEventArgs e)
        {
            mProgressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeARequest);
            thread.Start();

            StartActivity(typeof(ProfileActivity));
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
            Intent intent = new Intent(this, typeof(ProfileActivity));
            StartActivity(intent);
            //int x = Resource.Animation.slide_right;
        }
        private string createDatabase(string path)
        {
            try
            {
                var connection = new SQLiteAsyncConnection(path);
                {
                    connection.CreateTableAsync<Person>();
                    return "Database created";
                }
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }
        private async Task<string> insertUpdateData(Person data, string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                if (await db.InsertAsync(data) != 0)
                    db.UpdateAsync(data);
                return "Single data file inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }
    }
}

