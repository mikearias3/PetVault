using System;
using System.Collections.Generic;
using petvault.ViewModels;
using Xamarin.Forms;

namespace petvault.Views
{
    public partial class PetDetailPage : ContentPage
    {
        public PetDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((PetDetailPageViewModel)BindingContext).FillListCommand.Execute();
        }
    }
}
