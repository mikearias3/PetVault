using System;
using petvault.Models;
using Prism.Navigation;

namespace petvault.ViewModels
{
    public class PetDetailPageViewModel : BaseViewModel, INavigatedAware
    {
        public Pet pet { get; set; }
        public PetDetailPageViewModel()
        {
            pet = new Pet();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("pet"))
            {
                pet = parameters.GetValue<Pet>("pet");
            }
        }
    }
}
