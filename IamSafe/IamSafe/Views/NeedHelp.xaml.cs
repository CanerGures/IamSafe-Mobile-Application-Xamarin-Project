using IamSafe.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IamSafe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NeedHelp : ContentPage
    {
        public NeedHelp()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    
                    Person person = new Person();
                    person.Name = Name.Text;
                    person.Surname = Surname.Text;
                    person.MotherName = MotherName.Text;
                    person.FatherName = FatherName.Text;
                    person.Longitude = location.Longitude;
                    person.Latitude = location.Latitude;
                    NeedHelpMethod(person);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Faild", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Faild", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }

        }

        private async void NeedHelpMethod(Person person)
        {
            string jsonrequest = "";
            jsonrequest += "{\"Name\":\"";
            jsonrequest += person.Name;
            jsonrequest += "\" , \"Surname\":\"";
            jsonrequest += person.Surname;
            jsonrequest += "\" ,\"MotherName\":\"";
            jsonrequest += person.MotherName;
            jsonrequest += "\",\"FatherName\":\"";
            jsonrequest += person.FatherName;
            jsonrequest += "\",\"Longitude\":\"";
            jsonrequest += person.Longitude;
            jsonrequest += "\" , \"Latitude\":\"";
            jsonrequest += person.Latitude;
            jsonrequest += "\"}";
            var client = new RestClient("http://192.168.1.57/IamSafeApi/api/NeedHelp");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonrequest, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                await DisplayAlert("Alert!", "Distress Call Has Been Sent to Authorities!", "Okay");
            }
            else
            {
                await DisplayAlert("Alert!", "Something Went Wrong, Check Your Internet Connection and Try Again Later", "Okay");
            }
        }
    }
}