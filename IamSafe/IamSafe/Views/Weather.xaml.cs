using IamSafe.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace IamSafe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Weather : ContentPage
    {
        public Weather()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }


        private void citybtn_Clicked(object sender, EventArgs e)
        {
            CityName city = new CityName
            {
                Cityyy = CityEntry.Text,
            };

            GetWeatherInfo(city);
        }



        public async void GetWeatherInfo(CityName city)
        {
            var httpClient = new HttpClient();


            var uri = new Uri(string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid=29d06aa8c8cb3a8d341ab8765b124d7c&units=metric", city.Cityyy));

            var result = await httpClient.GetAsync(uri);

            if (result.IsSuccessStatusCode)
            {
                try
                {
                    string contentt = await result.Content.ReadAsStringAsync();
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(contentt);
                    descriptionTxt.Text = weatherInfo.weather[0].description.ToUpper();
                    iconImg.Source = $"w{weatherInfo.weather[0].icon}";
                    cityTxt.Text = weatherInfo.name.ToUpper();
                    temperatureTxt.Text = weatherInfo.main.temp.ToString("0");
                    humidityTxt.Text = $"{weatherInfo.main.humidity}%";
                    pressureTxt.Text = $"{weatherInfo.main.pressure} hpa";
                    windTxt.Text = $"{weatherInfo.wind.speed} m/s";
                    cloudinessTxt.Text = $"{weatherInfo.clouds.all}%";

                    var dt = new DateTime().ToUniversalTime().AddSeconds(weatherInfo.dt);
                    dateTxt.Text = dt.ToString("dddd, MMM dd").ToUpper();

                    GetForecast(city);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No weather information found", "OK");
            }
        }

        private async void GetForecast(CityName city)
        {
            var httpClient = new HttpClient();
            var uri = new Uri(string.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&appid=29d06aa8c8cb3a8d341ab8765b124d7c&units=metric", city.Cityyy));

            var result = await httpClient.GetAsync(uri);

            if (result.IsSuccessStatusCode)
            {
                try
                {
                    string contentt = await result.Content.ReadAsStringAsync();
                    var forcastInfo = JsonConvert.DeserializeObject<ForecastInfo>(contentt);

                    List<List> allList = new List<List>();

                    foreach (var list in forcastInfo.list)
                    {
                        //var date = DateTime.ParseExact(list.dt_txt, "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);
                        var date = DateTime.Parse(list.dt_txt);

                        if (date > DateTime.Now && date.Hour == 0 && date.Minute == 0 && date.Second == 0)
                            allList.Add(list);
                    }

                    dayOneTxt.Text = DateTime.Parse(allList[0].dt_txt).ToString("dddd");
                    dateOneTxt.Text = DateTime.Parse(allList[0].dt_txt).ToString("dd MMM");
                    iconOneImg.Source = $"w{allList[0].weather[0].icon}";
                    tempOneTxt.Text = allList[0].main.temp.ToString("0");

                    dayTwoTxt.Text = DateTime.Parse(allList[1].dt_txt).ToString("dddd");
                    dateTwoTxt.Text = DateTime.Parse(allList[1].dt_txt).ToString("dd MMM");
                    iconTwoImg.Source = $"w{allList[1].weather[0].icon}";
                    tempTwoTxt.Text = allList[1].main.temp.ToString("0");

                    dayThreeTxt.Text = DateTime.Parse(allList[2].dt_txt).ToString("dddd");
                    dateThreeTxt.Text = DateTime.Parse(allList[2].dt_txt).ToString("dd MMM");
                    iconThreeImg.Source = $"w{allList[2].weather[0].icon}";
                    tempThreeTxt.Text = allList[2].main.temp.ToString("0");

                    dayFourTxt.Text = DateTime.Parse(allList[3].dt_txt).ToString("dddd");
                    dateFourTxt.Text = DateTime.Parse(allList[3].dt_txt).ToString("dd MMM");
                    iconFourImg.Source = $"w{allList[3].weather[0].icon}";
                    tempFourTxt.Text = allList[3].main.temp.ToString("0");

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No forecast information found", "OK");
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}

