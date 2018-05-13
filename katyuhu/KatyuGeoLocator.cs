using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace katyuhu.Droid
{
	public class KatyuGeoLocator
	{
        
		private IGeolocator locator = CrossGeolocator.Current;

		public Address address;
		 
		public KatyuGeoLocator()
		{
		}

		public async Task<Position> GetCurrentLocation()
		{
			Position position = null;
			try
			{
				locator.DesiredAccuracy = 100;
                
				position = await locator.GetLastKnownLocationAsync();


				if (position != null)
				{
					//got a cahched position, so let's use it.
					IEnumerable<Address> adrs = await locator.GetAddressesForPositionAsync(position);
					this.address = adrs.ToList().FirstOrDefault<Address>();
					return position;

				}
				position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
				IEnumerable<Address> ads = await locator.GetAddressesForPositionAsync(position);
				this.address = ads.ToList().FirstOrDefault<Address>();

                
			}

			catch (Exception ex)
			{
				//Display error as we have timed out or can't get location.
				Console.WriteLine(ex);
			}

			if (position == null)
				return new Position();

			var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
				position.Timestamp, position.Latitude, position.Longitude,
				position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);



			Debug.WriteLine(output);
			return position;
		}
	}
}
