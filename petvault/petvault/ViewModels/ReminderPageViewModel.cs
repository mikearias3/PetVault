using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using petvault.Helpers;
using petvault.Models;
using Prism.Commands;
using Prism.Navigation;

namespace petvault.ViewModels
{
    public class ReminderPageViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        public DelegateCommand OnAddReminderCommand { get; set; }
        public ObservableCollection<Reminder> Reminders { get; set; }
        public ReminderPageViewModel(INavigationService navigationService)
        {
			_navigationService = navigationService;
            Reminders = new ObservableCollection<Reminder>();
            GetItems();
            OnAddReminderCommand = new DelegateCommand(async () => await OpenReminderForm());
        }

        async Task OpenReminderForm()
        {
            await _navigationService.NavigateAsync("AddReminderForm");
        }

        public void GetItems()
        {
            //TODO: Get Reminders from Azure.
            for (int i = 0; i < 5; i++)
            {
                var reminder = new Reminder() { Title = "Recordatorio " + i, Date = DateTime.Now, Type = "vet" };
                Reminders.Add(reminder);
            }
        }
    }
}
