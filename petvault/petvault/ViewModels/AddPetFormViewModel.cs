using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using petvault.Services;
using petvault.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace petvault.ViewModels
{
    public class AddPetFormViewModel : BaseViewModel
    {
        AzureService azureService;
        public DelegateCommand OnSavePetCommand { get; set; }
        INavigationService _navigationService;
        public Pet pet { get; set; }
        public AddPetFormViewModel(INavigationService navigationService)
        {
            azureService = DependencyService.Get<AzureService>();
            _navigationService = navigationService;
            pet = new Pet();
            OnSavePetCommand = new DelegateCommand(async () => await RunSafe(SavePet()));
        }

        async Task SavePet()
        {
            await azureService.AddPet(pet);
            await _navigationService.GoBackAsync();
        }
    }
}
