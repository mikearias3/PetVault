using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using petvault.Helpers;
using petvault.Models;
using petvault.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace petvault.ViewModels
{
    public class ReminderPageViewModel : BaseViewModel
    {
        AzureService azureService;
        public DelegateCommand OnAddReminderCommand { get; set; }
        public DelegateCommand FillListCommand { get; set; }
		INavigationService _navigationService;
        private ObservableCollection<Reminder> _Reminders = new ObservableCollection<Reminder>();
        public ObservableCollection<Reminder> Reminders
        {
            get { return _Reminders; }
            set { _Reminders = value; }
        }
        public ReminderPageViewModel(INavigationService navigationService)
        {
            azureService = DependencyService.Get<AzureService>();
			_navigationService = navigationService;
            OnAddReminderCommand = new DelegateCommand(async () => await OpenReminderForm());
            FillListCommand = new DelegateCommand(async () => await GetReminders());
        }

        async Task OpenReminderForm()
        {
            await _navigationService.NavigateAsync("AddReminderForm");
        }

        private async Task GetReminders()
        {
            var reminders = await azureService.GetReminders();

            foreach (var reminder in reminders)
            {
                if (_Reminders.Where(p => p.Id == reminder.Id).Count() == 0)
                {
                    _Reminders.Add(reminder);
                }
            }
        }
    }
}
