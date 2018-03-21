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
    public class PetPageViewModel : BaseViewModel, INavigatedAware
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
            FillListCommand.Execute();
        }

        private async Task GetPets()
        {
            var pets = await azureService.GetPets();

            //if (Pets == null)
                //Pets = new ObservableCollection<Pet>();

            foreach (var pet in pets)
            {
                if (_Pets.Where(p => p.Id == pet.Id).Count() == 0)
                {
                    _Pets.Add(pet);
                }
            }

            //Pets = new ObservableCollection<Pet>(pets);
        }

        async Task OpenPetForm()
        {
            await _navigationService.NavigateAsync("AddPetForm");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            //TODO: Find a way to update only the new Pets to the list.
            FillListCommand.Execute();
        }
    }
}
