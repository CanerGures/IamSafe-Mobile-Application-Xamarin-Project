using IamSafe.Views;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace IamSafe
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            
        }

        private void IamSafeClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage( new IamSafePage()));
        }

        private void FindPersonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage (new FindPerson()));
        }

        private void NeedHelpClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage (new NeedHelp()));
        }

        private void WeatherClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage (new Weather()));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            

           
        }
    }
        
    
}
