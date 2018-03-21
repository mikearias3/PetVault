using System;
using Prism.Navigation;

namespace petvault.ViewModels
{
    public class WearableViewModel
    {
        INavigationService _navigationService;
        public WearableViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
