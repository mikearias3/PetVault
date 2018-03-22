using System;
using System.Collections.Generic;
using petvault.ViewModels;
using Xamarin.Forms;

namespace petvault.Views.Tabs
{
    public partial class PetPage : ContentPage
    {
        public PetPage()
        {
            InitializeComponent();
        }

		protected override void OnAppearing()
		{
            base.OnAppearing();
            ((PetPageViewModel)BindingContext).FillListCommand.Execute();
		}
	}
}
