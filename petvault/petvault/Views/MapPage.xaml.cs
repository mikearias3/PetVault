using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using petvault.Models;
using petvault.ViewModels;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace petvault.Views
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
			((MapPageViewModel)BindingContext).OnMapLoadCommand.Execute(PetMap);
		}
	}
}
