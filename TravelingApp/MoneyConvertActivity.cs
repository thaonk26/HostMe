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
    [Activity(Label = "MoneyConvertActivity")]
    public class MoneyConvertActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MoneyConvertPage);
            // Create your application here
            EditText mtxtCurrentMoney;
            TextView mtxtConvertMoney;
            Button mbtnConvertMoney;

            mtxtCurrentMoney = FindViewById<EditText>(Resource.Id.txtEditCurrentMoney);
            mtxtConvertMoney = FindViewById<TextView>(Resource.Id.txtDisplayConvertMoney);

            mbtnConvertMoney = FindViewById<Button>(Resource.Id.btnConvertMoney);

            mbtnConvertMoney.Click += (s, e) =>
            {
                mtxtCurrentMoney.Text.ToString();
                double current = Convert.ToDouble(mtxtCurrentMoney.Text);
                double convert = 6.78 * current;

                mtxtConvertMoney.Text = convert.ToString() + " CNY";
            };
        }
    }
}