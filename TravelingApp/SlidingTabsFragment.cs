using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Java.Lang;

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
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_item, container, false);
                TextView textTitle;
                int pos = position + 1;

                if(pos == 1)
                {
                    view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_item, container, false);
                    container.AddView(view);
                    textTitle = view.FindViewById<TextView>(Resource.Id.item_title);
                    textTitle.Text = "This is a teset";
                    TextView textSubtitle = view.FindViewById<TextView>(Resource.Id.item_subtitle);
                    textSubtitle.Text = "Are you sure?";
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