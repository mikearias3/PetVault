using System;
using System.Threading.Tasks;
using petvault.Models;
using Plugin.Geolocator;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms.Maps;

namespace petvault.ViewModels
{
    public class MapPageViewModel : BaseViewModel, INavigatedAware
    {
        public Pet pet { get; set; }
		public DelegateCommand<Map> OnMapLoadCommand { get; set; }
        INavigationService _navigationService;
        public MapPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            OnMapLoadCommand = new DelegateCommand<Map>(async (param) => await LoadMap(param));
        }

        async Task LoadMap(Map petMap)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            //var locator = CrossGeolocator.Current;
            //var pos = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            //var userPos = new Position(pos.Latitude, pos.Longitude);
            //var userPin = new Pin
            //{
            //    Type = PinType.SavedPin,
            //    Position = userPos,
            //    Label = "Estás aquí"
            //};

            var petPos = new Position(pet.LastLatitude, pet.LastLongitude);
            petMap.MoveToRegion(MapSpan.FromCenterAndRadius(petPos, Distance.FromMiles(0.1)));
            var petPin = new Pin
            {
                Type = PinType.Generic,
                Position = petPos,
                Label = pet.Name
            };
            petMap.Pins.Add(petPin);
            //petMap.Pins.Add(userPin);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("pet"))
            {
                pet = parameters.GetValue<Pet>("pet");
            }
        }
    }
}
