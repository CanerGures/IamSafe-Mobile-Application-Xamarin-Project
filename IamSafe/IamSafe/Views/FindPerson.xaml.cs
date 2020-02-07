using IamSafe.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.CSharp;
using Newtonsoft.Json;

namespace IamSafe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindPerson : ContentPage
    {
        public FindPerson()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void IamSafeEvent(object sender, EventArgs e)
        {
            
            Person person = new Person();
            person.Name = Name.Text;
            person.Surname = Surname.Text;
            person.MotherName = MotherName.Text;
            person.FatherName = FatherName.Text;
            FindCasualty(person);
        }
        public async void FindCasualty(Person person)
        {
            
           GetPersonModel jsonrequest = new GetPersonModel();
            jsonrequest.Name = person.Name;
            jsonrequest.Surname = person.Surname;
            jsonrequest.MotherName = person.MotherName;
            jsonrequest.FatherName = person.FatherName;
            


           var client = new RestClient("http://192.168.1.57/IamSafeApi/api/getPerson");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(jsonrequest), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                await DisplayAlert("Alert!", "The Person You Have Searched, Reported That He/She is Safe", "Okay");
            }
            else
            {
                await DisplayAlert("Alert!", "The Person You Have Searched, Did Not Report That He/She is Safe", "Okay");
            }
        }

    }

}