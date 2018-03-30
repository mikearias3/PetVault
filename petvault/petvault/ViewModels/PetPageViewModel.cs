using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using petvault.Services;
using petvault.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using System.Linq;

namespace petvault.ViewModels
{
    public class PetPageViewModel : BaseViewModel
    {
        AzureService azureService;
        public DelegateCommand OnAddPetCommand { get; set; }
        public DelegateCommand FillListCommand { get; set; }
        INavigationService _navigationService;
        private ObservableCollection<Pet> _Pets = new ObservableCollection<Pet>();
        public ObservableCollection<Pet> Pets
        { 
            get { return _Pets; } 
            set { _Pets = value; } 
        }
        public PetPageViewModel(INavigationService navigationService)
        {
            azureService = DependencyService.Get<AzureService>();
            _navigationService = navigationService;
            OnAddPetCommand = new DelegateCommand(async () => await OpenPetForm());
            FillListCommand = new DelegateCommand(async () => await GetPets());
        }

        private async Task GetPets()
        {
            var pets = await azureService.GetPets();

            foreach (var pet in pets)
            {
                if (_Pets.Where(p => p.Id == pet.Id).Count() == 0)
                {
                    _Pets.Add(pet);
                }
            }
        }

        async Task OpenPetForm()
        {
            await _navigationService.NavigateAsync("AddPetForm");
        }
    }
}
