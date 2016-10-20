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
        private TextView mTxtSysLog;

        public event EventHandler<OnSignInEventArgs> mOnSignInComplete;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_sign_in, container, false);
            mTxtEmail = view.FindViewById<EditText>(Resource.Id.txtEmail);
            mTxtPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);
            mBtnSignIn = view.FindViewById<Button>(Resource.Id.btnDialogSignIn);
            mTxtSysLog = view.FindViewById<TextView>(Resource.Id.txtSysLog);

            mBtnSignIn.Click += MBtnSignIn_Click;

            return view;
        }

        private void MBtnSignIn_Click(object sender, EventArgs e)
        {
            //user has clicked signin
            int UserId = 0;
            string Message = "";

            mOnSignInComplete.Invoke(this, new OnSignInEventArgs(mTxtEmail.Text, mTxtPassword.Text));
            MySqlConnection connection = new MySqlConnection("Server=db4free.net;Port=3306;database=reflex;User Id=flex;Password=asd123;charset=utf8");
            try
            {
                MySqlDataReader reader;
                if (connection.State == ConnectionState.Closed)
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM UserInfo WHERE Email = @Email AND Password = @Password", connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Email", mTxtEmail.Text);
                    cmd.Parameters.AddWithValue("@Password", mTxtPassword.Text);
                    connection.Open();

                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserId = reader.GetInt32(0);
                    }
                    if(UserId == 0)
                    {
                        Message = "Email or Password is incorrect";
                    }
                    reader.Close();
                    connection.Close();
                }

            }
            catch(Exception exe)
            {
                Message = exe.Message;
            }
            finally
            {
                connection.Close();
            }
            this.Dismiss();
        }
    }
}