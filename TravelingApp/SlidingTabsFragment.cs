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
using System.Web.Services;
using System.Web;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Java.Lang;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using Java.Util;
using Android.Graphics;

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
            List<Host> mItemsHost;
            public SamplePagerAdapter() : base()
            {
                items.Clear();
                items.Add("Search Host");
                items.Add("Search Airlines");
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
                
                ListView mListViewKey, mListViewKeyAirlines;

                int pos = position + 1;

                view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.SearchHostPage, container, false);
                AutoCompleteTextView country = view.FindViewById<AutoCompleteTextView>(Resource.Id.txtEditSearchCountry);
                AutoCompleteTextView city = view.FindViewById<AutoCompleteTextView>(Resource.Id.txtEditSearchCity);
                AutoCompleteTextView work = view.FindViewById<AutoCompleteTextView>(Resource.Id.txtEditSearchWork);
                
                Button search = view.FindViewById<Button>(Resource.Id.btnSearchHost);

                EditText mtxtDepartureDate;
                AutoCompleteTextView mtxtOriginCode, mtxtDestinationCode;
                TextView mtxtDepartureTime, mtxtArrivalTime, mtxtDurationFlight, mtxtDepartureAirportCode, mtxtArrivalAirportCode, mtxtDepartureTerminal, mtxtArrivalTerminal;
                Button mbtnSearchAirlines;
                string[] Airport_Codes;
                string[] Country_Names;
                string[] City_Names;
                string[] Work_Type;

                Country_Names = view.Resources.GetStringArray(Resource.Array.country_names);
                ArrayAdapter<string> countryAdapter = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleDropDownItem1Line, Country_Names);
                
                country.Adapter = countryAdapter;

                City_Names = view.Resources.GetStringArray(Resource.Array.city_names);
                ArrayAdapter<string> cityAdapter = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleDropDownItem1Line, City_Names);
                city.Adapter = cityAdapter;

                Work_Type = view.Resources.GetStringArray(Resource.Array.work_type);
                ArrayAdapter<string> workAdapter = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleDropDownItem1Line, Work_Type);
                work.Adapter = workAdapter;

                mListViewKey = view.FindViewById<ListView>(Resource.Id.txtHostListViewKey);
                
                

                container.AddView(view);
                search.Click += (sender, e) =>
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

                  GetHost(url, mListViewKey);
                  //ParseAndDisplay(obj, mtxtCountry, mtxtCity, mtxtWork, mtxtAddress, mtxtPay);
              };

                if (pos == 2)
                {
                    view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.SearchAirlinesPage, container, false);
                    container.AddView(view);
                    //mtxtDurationFlight = view.FindViewById<TextView>(Resource.Id.txtDisplayDurationFlight);
                    //mtxtDepartureAirportCode = view.FindViewById<TextView>(Resource.Id.txtDisplayOriginCode);
                    //mtxtDepartureTime = view.FindViewById<TextView>(Resource.Id.txtDisplayDepartureTime);
                    //mtxtDepartureTerminal = view.FindViewById<TextView>(Resource.Id.txtDisplayTerminal);
                    //mtxtArrivalAirportCode = view.FindViewById<TextView>(Resource.Id.txtDisplayDestinationCode);
                    //mtxtArrivalTime = view.FindViewById<TextView>(Resource.Id.txtDisplayArrivalTime);
                    //mtxtArrivalTerminal = view.FindViewById<TextView>(Resource.Id.txtDisplayArrivalTerminal);

                    mbtnSearchAirlines = view.FindViewById<Button>(Resource.Id.btnSearchAirline);

                    mtxtDepartureDate = view.FindViewById<EditText>(Resource.Id.txtEditSelectDepartureDate);
                    mtxtOriginCode = view.FindViewById<AutoCompleteTextView>(Resource.Id.txtEditSelectOrigin);
                    mtxtDestinationCode = view.FindViewById<AutoCompleteTextView>(Resource.Id.txtEditSelectArrival);

                    Airport_Codes = view.Resources.GetStringArray(Resource.Array.airport_codes);
                    ArrayAdapter<string> adapter = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleDropDownItem1Line, Airport_Codes);
                    mtxtOriginCode.Adapter = adapter;
                    mtxtDestinationCode.Adapter = adapter;

                    mListViewKeyAirlines = view.FindViewById<ListView>(Resource.Id.txtHostListViewKeyAirlines);


                    mbtnSearchAirlines.Click += async (s, e) =>
                   {
                       string origin = mtxtOriginCode.Text.Split('(', ')')[1];
                       string destination = mtxtDestinationCode.Text.Split('(', ')')[1];
                       string url = "https://api.lufthansa.com/v1/operations/schedules/" + origin + "/" + destination + "/" + mtxtDepartureDate.Text + "?directFlights=0";
                       //GetFlight(url, mtxtDurationFlight, mtxtDepartureAirportCode, mtxtDepartureTime, mtxtTerminal, mtxtArrivalAirportCode, mtxtArrivalTime);
                       try
                       {

                       JsonValue json = await FetchAirlineAsync(url);
                       ParseAndDisplayFlights(json, mListViewKeyAirlines);
                       }catch(InvalidCastException ex)
                       {
                           Toast.MakeText(Application.Context, ex.Message, ToastLength.Short).Show();
                       }
                   };
                }
                return view;
            }

            private void ParseAndDisplayFlights(JsonValue json, ListView mListViewKeyAirlines)
            {
                JsonValue scheduleResource = json["ScheduleResource"];
                JsonValue schedule = scheduleResource["Schedule"];
                JsonValue departure, arrival, flight;
                
                mItemsHost = new List<TravelingApp.Host>();

                
                    JsonValue totalyJourney = schedule[0]["TotalJourney"];       
                    mItemsHost.Add(new Host() { key = "Flight Duration: ", value = totalyJourney["Duration"] });

                    try
                    {
                        flight = schedule[0]["Flight"];
                        departure = flight[0]["Departure"];
                        string departureCode = departure["AirportCode"];
                        string departureTime = departure["ScheduledTimeLocal"]["DateTime"];
                        mItemsHost.Add(new Host() { key = "Departure Code: ", value = departureCode.ToString() });
                        mItemsHost.Add(new Host() { key = "Departure Time: ", value = departureTime });
                    }
                    catch
                    {
                        flight = schedule["Flight"];
                        departure = flight["Departure"];
                        string departureCode = departure["AirportCode"];
                        string departureTime = departure["ScheduledTimeLocal"]["DateTime"];
                        mItemsHost.Add(new Host() { key = "Departure Code: ", value = departureCode.ToString() });
                        mItemsHost.Add(new Host() { key = "Departure Time: ", value = departureTime });
                    }
                    try
                    {
                        flight = schedule[0]["Flight"];
                        arrival = flight[0]["Arrival"];
                        string arrivalCode = arrival["AirportCode"];
                        string arrivalTime = arrival["ScheduledTimeLocal"]["DateTime"];
                        mItemsHost.Add(new Host() { key = "Arrival Code: ", value = arrivalCode.ToString() });
                        mItemsHost.Add(new Host() { key = "Arrival Time: ", value = arrivalTime });
                    }
                    catch
                    {
                        flight = schedule["Flight"];
                        arrival = flight["Arrival"];
                        string arrivalCode = arrival["AirportCode"];
                        string arrivalTime = arrival["ScheduledTimeLocal"]["DateTime"];
                        mItemsHost.Add(new Host() { key = "Arrival Code: ", value = arrivalCode.ToString() });
                        mItemsHost.Add(new Host() { key = "Arrival Time: ", value = arrivalTime });
                    }
                    try
                    {
                        string departureTerminal = departure["Terminal"]["Name"];
                        mItemsHost.Add(new Host() { key = "Departure Terminal: ", value = departureTerminal });
                    }
                    catch { mItemsHost.Add(new Host() { key = "Departure Terminal: ", value = "To be Announced" }); }
                    try
                    {
                        string arrivalTerminal = arrival["Terminal"]["Name"];
                        mItemsHost.Add(new Host() { key = "Arrival Terminal: ", value = arrivalTerminal });
                    }
                    catch { mItemsHost.Add(new Host() { key = "Arrival Terminal: ", value = "To be Announced" }); }
                try
                {
                    totalyJourney = schedule[1]["TotalJourney"];
                    mItemsHost.Add(new Host() { key = "Flight Duration: ", value = totalyJourney["Duration"] });

                    try
                    {
                        flight = schedule[1]["Flight"];
                        departure = flight[1]["Departure"];
                        string departureCode = departure["AirportCode"];
                        string departureTime = departure["ScheduledTimeLocal"]["DateTime"];
                        mItemsHost.Add(new Host() { key = "Departure Code: ", value = departureCode.ToString() });
                        mItemsHost.Add(new Host() { key = "Departure Time: ", value = departureTime });
                    }
                    catch
                    {
                       
                    }
                    try
                    {
                        flight = schedule[1]["Flight"];
                        arrival = flight[1]["Arrival"];
                        string arrivalCode = arrival["AirportCode"];
                        string arrivalTime = arrival["ScheduledTimeLocal"]["DateTime"];
                        mItemsHost.Add(new Host() { key = "Arrival Code: ", value = arrivalCode.ToString() });
                        mItemsHost.Add(new Host() { key = "Arrival Time: ", value = arrivalTime });
                    }
                    catch
                    {
                        
                    }
                    try
                    {
                        string departureTerminal = departure["Terminal"]["Name"];
                        mItemsHost.Add(new Host() { key = "Departure Terminal: ", value = departureTerminal });
                    }
                    catch { mItemsHost.Add(new Host() { key = "Departure Terminal: ", value = "To be Announced" }); }
                    try
                    {
                        string arrivalTerminal = arrival["Terminal"]["Name"];
                        mItemsHost.Add(new Host() { key = "Arrival Terminal: ", value = arrivalTerminal });
                    }
                    catch { mItemsHost.Add(new Host() { key = "Arrival Terminal: ", value = "To be Announced" }); }
                }
                catch { }
                try
                {
                    totalyJourney = schedule[2]["TotalJourney"];
                    mItemsHost.Add(new Host() { key = "Flight Duration: ", value = totalyJourney["Duration"] });

                    try
                    {
                        flight = schedule[2]["Flight"];
                        departure = flight[2]["Departure"];
                        string departureCode = departure["AirportCode"];
                        string departureTime = departure["ScheduledTimeLocal"]["DateTime"];
                        mItemsHost.Add(new Host() { key = "Departure Code: ", value = departureCode.ToString() });
                        mItemsHost.Add(new Host() { key = "Departure Time: ", value = departureTime });
                    }
                    catch
                    {

                    }
                    try
                    {
                        flight = schedule[2]["Flight"];
                        arrival = flight[2]["Arrival"];
                        string arrivalCode = arrival["AirportCode"];
                        string arrivalTime = arrival["ScheduledTimeLocal"]["DateTime"];
                        mItemsHost.Add(new Host() { key = "Arrival Code: ", value = arrivalCode.ToString() });
                        mItemsHost.Add(new Host() { key = "Arrival Time: ", value = arrivalTime });
                    }
                    catch
                    {

                    }
                    try
                    {
                        string departureTerminal = departure["Terminal"]["Name"];
                        mItemsHost.Add(new Host() { key = "Departure Terminal: ", value = departureTerminal });
                    }
                    catch { mItemsHost.Add(new Host() { key = "Departure Terminal: ", value = "To be Announced" }); }
                    try
                    {
                        string arrivalTerminal = arrival["Terminal"]["Name"];
                        mItemsHost.Add(new Host() { key = "Arrival Terminal: ", value = arrivalTerminal });
                    }
                    catch { mItemsHost.Add(new Host() { key = "Arrival Terminal: ", value = "To be Announced" }); }
                }
                catch { }

                ListViewAdapter adapter = new ListViewAdapter(Application.Context, mItemsHost);
                mListViewKeyAirlines.Adapter = adapter;
            }

            //private async void GetFlight(string url, TextView mDurationFlight, TextView mDepartureAirportCode, TextView mDepartureTime, TextView mTerminal, TextView mArrivalAirportCode, TextView mArrivalTime)
            //{
            //    List<RootObject2> rootResult = new List<RootObject2>();
            //    object flight = "";
            //    using (var client = new HttpClient())
            //    {
            //        client.DefaultRequestHeaders.Add("authorization", "Bearer z2g9h5966bwa6tygngncr5s6");
            //        client.DefaultRequestHeaders.Add("accept", "application/json");
            //        HttpResponseMessage response = await client.GetAsync(url);
            //        if (response.IsSuccessStatusCode)
            //        {
            //            string result = await response.Content.ReadAsStringAsync();
            //            var test = JsonConvert.DeserializeObject(result);                                                       //WORKS AND IS AN OBJ
            //            flight = JsonConvert.DeserializeObject(result);
                        
            //            //RootObject2 sch = JsonConvert.DeserializeObject<RootObject2>(result);                                         //DOESNT WORK
            //            //var mList = JsonConvert.DeserializeObject<IDictionary<string, RootObject2>>(result);                  //CANT FIND CHILDREN
            //            //foreach (Schedule s in mList) { string txt = s.Flight.Departure.AirportCode; }
            //            //rootResult = (List<RootObject2>)JsonConvert.DeserializeObject(result, typeof(List<RootObject2>));      //DOESNT WORK
            //        }
            //        ParseAndDisplayFlights(rootResult, mDurationFlight, mDepartureAirportCode, mDepartureTime, mTerminal, mArrivalAirportCode, mArrivalTime);

            //    }
            //}

            private async Task<JsonValue> FetchAirlineAsync(string url)
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";
                request.Headers.Add("authorization", "Bearer 5gzev7qz5hrh2gh4fy4bx44n");
                //request.Headers.Add("accept", "application/json");
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
            private async void GetHost(string url, ListView mtxtHostKey)
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
                    ParseAndDisplay(rootResult, mtxtHostKey);
                }
            }
            private void ParseAndDisplay(List<RootObject> json,  ListView mtxtHostKey)
            {
                string temp = "";
                for (int i = 0; i < json[0].datesAvailable.Count; i++)
                {
                    temp += Convert.ToString(json[0].datesAvailable[i]);
                }

                mItemsHost = new List<Host>();
                for (int i = 0; i < json.Count; i++)
                {
                    try
                    {

                        mItemsHost.Add(new Host() { key = "Country: ", value = json[i].country });
                        mItemsHost.Add(new Host() { key = "City: ", value = json[i].city });
                        mItemsHost.Add(new Host() { key = "Work: ", value = json[i].work });
                        mItemsHost.Add(new Host() { key = "Address: ", value = json[i].address });
                        mItemsHost.Add(new Host() { key = "Pay: ", value = "$" + json[i].pay.ToString() + "USD" });
                        mItemsHost.Add(new Host() { key = "Age: ", value = json[i].age });
                        mItemsHost.Add(new Host() { key = "Duration: ", value = json[i].duration });
                        mItemsHost.Add(new Host() { key = "Date: ", value = temp });
                        mItemsHost.Add(new Host() { key = "Space: ", value = json[i].spaceAvailable.ToString() });
                        mItemsHost.Add(new Host() { key = "Gender: ", value = json[i].gender });
                        
                    }
                    catch { }
                }
                
                ListViewAdapter adapter = new ListViewAdapter(Application.Context, mItemsHost);
                
                mtxtHostKey.Adapter = adapter;

                
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
        public class TotalJourney
        {
            public string Duration { get; set; }
        }

        public class Schedule
        {
            public TotalJourney TotalJourney { get; set; }
            public Flight Flight { get; set; }
        }
        public class Flight
        {
            public Departure Departure { get; set; }
            public Arrival Arrival { get; set; }
        }
        public class Departure
        {
            public string AirportCode { get; set; }
            public ScheduledTimeLocal ScheduledTimeLocal { get; set; }
            public Terminal Terminal { get; set; }
        }
        public class Arrival
        {
            public string AirportCode { get; set; }
            public ScheduledTimeLocal ScheduledTimeLocal { get; set; }
        }
        public class Terminal
        {
            public int Name { get; set; }
        }
        public class ScheduledTimeLocal
        {
            public string DateTime { get; set; }
        }
        public class ScheduleResource
        {
            public List<Schedule> ScheduleList { get; set; }
            public Schedule Schedule { get; set; }
        }

        public class RootObject2
        {
            public ScheduleResource ScheduleResource { get; set; }
        }

    }
}