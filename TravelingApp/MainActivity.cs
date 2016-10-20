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

namespace TravelingApp
{
    [Activity(Label = "TravelingApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button mBtnSignUp;
        private Button mBtnSignIn;
        private ProgressBar mProgressBar;
        private EditText txtUserName, txtEmail, txtPassword;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mBtnSignIn = FindViewById<Button>(Resource.Id.btnSignin);
            mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            txtUserName = FindViewById<EditText>(Resource.Id.txtUserName);
            txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);

            mBtnSignIn.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction(); //This pulls up the dialog
                dialog_SignIn signInDialog = new dialog_SignIn();
                signInDialog.Show(transaction, "dialog fragment");

                signInDialog.mOnSignInComplete += SignInDialog_mOnSignInComplete;
            };
            mBtnSignUp.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction(); //This pulls up the dialog
                dialog_SignUp signUpDialog = new dialog_SignUp();
                signUpDialog.Show(transaction, "dialog fragment");

                signUpDialog.mOnSignUpComplete += signUpDialog_mOnSignUpComplete;
            };
           
        }

        private void SignInDialog_mOnSignInComplete(object sender, OnSignUpEventArgs e)
        {
            mProgressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeARequest);
            thread.Start();
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

