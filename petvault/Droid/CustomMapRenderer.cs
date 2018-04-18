using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.Content;
using Android.Gms.Maps.Model;
using petvault.Controls;
using petvault.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace petvault.Droid
{
    public class CustomMapRenderer : MapRenderer
    {
        List<Position> routeCoordinates;

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals("VisibleRegion"))
            {
                var formsMap = (CustomMap)sender;
                routeCoordinates = formsMap.RouteCoordinates;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(Android.Gms.Maps.GoogleMap map)
        {
            base.OnMapReady(map);

            if (routeCoordinates != null)
            {
				var polylineOptions = new PolylineOptions();
				polylineOptions.InvokeColor(0x66FF0000);
				
				foreach (var position in routeCoordinates)
				{
					polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
				}
				
				NativeMap.AddPolyline(polylineOptions);
            }

        }
    }
}