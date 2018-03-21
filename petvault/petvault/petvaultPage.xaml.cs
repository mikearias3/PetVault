using petvault.Views;
using Xamarin.Forms;

namespace petvault
{
    public partial class petvaultPage : ContentPage
    {
        public petvaultPage()
        {
            InitializeComponent();
        }

        void OnWearableButtonClicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Wearable());
        }

        void OnClientButtonClicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Client());
        }
    }
}
