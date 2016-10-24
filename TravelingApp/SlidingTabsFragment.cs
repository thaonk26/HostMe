using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Json;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Java.Lang;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;

namespace TravelingApp
{
    public class SlidingTabsFragment : Fragment
    {
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            
            return inflater.Inflate(Resource.Layout.fragmentpage, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            mViewPager.Adapter = new SamplePagerAdapter();
            mSlidingTabScrollView.ViewPager = mViewPager;
        }
        public class SamplePagerAdapter : PagerAdapter
        {
            List<string> items = new List<string>();
            //TextView mtxtCountry, mtxtCity, mtxtWork;
            View view;
            public SamplePagerAdapter() : base()
            {
                items.Clear();
                items.Add("Search Host");
                items.Add("Search Airlines");
                items.Add("Search Trains");
            }
            public override int Count
            {
                get
                {
                    return items.Count;
                }
            }
            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }
            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {

                TextView textTitle;
                int pos = position + 1;
                
                view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.SearchHostPage, container, false);
                EditText country = view.FindViewById<EditText>(Resource.Id.txtEditSearchCountry);
                EditText city = view.FindViewById<EditText>(Resource.Id.txtEditSearchCity);
                EditText work = view.FindViewById<EditText>(Resource.Id.txtEditSearchWork);
                TextView mtxtCountry = view.FindViewById<TextView>(Resource.Id.txtDisplayCountry);
                TextView mtxtCity = view.FindViewById<TextView>(Resource.Id.txtDisplayCity);
                TextView mtxtWork = view.FindViewById<TextView>(Resource.Id.txtDisplayWork);
                TextView mtxtAddress = view.FindViewById<TextView>(Resource.Id.txtDisplayAddress);
                TextView mtxtPay = view.FindViewById<TextView>(Resource.Id.txtDisplayPay);

                TextView mtxtAge = view.FindViewById<TextView>(Resource.Id.txtDisplayAge);
                TextView mtxtDate = view.FindViewById<TextView>(Resource.Id.txtDisplayDates);
                TextView mtxtDuration = view.FindViewById<TextView>(Resource.Id.txtDisplayDuration);
                TextView mtxtSpace = view.FindViewById<TextView>(Resource.Id.txtDisplaySpace);
                TextView mtxtGender = view.FindViewById<TextView>(Resource.Id.txtDisplayGender);
                Button search = view.FindViewById<Button>(Resource.Id.btnSearchHost);

                container.AddView(view);
                search.Click +=  (sender, e) =>
              {

                  string url = "http://hostapi.azurewebsites.net/api/hosts?search=" + country.Text;
                  if (city.Text != "") { url += "$" + city.Text; }
                  if (work.Text != "") { url += "$" + work.Text; }
                  //JsonValue json = await FetchHostAsync(url);
                  //ParseAndDisplay(json, mtxtCountry, mtxtCity, mtxtWork, mtxtAddress, mtxtPay, mtxtAge, mtxtDate, mtxtDuration, mtxtSpace, mtxtGender);
                  // var taskA = new Task(() =>
                  //{
                  //});
                  // taskA.Start();

                  GetHost(url, mtxtCountry, mtxtCity, mtxtWork, mtxtAddress, mtxtPay, mtxtAge, mtxtDate, mtxtDuration, mtxtSpace, mtxtGender);
                  //ParseAndDisplay(obj, mtxtCountry, mtxtCity, mtxtWork, mtxtAddress, mtxtPay);
              };

                if (pos == 1)
                {

                }
                if (pos == 2)
                {
                    view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_item, container, false);
                    container.AddView(view);
                    textTitle = view.FindViewById<TextView>(Resource.Id.item_title);
                    textTitle.Text = "Position 2";
                }
                else if(pos == 3)
                {
                    view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_item, container, false);
                    container.AddView(view);
                    textTitle = view.FindViewById<TextView>(Resource.Id.item_title);
                    textTitle.Text = "Position 3";
                }
                return view;
            }

            //private async Task<JsonValue> FetchHostAsync(string url)
            //{
            //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            //    request.ContentType = "application/json";
            //    request.Method = "GET";
            //    using (WebResponse response = await request.GetResponseAsync())
            //    {
            //        // Get a stream representation of the HTTP web response:
            //        using (Stream stream = response.GetResponseStream())
            //        {
            //            // Use this stream to build a JSON document object:
            //            JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
            //            Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

            //            // Return the JSON document:
            //            return jsonDoc;
            //        }
            //    }
            //}
            //private void ParseAndDisplay(JsonValue json, TextView mtxtCountry, TextView mtxtCity, TextView mtxtWork, TextView mtxtAddress, TextView mtxtPay, TextView mtxtAge, TextView mtxtDate, TextView mtxtDuration, TextView mtxtSpace, TextView mtxtGender)
            //{
            //    JsonValue hosts = json[0];
            //    mtxtCountry.Text = hosts["country"];
            //    mtxtCity.Text = hosts["city"];
            //    mtxtWork.Text = hosts["work"];
            //    mtxtAddress.Text = hosts["address"];
            //    mtxtPay.Text = hosts["pay"].ToString();
            //    mtxtAge.Text = hosts["age"].ToString();
            //    mtxtDate.Text = hosts["datesAvailable"];
            //    mtxtDuration.Text = hosts["duration"];
            //    mtxtSpace.Text = hosts["spaceAvailable"].ToString();
            //    mtxtGender.Text = hosts["gender"];
            //}
            private async void GetHost(string url, TextView mtxtCountry, TextView mtxtCity, TextView mtxtWork, TextView mtxtAddress, TextView mtxtPay, TextView mtxtAge, TextView mtxtDate, TextView mtxtDuration, TextView mtxtSpace, TextView mtxtGender)
            {
                List<RootObject> rootResult = new List<RootObject>();
                List<RootObject> temp = new List<RootObject>();
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        rootResult = (List<RootObject>)JsonConvert.DeserializeObject(result, typeof(List<RootObject>));

                    }
                    //return rootResult;
                    ParseAndDisplay(rootResult, mtxtCountry, mtxtCity, mtxtWork, mtxtAddress, mtxtPay, mtxtAge, mtxtDate, mtxtDuration, mtxtSpace, mtxtGender);
                }
            }
            private void ParseAndDisplay(List<RootObject> json, TextView mtxtCountry, TextView mtxtCity, TextView mtxtWork, TextView mtxtAddress, TextView mtxtPay, TextView mtxtAge, TextView mtxtDate, TextView mtxtDuration, TextView mtxtSpace, TextView mtxtGender)
            {
                //var mtxtCountry = view.FindViewById<TextView>(Resource.Id.txtDisplayCountry);
                //mtxtCity = view.FindViewById<TextView>(Resource.Id.txtDisplayCity);
                //mtxtWork = view.FindViewById<TextView>(Resource.Id.txtDisplayWork);
                var dates = json[0].datesAvailable;
                string temp = "";
                for (int i = 0; i < json[0].datesAvailable.Count; i++)
                {
                    temp += Convert.ToString(json[0].datesAvailable[i]);
                }
                
                mtxtCountry.Text = json[0].country;
                mtxtCity.Text = json[0].city;
                mtxtWork.Text = json[0].work;
                mtxtAddress.Text = json[0].address;
                mtxtPay.Text = "$" + json[0].pay.ToString() + "USD";
                mtxtAge.Text = json[0].age;
                mtxtDuration.Text = json[0].duration;
                mtxtDate.Text = temp;
                mtxtSpace.Text = json[0].spaceAvailable.ToString();
                mtxtGender.Text = json[0].gender;

                //mtxtCountry.SetBackgroundResource(Resource.Id.txtDisplayCountry);
                //mtxtCountry.Append(json[0].country);

                //JsonValue hosts = json["Hosts"];
                //JsonValue obj = JsonObject.Parse(json);
                //JsonValue hosts = json[0];
                //var test = JsonValue.Parse(json);
                //var data = test[0];
                //JsonValue hosts = JsonValue.Parse(json);
                //var objs = JsonObject.Parse(hosts);
                //JsonObject obj = hosts as JsonObject;
                //var host = new RootObject();
                //host.country = json[0].country;
                //string hosts = json[0].country;
                //mtxtCountry.Text = hosts;
                //mtxtCountry.Text = host.country;

                //mtxtCountry.Text = hosts["country"];       //stops working here
                //mtxtCity.Text = hosts["city"];
                //mtxtWork.Text = hosts["work"];


            }
            public string GetHeaderTitle(int position)
            {
                return items[position];
            }
            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
            {
                container.RemoveView((View)obj);
            }
        }
        public class RootObject
        {
            public int id { get; set; }
            public string country { get; set; }
            public string city { get; set; }
            public string address { get; set; }
            public int spaceAvailable { get; set; }
            public string work { get; set; }
            public string duration { get; set; }
            public int pay { get; set; }
            public string gender { get; set; }
            public string age { get; set; }
            public List<string> datesAvailable { get; set; }
        }
    }
}