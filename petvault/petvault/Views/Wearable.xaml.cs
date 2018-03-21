using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using petvault.Models;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace petvault.Views
{
    public partial class Wearable : ContentPage
    {
        public static MobileServiceClient MobileService = new MobileServiceClient("https://petvault.azurewebsites.net");

        public Wearable()
        {
            InitializeComponent();
        }

        void OnGuardarButtonClicked(object sender, System.EventArgs e)
        {
            UploadDataAsync();
        }

        public async void UploadDataAsync()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync();
            Debug.WriteLine("Position Status: {0}", position.Timestamp);
            Debug.WriteLine("Position Latitude: {0}", position.Latitude);
            Debug.WriteLine("Position Longitude: {0}", position.Longitude);

            PetPositions item = new PetPositions { 
                PetName = dummy.Text, 
                Latitude = position.Latitude.ToString(), 
                Longitude = position.Longitude.ToString() 
            };
            await MobileService.GetTable<PetPositions>().InsertAsync(item);
            result.Text = "Saved data.";
        }
    }
}
