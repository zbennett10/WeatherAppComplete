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
        // global variable that causes the name of the searched city to persist throughout multiple pages  
        public static string cityName { get; set; }

        // global variable that determines which day of the week to display
        public static int pageState { get; set; }

        // global variable that determines which unit to fetch from OpenWeatherMap's API
        public static string unit { get; set; }
    
        public MainPage()
        {
            this.InitializeComponent();
           
        }

        //sets focus off of the textbox where our search is conducted
        private void MainPage_OnLoad(object sender, RoutedEventArgs e)
        {
            
            buttonSearch.Focus(FocusState.Programmatic);
        }

        //causes placeholder text to disappear when user clicks inside of text box in order to search
        private void OnFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = "";
            searchBox.GotFocus -= OnFocus;
        }

        //displays day of the week buttons
        private void Button_Displayer()
        {
            buttonDayZero.Visibility = Visibility.Visible;
            buttonDayOne.Visibility = Visibility.Visible;
            buttonDayTwo.Visibility = Visibility.Visible;
            buttonDayThree.Visibility = Visibility.Visible;
            buttonDayFour.Visibility = Visibility.Visible;
        }

        //populates xaml images with corresponding weather icon 
        private void Icon_Populator(FiveDayForecast forecast, int[] days)
        {
            icon0.Source = forecast.fiveDayForecastArr[days[0]].IconSource;
            icon1.Source = forecast.fiveDayForecastArr[days[1]].IconSource;
            icon2.Source = forecast.fiveDayForecastArr[days[2]].IconSource;
            icon3.Source = forecast.fiveDayForecastArr[days[3]].IconSource;
            icon4.Source = forecast.fiveDayForecastArr[days[4]].IconSource;
        }

        //populates the days of the week text
        private void Date_Populator(FiveDayForecast forecast, int[] days)
        {
            date0.Text = forecast.fiveDayForecastArr[days[0]].Date;
            date1.Text = forecast.fiveDayForecastArr[days[1]].Date;
            date2.Text = forecast.fiveDayForecastArr[days[2]].Date;
            date3.Text = forecast.fiveDayForecastArr[days[3]].Date;
            date4.Text = forecast.fiveDayForecastArr[days[4]].Date;
        }

        //populates the temperature text for each day
        private void Temp_Populator(FiveDayForecast forecast, int[] days)
        {
            maxTemp0.Text = forecast.fiveDayForecastArr[days[0]].Temp;
            maxTemp1.Text = forecast.fiveDayForecastArr[days[1]].Temp;
            maxTemp2.Text = forecast.fiveDayForecastArr[days[2]].Temp;
            maxTemp3.Text = forecast.fiveDayForecastArr[days[3]].Temp;
            maxTemp4.Text = forecast.fiveDayForecastArr[days[4]].Temp;
        }

        //controls the flow of data population methods
        public delegate void Control_Population_Handler(FiveDayForecast forecast, int[] days);

        //activates proxy and populates xaml controls with data from proxy with OpenWeatherMap
        private async void Search_Weather(object sender, RoutedEventArgs e)
        {
            if ((bool)Imperial.IsChecked) unit = "imperial";
            

            cityName = searchBox.Text;
            textBlockCity.Text = cityName;
            RootObject currentWeather = await Proxy.GetWeather(cityName, unit);
            FiveDayForecast Forecast = new FiveDayForecast(currentWeather);
            int[] days = { 0, 1, 2, 3, 4 };

            Button_Displayer();
            Control_Population_Handler Icon_Control = new Control_Population_Handler(Icon_Populator);
            Control_Population_Handler Date_Control = new Control_Population_Handler(Date_Populator);
            Control_Population_Handler Temp_Control = new Control_Population_Handler(Temp_Populator);
            Icon_Control += Date_Control + Temp_Control;           
            Icon_Control(Forecast, days);
        }

        //these methods determine which day of the week to display based upon the button pressed by the user
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
