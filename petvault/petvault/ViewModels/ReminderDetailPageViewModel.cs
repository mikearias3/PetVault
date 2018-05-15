using System;
using System.Threading.Tasks;
using petvault.Models;
using Prism.Commands;
using Prism.Navigation;

namespace petvault.ViewModels
{
    public class ReminderDetailPageViewModel : BaseViewModel, INavigatedAware
    {
        public Reminder reminder { get; set; }
        public DelegateCommand OnDeleteReminderCommand { get; set; }
        public DelegateCommand OnUpdateReminderCommand { get; set; }
        INavigationService _navigationService;
        public ReminderDetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            reminder = new Reminder();
            OnDeleteReminderCommand = new DelegateCommand(async () => await DeleteReminder());
            OnUpdateReminderCommand = new DelegateCommand(async () => await UpdateReminder());
        }

        async Task UpdateReminder()
        {
            //throw new NotImplementedException();
        }

        async Task DeleteReminder()
        {
            //throw new NotImplementedException();
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
