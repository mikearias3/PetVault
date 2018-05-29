using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using petvault.Models;
using petvault.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace petvault.ViewModels
{
    public class AddReminderFormViewModel : BaseViewModel
    {
        AzureService azureService;
        public DelegateCommand OnSaveReminderCommand { get; set; }
        public DelegateCommand OnCancelCommand { get; set; }
        public DelegateCommand OnGetPetsCommand { get; set; }
        INavigationService _navigationService;
        public Reminder reminder { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public TimeSpan Time { get; set; }
        public Pet SelectedPet { get; set; }
        private ObservableCollection<String> _ReminderTypes = new ObservableCollection<string>();
        public ObservableCollection<String> ReminderTypes
        {
            get { return _ReminderTypes; }
            set { _ReminderTypes = value; }
        }
        public string SelectedType { get; set; }
        private ObservableCollection<Pet> _Pets = new ObservableCollection<Pet>();
        public ObservableCollection<Pet> Pets
        {
            get { return _Pets; }
            set { _Pets = value; }
        }
        public AddReminderFormViewModel(INavigationService navigationService)
        {
            azureService = DependencyService.Get<AzureService>();
            _navigationService = navigationService;
            ReminderTypes.Add("Veterinario");
            ReminderTypes.Add("Alimentacion");
            ReminderTypes.Add("Higine");
            ReminderTypes.Add("Ocio");
            reminder = new Reminder();
            OnSaveReminderCommand = new DelegateCommand(async () => await RunSafe(SaveReminder()));
            OnCancelCommand = new DelegateCommand(async () => await _navigationService.GoBackAsync());
            OnGetPetsCommand = new DelegateCommand(async () => await GetPets());
            OnGetPetsCommand.Execute();
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

        async Task SaveReminder()
        {
            var _DateTime = new DateTime(Date.Year, Date.Month, Date.Day, Time.Hours, Time.Minutes, Time.Seconds);
            reminder.Date = _DateTime;
            reminder.Pet = SelectedPet.Id;
            reminder.Type = SelectedType;
            await azureService.AddReminder(reminder);
            await _navigationService.GoBackAsync();
        }
    }
}
