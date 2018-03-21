using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Diagnostics;
using Xamarin.Forms;
using petvault.Services;
using System.IO;
using petvault.Models;
using Plugin.Connectivity;

[assembly: Dependency(typeof(AzureService))]
namespace petvault.Services
{
    public class AzureService
    {

        public MobileServiceClient Client { get; set; } = null;
        IMobileServiceSyncTable<Pet> petTable;

#if AUTH
        public static bool UseAuth { get; set; } = true;
#else
        public static bool UseAuth { get; set; } = false;
#endif

        public async Task Initialize()
        {
            if (Client?.SyncContext?.IsInitialized ?? false)
                return;


            var appUrl = "https://petvault.azurewebsites.net";

#if AUTH
            Client = new MobileServiceClient(appUrl, new AuthHandler());

            if (!string.IsNullOrWhiteSpace (Settings.AuthToken) && !string.IsNullOrWhiteSpace (Settings.UserId)) {
                Client.CurrentUser = new MobileServiceUser (Settings.UserId);
                Client.CurrentUser.MobileServiceAuthenticationToken = Settings.AuthToken;
            }
#else
            //Create our client

            Client = new MobileServiceClient(appUrl);

#endif

            //InitialzeDatabase for path
            var path = "syncstore.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);

            //Define table
            store.DefineTable<Pet>();


            //Initialize SyncContext
            await Client.SyncContext.InitializeAsync(store);

            //Get our sync table that will call out to azure
            petTable = Client.GetSyncTable<Pet>();


        }

        public async Task SyncPet()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                    return;

                await petTable.PullAsync("allPet", petTable.CreateQuery());

                await Client.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync pets, that is alright as we have offline capabilities: " + ex);
            }

        }

        public async Task<IEnumerable<Pet>> GetPets()
        {
            //Initialize & Sync
            await Initialize();
            await SyncPet();

            return await petTable.OrderBy(p => p.Name).ToEnumerableAsync(); ;

        }

        public async Task<Pet> AddPet(Pet pet)
        {
            await Initialize();

            //var pet = new Pet
            //{
            //    Name = name,
            //    Age = age
            //};

            await petTable.InsertAsync(pet);

            await SyncPet();
            return pet;
        }



//        public async Task<bool> LoginAsync()
//        {

//            await Initialize();

//            var provider = MobileServiceAuthenticationProvider.Twitter;
//            var uriScheme = "coffeecups";


//#if __ANDROID__
//            var user = await Client.LoginAsync(Forms.Context, provider, uriScheme);

//#elif __IOS__
//            CoffeeCups.iOS.AppDelegate.ResumeWithURL = url => url.Scheme == uriScheme && Client.ResumeWithURL(url);
//            var user = await Client.LoginAsync(GetController(), provider, uriScheme);
            
//#else
//            var user = await Client.LoginAsync(provider, uriScheme);

//#endif
        //    if (user == null)
        //    {
        //        Settings.AuthToken = string.Empty;
        //        Settings.UserId = string.Empty;
        //        Device.BeginInvokeOnMainThread(async () =>
        //        {
        //            await App.Current.MainPage.DisplayAlert("Login Error", "Unable to login, please try again", "OK");
        //        });
        //        return false;
        //    }
        //    else
        //    {
        //        Settings.AuthToken = user.MobileServiceAuthenticationToken;
        //        Settings.UserId = user.UserId;
        //    }

        //    return true;
        //}


#if __IOS__
         UIKit.UIViewController GetController()
        {
            var window = UIKit.UIApplication.SharedApplication.KeyWindow;
            var root = window.RootViewController;
            if (root == null)
                return null;

            var current = root;
            while (current.PresentedViewController != null)
            {
                current = current.PresentedViewController;
            }

            return current;
        }
#endif
    }
}
