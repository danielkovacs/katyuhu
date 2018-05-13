using Android.App;
using Android.Widget;
using Android.OS;
using katyuhu.Base;

namespace katyuhu.Droid
{
    [Activity(Label = "katyuhu", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        string connStr = "server=katyu.hu;user=root;database=Katyuhu;port=3306;password=Lofasz2";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            DBConnector connector = new DBConnector(connStr);
			KatyuGeoLocator locator = new KatyuGeoLocator();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate { 
				button.Text = $"{count++} clicks!";
				var loc = System.Threading.Tasks.Task.Run( () => locator.GetCurrentLocation()).Result;
				button.Text = $"Your current location is : LAT:{loc.Latitude},LON:{loc.Longitude}";
				button.Text += $"Address:{locator.address}";
			};
        }
    }
}

