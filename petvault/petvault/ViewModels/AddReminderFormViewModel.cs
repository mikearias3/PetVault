using System;
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
        INavigationService _navigationService;
        public Reminder reminder { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public TimeSpan Time { get; set; }
        public AddReminderFormViewModel(INavigationService navigationService)
        {
            azureService = DependencyService.Get<AzureService>();
            _navigationService = navigationService;
            reminder = new Reminder();
            OnSaveReminderCommand = new DelegateCommand(async () => await RunSafe(SaveReminder()));
            OnCancelCommand = new DelegateCommand(async () => await _navigationService.GoBackAsync());
        }

        async Task SaveReminder()
        {
            var _DateTime = new DateTime(Date.Year, Date.Month, Date.Day, Time.Hours, Time.Minutes, Time.Seconds);
            reminder.Date = _DateTime;
            await azureService.AddReminder(reminder);
            await _navigationService.GoBackAsync();
        }
    }
}
