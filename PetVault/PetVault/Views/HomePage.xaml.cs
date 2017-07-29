using System;
using System.Collections.Generic;
using PetVault.Views.Tabs;
using Xamarin.Forms;

namespace PetVault.Views
{
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            this.Children.Add(new DogPage());
            this.Children.Add(new CatPage());
            InitializeComponent();
        }
    }
}
