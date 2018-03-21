using System;
using Prism.Navigation;

namespace petvault.ViewModels
{
    public class HomePageViewModel
    {
        INavigationService _navigationService; 
        public HomePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
