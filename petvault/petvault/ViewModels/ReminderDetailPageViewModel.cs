using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using petvault.Models;
using petvault.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace petvault.ViewModels
{
    public class ReminderDetailPageViewModel : BaseViewModel, INavigatedAware
    {
        AzureService azureService;
        public Reminder reminder { get; set; }
        public string PetName { get; set; }
        public DelegateCommand OnDeleteReminderCommand { get; set; }
        public DelegateCommand OnUpdateReminderCommand { get; set; }
        public DelegateCommand OnGetPetCommand { get; set; }
        INavigationService _navigationService;
        public ReminderDetailPageViewModel(INavigationService navigationService)
        {
            azureService = DependencyService.Get<AzureService>();
            _navigationService = navigationService;
            reminder = new Reminder();
            OnDeleteReminderCommand = new DelegateCommand(async () => await RunSafe(DeleteReminder()));
            OnUpdateReminderCommand = new DelegateCommand(async () => await RunSafe(UpdateReminder()));
            OnGetPetCommand = new DelegateCommand(async () => await GetPet());
            OnGetPetCommand.Execute();
        }

        async Task GetPet()
        {
            var pets = await azureService.GetPets();

            foreach (var pet in pets)
            {
                if (pet.Id == reminder.Pet)
                {
                    PetName = pet.Name;
                }
            }
        }

        async Task UpdateReminder()
        {
            await azureService.UpdateReminder(reminder);
            await _navigationService.GoBackAsync();
        }

        async Task DeleteReminder()
        {
            await azureService.DeleteReminder(reminder);
            await _navigationService.GoBackAsync();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("reminder"))
            {
                reminder = parameters.GetValue<Reminder>("reminder");
            }
        }
    }
}
