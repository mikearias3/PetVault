using System;
using PetVault.ViewModels;
using PetVault.Views;
using PetVault.Views.Tabs;
using Prism.Unity;
using Xamarin.Forms;

namespace PetVault
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
		protected override void OnInitialized()
		{
			InitializeComponent();
            NavigationService.NavigateAsync(new System.Uri("http://www.PetVault/LoginPage", System.UriKind.Absolute));
		}
		
		protected override void RegisterTypes()
		{
            Container.RegisterTypeForNavigation<LoginPage, LoginPageViewModel>();
            Container.RegisterTypeForNavigation<HomePage, HomePageViewModel>();
            Container.RegisterTypeForNavigation<DogPage, DogPageViewModel>();
            Container.RegisterTypeForNavigation<CatPage, CatPageViewModel>();
		}


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
