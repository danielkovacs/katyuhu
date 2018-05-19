
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace katyuhu.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class KatyuActivity : Activity, IOnMapReadyCallback
    {
        private DisplayMetrics metrics;
        RelativeLayout KatyuView;
        EditText CommentET;
        EditText LongitudeET;
        EditText LatitudeET;
        Button PositionBtn;
        MapFragment map;
        //Button AddBtn;
        ImageView[] Tumbs;
        Button CreateBtn;

        public void OnMapReady(GoogleMap googleMap)
        {
            throw new NotImplementedException();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            metrics = new DisplayMetrics();
            this.WindowManager.DefaultDisplay.GetMetrics(metrics);

            KatyuView = new RelativeLayout(this);
            KatyuView.Id = 11110;

            CommentET = new EditText(this);
            CommentET.Id = 11111;
            CommentET.Hint = "comment";
            var rlpComment = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, (int)(metrics.HeightPixels * .1));
            KatyuView.AddView(CommentET, rlpComment);

            var locationLL = new LinearLayout(this);
            locationLL.Orientation = Orientation.Horizontal;
            locationLL.Id = 11112;
            var rlpLocation = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, (int)(metrics.HeightPixels * .1));
            rlpLocation.AddRule(LayoutRules.Below, CommentET.Id);

            LongitudeET = new EditText(this);
            LongitudeET.Hint = "long";
            var llpLongitude = new LinearLayout.LayoutParams((int)(metrics.WidthPixels * .44), (int)(metrics.HeightPixels * .08));
            locationLL.AddView(LongitudeET, llpLongitude);

            LatitudeET = new EditText(this);
            LatitudeET.Hint = "lat";
            var llpLatitude = new LinearLayout.LayoutParams((int)(metrics.WidthPixels * .44), (int)(metrics.HeightPixels * .08));
            locationLL.AddView(LatitudeET, llpLatitude);

            PositionBtn = new Button(this);
            PositionBtn.SetBackgroundResource(Android.Resource.Drawable.IcMenuMyPlaces);
            var llpPosition = new LinearLayout.LayoutParams((int)(metrics.WidthPixels * .12), (int)(metrics.HeightPixels * .08));
            locationLL.AddView(PositionBtn, llpPosition);
            PositionBtn.Click += (sender, e) => {
                Toast.MakeText(this, "Refreshing location...", ToastLength.Long).Show();
            };

            KatyuView.AddView(locationLL, rlpLocation);

            var mapLL = new LinearLayout(this);
            mapLL.Id = 11113;
            mapLL.SetBackgroundColor(Android.Graphics.Color.Red);
            var rlpMap = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, (int)(metrics.HeightPixels * .4));
            rlpMap.AddRule(LayoutRules.Below, locationLL.Id);
            KatyuView.AddView(mapLL, rlpMap);

            FragmentTransaction ft = this.FragmentManager.BeginTransaction();
            map = new MapFragment();
            ft.Replace(mapLL.Id, map);
            ft.Commit();

            //AddBtn = new Button(this);
            //AddBtn.SetBackgroundResource(Android.Resource.Drawable.IcMenuAdd);
            //var rlpAdd = new RelativeLayout.LayoutParams((int)(metrics.WidthPixels * .12), (int)(metrics.HeightPixels * .08));
            //rlpAdd.AddRule(LayoutRules.Below, mapLL.Id);
            //KatyuView.AddView(AddBtn, rlpAdd);

            var tumbLL = new LinearLayout(this);
            tumbLL.Id = 11114;
            tumbLL.Orientation = Orientation.Horizontal;
            var rlpTumb = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, (int)(metrics.HeightPixels * .2));
            rlpTumb.AddRule(LayoutRules.Below, mapLL.Id);

            Tumbs = new ImageView[3];
            var llpTumb = new LinearLayout.LayoutParams((int)(metrics.WidthPixels * .3), (int)(metrics.HeightPixels * .2));
            llpTumb.LeftMargin = (int)(metrics.WidthPixels * .025);
            for (int i = 0; i < 3; i++)
            {
                Tumbs[i] = new ImageView(this);
                Tumbs[i].SetBackgroundColor(Android.Graphics.Color.Blue);
                Tumbs[i].SetBackgroundResource(Android.Resource.Drawable.IcMenuReportImage);
                Tumbs[i].Click += (sender, e) => {
                    Toast.MakeText(this, "Opening camera app...", ToastLength.Long).Show();
                };
                tumbLL.AddView(Tumbs[i], llpTumb);
            }

            KatyuView.AddView(tumbLL, rlpTumb);

            CreateBtn = new Button(this);
            CreateBtn.Text = "create >>";
            var rlpCreate = new RelativeLayout.LayoutParams((int)(metrics.WidthPixels * .4), (int)(metrics.HeightPixels * .08));
            rlpCreate.AddRule(LayoutRules.AlignParentRight);
            rlpCreate.AddRule(LayoutRules.Below, tumbLL.Id);
            KatyuView.AddView(CreateBtn, rlpCreate);
            CreateBtn.Click += (sender, e) => {
                Toast.MakeText(this, "Create new katyu entity...", ToastLength.Long).Show();
            };

            SetContentView(KatyuView);
        }
    }
}
