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
using Windows.UI.Xaml.Media.Imaging;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherAppComplete
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        

        public MainPage()
        {
            this.InitializeComponent();
        }


        private async void Search_Weather(object sender, RoutedEventArgs e)
        {
            string cityName = searchBox.Text;
            textBlockCity.Text = cityName;
            RootObject currentWeather = await Proxy.GetWeather(cityName);
            FiveDayForecast Forecast = new FiveDayForecast(currentWeather);


            ////Day0
            icon0.Source = Forecast.fiveDayForecastArr[0].IconSource;
            date0.Text = Forecast.fiveDayForecastArr[0].Date;
            maxTemp0.Text = Forecast.fiveDayForecastArr[0].Temp;

            ////Day1
            icon1.Source = Forecast.fiveDayForecastArr[1].IconSource;
            date1.Text = Forecast.fiveDayForecastArr[1].Date;
            maxTemp1.Text = Forecast.fiveDayForecastArr[1].Temp;


            ////Day2
            icon2.Source = Forecast.fiveDayForecastArr[2].IconSource;
            date2.Text = Forecast.fiveDayForecastArr[2].Date;
            maxTemp2.Text = Forecast.fiveDayForecastArr[2].Temp;


            ////Day3
            icon3.Source = Forecast.fiveDayForecastArr[3].IconSource;
            date3.Text = Forecast.fiveDayForecastArr[3].Date;
            maxTemp3.Text = Forecast.fiveDayForecastArr[3].Temp;

            ////Day4
            icon4.Source = Forecast.fiveDayForecastArr[4].IconSource;
            date4.Text = Forecast.fiveDayForecastArr[4].Date;
            maxTemp4.Text = Forecast.fiveDayForecastArr[4].Temp;  
        }
    }
}
