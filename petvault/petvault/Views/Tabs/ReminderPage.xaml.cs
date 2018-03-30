using System;
using System.Collections.Generic;
using petvault.ViewModels;
using Xamarin.Forms;

namespace petvault.Views.Tabs
{
    public partial class ReminderPage : ContentPage
    {
        public ReminderPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((ReminderPageViewModel)BindingContext).FillListCommand.Execute();
        }
    }
}
