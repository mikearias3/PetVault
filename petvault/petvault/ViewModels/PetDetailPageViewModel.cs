using System;
using System.Threading.Tasks;
using petvault.Models;
using Prism.Commands;
using Prism.Navigation;

namespace petvault.ViewModels
{
    public class PetDetailPageViewModel : BaseViewModel, INavigatedAware
    {
        public Pet pet { get; set; }
        public DelegateCommand OnFindPetCommand { get; set; }
        INavigationService _navigationService;
        public PetDetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            pet = new Pet();
            //OnFindPetCommand = new DelegateCommand(async () => await RunSafe(OpenMap()));
            OnFindPetCommand = new DelegateCommand(async () => await OpenMap());

        }

        async Task OpenMap()
        {
            var navParams = new NavigationParameters();
            navParams.Add("pet", pet);
            await _navigationService.NavigateAsync("MapPage", navParams);
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
