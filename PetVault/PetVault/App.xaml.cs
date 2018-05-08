using Acr.UserDialogs;
using petvault.ViewModels;
using petvault.Views;
using petvault.Views.Forms;
using petvault.Views.Tabs;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

namespace petvault
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync(new System.Uri("/NavigationPage/HomePage?selectedTab=ReminderPage", System.UriKind.Absolute));
            //NavigationService.NavigateAsync(new System.Uri("/Wearable", System.UriKind.Absolute));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Wearable, WearableViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<PetPage, PetPageViewModel>();
            containerRegistry.RegisterForNavigation<ReminderPage, ReminderPageViewModel>();
            containerRegistry.RegisterForNavigation<HelpPage, HelpPageViewModel>();
            containerRegistry.RegisterForNavigation<AddPetForm, AddPetFormViewModel>();
            containerRegistry.RegisterForNavigation<AddReminderForm, AddReminderFormViewModel>();
            containerRegistry.RegisterForNavigation<PetDetailPage, PetDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
			containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
        }
    }
}
