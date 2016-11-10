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
        public static string cityName { get; set; }
        public static int pageState { get; set; }
       

        public MainPage()
        {
            this.InitializeComponent();
           
        }

        private void MainPage_OnLoad(object sender, RoutedEventArgs e)
        {
            
            buttonSearch.Focus(FocusState.Programmatic);
        }

        
        private void OnFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = "";
            searchBox.GotFocus -= OnFocus;
        }

        private async void Search_Weather(object sender, RoutedEventArgs e)
        {
            cityName = searchBox.Text;
            textBlockCity.Text = cityName;
            RootObject currentWeather = await Proxy.GetWeather(cityName);
            FiveDayForecast Forecast = new FiveDayForecast(currentWeather);

            ////Day0
            buttonDayZero.Visibility = Visibility.Visible;
            icon0.Source = Forecast.fiveDayForecastArr[0].IconSource;
            date0.Text = Forecast.fiveDayForecastArr[0].Date;
            maxTemp0.Text = Forecast.fiveDayForecastArr[0].Temp;

            ////Day1
            buttonDayOne.Visibility = Visibility.Visible;
            icon1.Source = Forecast.fiveDayForecastArr[1].IconSource;
            date1.Text = Forecast.fiveDayForecastArr[1].Date;
            maxTemp1.Text = Forecast.fiveDayForecastArr[1].Temp;

            ////Day2
            buttonDayTwo.Visibility = Visibility.Visible;
            icon2.Source = Forecast.fiveDayForecastArr[2].IconSource;
            date2.Text = Forecast.fiveDayForecastArr[2].Date;
            maxTemp2.Text = Forecast.fiveDayForecastArr[2].Temp;

            ////Day3
            buttonDayThree.Visibility = Visibility.Visible;
            icon3.Source = Forecast.fiveDayForecastArr[3].IconSource;
            date3.Text = Forecast.fiveDayForecastArr[3].Date;
            maxTemp3.Text = Forecast.fiveDayForecastArr[3].Temp;

            ////Day4
            buttonDayFour.Visibility = Visibility.Visible;
            icon4.Source = Forecast.fiveDayForecastArr[4].IconSource;
            date4.Text = Forecast.fiveDayForecastArr[4].Date;
            maxTemp4.Text = Forecast.fiveDayForecastArr[4].Temp;     
        }

        private void DayZeroWeather(object sender, RoutedEventArgs e)
        {

            pageState = 0;
            Frame.Navigate(typeof(DailyWeather), null);
            
        }

        private void DayOneWeather(object sender, RoutedEventArgs e)
        {
            pageState = 1;
            Frame.Navigate(typeof(DailyWeather), null);
        }

        private void DayTwoWeather(object sender, RoutedEventArgs e)
        {
            pageState = 2;
            Frame.Navigate(typeof(DailyWeather), null);
        }

        private void DayThreeWeather(object sender, RoutedEventArgs e)
        {
            pageState = 3;
            Frame.Navigate(typeof(DailyWeather), null);
        }

        private void DayFourWeather(object sender, RoutedEventArgs e)
        {
            pageState = 4;
            Frame.Navigate(typeof(DailyWeather), null);
        }
    }
}
