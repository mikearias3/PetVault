using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using petvault.Models;
using petvault.Services;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace petvault.Views
{
    public partial class Wearable : ContentPage
    {
        AzureService azureService;
        private static Random randStatus = new Random();
        private static List<String> StatusTypes = new List<string>() { "Normal", "Activo", "Inactivo" };

        public Wearable()
        {
            InitializeComponent();
            azureService = DependencyService.Get<AzureService>();
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
                PetID = "3129a8e2-28a0-4d0c-8e48-95e2737bc259",
                Status = GetStatus(),
                Latitude = position.Latitude, 
                Longitude = position.Longitude
            };

            await azureService.AddPetPosition(item);
            await azureService.UpdatePet(item);
            result.Text = "Saved data.";
        }

        private string GetStatus()
        {
            int index = randStatus.Next(0, StatusTypes.Count);
            return StatusTypes[index];
        }
    }
}
