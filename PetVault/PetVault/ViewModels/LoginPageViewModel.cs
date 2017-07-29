using System;
using Prism.Navigation;

namespace PetVault.ViewModels
{
    public class LoginPageViewModel
    {
        INavigationService _navigationService;
        public LoginPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void Login()
        {
            _navigationService.NavigateAsync(new System.Uri("http://www.PetVault/HomePage", System.UriKind.Absolute));
        }
    }
}
