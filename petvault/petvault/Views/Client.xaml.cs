using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using petvault.Helpers;
using petvault.Models;
using Xamarin.Forms;

namespace petvault.Views
{
    public partial class Client : ContentPage
    {
        public Client()
        {
            InitializeComponent();
            GetItems();
        }

        public async void GetItems() 
        {
            try
            {
                IEnumerable<PetPositions> data = await PetPositionsManager.GetPetPositionsItemsAsync();
                ObservableCollection<string> list = new ObservableCollection<string>();
                foreach (var item in data)
                {
                    //list.Add(item.PetName + " " + item.Latitude + "," + item.Longitude);
                }
                listView.ItemsSource = list;
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
