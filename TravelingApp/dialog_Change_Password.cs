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

    class dialog_Change_Password : DialogFragment
    {
        private EditText mCurrentPassword, mNewPassword, mConfirmNewPassword, mUserName;
        private Button mbtnConfirmPassword;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_change_password, container, false);
            mUserName = view.FindViewById<EditText>(Resource.Id.txtUserName);
            mCurrentPassword = view.FindViewById<EditText>(Resource.Id.txtCurrentPassword);
            mNewPassword = view.FindViewById<EditText>(Resource.Id.txtNewPassword);
            mConfirmNewPassword = view.FindViewById<EditText>(Resource.Id.txtConfirmNewPassword);
            mbtnConfirmPassword = view.FindViewById<Button>(Resource.Id.btnChangePassword);

            mbtnConfirmPassword.Click += MbtnConfirmPassword_Click;

            return view;
        }

        private void MbtnConfirmPassword_Click(object sender, EventArgs e)
        {
            int UserId = 0;
            string Message = "";


            MySqlConnection connection = new MySqlConnection("Server=db4free.net;Port=3306;database=reflex;User Id=flex;Password=asd123;charset=utf8");
            try
            {
                MySqlDataReader reader;
                if (connection.State == ConnectionState.Closed)
                {

                    connection.Open();
                    //MySqlCommand cmdSelect = new MySqlCommand("SELECT * FROM UserInfo WHERE Password = @Password", connection);
                    //cmdSelect.Parameters.AddWithValue("@Password", mCurrentPassword.Text);
                    if (mNewPassword.Text == mConfirmNewPassword.Text)
                    {
                        MySqlCommand cmd = new MySqlCommand("UPDATE UserInfo SET Password=@NewPass WHERE UserName=@UserName", connection);
                        cmd.Parameters.AddWithValue("@NewPass", mNewPassword.Text);
                        cmd.Parameters.AddWithValue("@UserName", mUserName.Text);
                        cmd.ExecuteNonQuery();
                    }else
                    {
                        Message = "Your new Password does not match.";
                    }
                    

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