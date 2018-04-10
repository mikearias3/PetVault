using System;
using petvault.Models;
using Prism.Navigation;

namespace petvault.ViewModels
{
    public class MapPageViewModel : BaseViewModel, INavigatedAware
    {
        public Pet pet { get; set; }
        INavigationService _navigationService;
        public MapPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
