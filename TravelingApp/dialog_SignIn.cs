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
using MySql.Data.MySqlClient;
using System.Data;

namespace TravelingApp
{
    public class OnSignInEventArgs : EventArgs
    {
        private string mEmail;
        private string mPassword;


        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }

        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }

        public OnSignInEventArgs(string email, string password) : base()
        {

            Email = email;
            Password = password;
        }

    }
    class dialog_SignIn : DialogFragment
    {
        private EditText mTxtEmail;
        private EditText mTxtPassword;
        private Button mBtnSignIn;

        public event EventHandler<OnSignInEventArgs> mOnSignInComplete;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_sign_in, container, false);
            mTxtEmail = view.FindViewById<EditText>(Resource.Id.txtEmail);
            mTxtPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);
            mBtnSignIn = view.FindViewById<Button>(Resource.Id.btnDialogSignIn);

            mBtnSignIn.Click += MBtnSignIn_Click;

            return view;
        }

        private void MBtnSignIn_Click(object sender, EventArgs e)
        {
            //user has clicked signin
            mOnSignInComplete.Invoke(this, new OnSignInEventArgs(mTxtEmail.Text, mTxtPassword.Text));
            MySqlConnection con = new MySqlConnection("Server=db4free.net;Port=3306;database=reflex;User Id=flex;Password=asd123;charset=utf8");
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO UserInfo( Email, Password) VALUES(@Email, @Password)", con);
                    if(cmd.Parameters.Contains(mTxtEmail.Text) && cmd.Parameters.Contains(mTxtPassword.Text))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();

                    }

                }

            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
            this.Dismiss();
        }
    }
}