using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherAppComplete
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DailyWeather : Page
    {
        public DailyWeather()
        {
            this.InitializeComponent();         
        }

        private async void PageLoaded(object sender, RoutedEventArgs e)
        {
            string city = MainPage.cityName;
            //dailyCityName.Text = city;
            RootObject dayWeather = await Proxy.GetWeather(city);

            FullDayWeather day = new FullDayWeather(MainPage.pageState, dayWeather);
            dailyWeatherDesc.Text = day.Description;
            dailyIcon.Source = day.IconSource;
            dailyDayName.Text = day.Date;
            dailyHumidity.Text = day.Humidity;
            dailyPressure.Text = day.Pressure;
            dailyMaxTemp.Text = day.Temp;
            dailyWindDir.Text = day.WindDirection;
            dailyWindSpeed.Text = day.WindSpeed;
        }

        private void BackToMain_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage),null);           
        }
    }
}
