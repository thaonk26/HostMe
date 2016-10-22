using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            TextView mtxtCountry, mtxtCity, mtxtWork;
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

                if(pos == 1)
                {
                    view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.SearchHostPage, container, false);
                    container.AddView(view);
                    EditText country = view.FindViewById<EditText>(Resource.Id.txtEditSearchCountry);
                    EditText city = view.FindViewById<EditText>(Resource.Id.txtEditSearchCity);
                    Button search = view.FindViewById<Button>(Resource.Id.btnSearchHost);
                    search.Click += async (sender, e) =>
                    {
                        string url = "http://hostapi.azurewebsites.net/api/hosts?search=" + country + "$" + city;
                        JsonValue json = await FetchHostAsync(url);

                    };
                }
                else if(pos == 2)
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

            private async Task<JsonValue> FetchHostAsync(string url)
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";
                using (WebResponse response = await request.GetResponseAsync())
                {
                    // Get a stream representation of the HTTP web response:
                    using (Stream stream = response.GetResponseStream())
                    {
                        // Use this stream to build a JSON document object:
                        JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                        Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                        // Return the JSON document:
                        return jsonDoc;
                    }
                }
            }

            private void ParseAndDisplay(JsonValue json)
            {
                mtxtCountry = view.FindViewById<TextView>(Resource.Id.txtDisplayCountry);
                mtxtCity = view.FindViewById<TextView>(Resource.Id.txtDisplayCity);
                mtxtWork = view.FindViewById<TextView>(Resource.Id.txtDisplayWork);

                JsonValue hosts = json["Hosts"];

                mtxtCountry.Text = hosts["country"];
                mtxtCity.Text = hosts["city"];
                mtxtWork.Text = hosts["work"];


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
    }
}