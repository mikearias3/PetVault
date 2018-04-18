using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using petvault.Controls;
using petvault.Models;
using petvault.Services;
using Plugin.Geolocator;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace petvault.ViewModels
{
    public class MapPageViewModel : BaseViewModel, INavigatedAware
    {
        AzureService azureService;
        public Pet pet { get; set; }
        public IEnumerable<PetPositions> petPositions = new ObservableCollection<PetPositions>();
		public DelegateCommand<CustomMap> OnMapLoadCommand { get; set; }
        public DelegateCommand<CustomMap> OnTrackingCommand { get; set; }
        INavigationService _navigationService;
        public MapPageViewModel(INavigationService navigationService)
        {
            azureService = DependencyService.Get<AzureService>();
            _navigationService = navigationService;
            OnMapLoadCommand = new DelegateCommand<CustomMap>(async (param) => await LoadMap(param));
            OnTrackingCommand = new DelegateCommand<CustomMap>(async (param) => await LoadPetPositions(param));
        }

        private async Task LoadPetPositions(CustomMap _petMap)
        {
            var pps = await azureService.GetPetPositions();

            petPositions = pps.Where(p => p.PetID == pet.Id);

            _petMap.Pins.Clear();

            foreach (var pp in petPositions)
            {
                _petMap.RouteCoordinates.Add(new Position(pp.Latitude, pp.Longitude));

                var pin = new Pin
                {
                    Type = PinType.Generic,
                    Position = new Position(pp.Latitude, pp.Longitude),
                    Label = pet.Name
                };
                _petMap.Pins.Add(pin);
            }

            _petMap.MoveToRegion(MapSpan.FromCenterAndRadius(_petMap.RouteCoordinates.Last(), Distance.FromMiles(1.0)));
        }

        async Task LoadMap(CustomMap _petMap)
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
            _petMap.MoveToRegion(MapSpan.FromCenterAndRadius(petPos, Distance.FromMiles(0.1)));
            var petPin = new Pin
            {
                Type = PinType.Generic,
                Position = petPos,
                Label = pet.Name
            };
            _petMap.Pins.Add(petPin);
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
