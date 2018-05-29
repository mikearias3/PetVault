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
    public class PetDetailPageViewModel : BaseViewModel, INavigatedAware
    {
        AzureService azureService;
        public Pet pet { get; set; }
        public DelegateCommand<Reminder> OnOpenReminderCommand { get; set; }
        public DelegateCommand OnFindPetCommand { get; set; }
        public DelegateCommand FillListCommand { get; set; }
        INavigationService _navigationService;
        private ObservableCollection<Reminder> _Reminders = new ObservableCollection<Reminder>();
        public ObservableCollection<Reminder> Reminders
        {
            get { return _Reminders; }
            set { _Reminders = value; }
        }
        public ObservableCollection<Reminder> rm { get; set; }
        public PetDetailPageViewModel(INavigationService navigationService)
        {
            azureService = DependencyService.Get<AzureService>();
            _navigationService = navigationService;
            pet = new Pet();
            OnFindPetCommand = new DelegateCommand(async () => await OpenMap());
            OnOpenReminderCommand = new DelegateCommand<Reminder>(async (param) => await OpenReminderDetail(param));
            FillListCommand = new DelegateCommand(async () => await GetReminders());
        }

        async Task OpenMap()
        {
            var navParams = new NavigationParameters();
            navParams.Add("pet", pet);
            await _navigationService.NavigateAsync("MapPage", navParams);
        }

        private async Task GetReminders()
        {
            var reminders = await azureService.GetReminders();

            foreach (var reminder in reminders)
            {
                if (_Reminders.Where(p => p.Id == reminder.Id).Count() == 0)
                {
                    if (reminder.Pet == pet.Id)
                    {
                        _Reminders.Add(reminder);
                    }
                }
            }
            rm = new ObservableCollection<Reminder>(_Reminders);
        }

        async Task OpenReminderDetail(Reminder param)
        {
            var navParams = new NavigationParameters();
            navParams.Add("reminder", param);
            await _navigationService.NavigateAsync("ReminderDetailPage", navParams);
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
