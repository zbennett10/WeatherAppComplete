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
using System.Globalization;

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


        //capitalizes first letter of every word in the weather description
        public static string ToTitleCase(string str)
        {
          var wordArr = str.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
          for (var i = 0; i < wordArr.Length; i++)
           {
               var word = wordArr[i];
               wordArr[i] = word.Substring(0, 1).ToUpper() + word.Substring(1);
           }

           return string.Join(" ", wordArr);
         }
        

        //Loads weather data of specific day depending on current pageState value
        private async void PageLoaded(object sender, RoutedEventArgs e)
        {
            string city = MainPage.cityName;        
            RootObject dayWeather = await Proxy.GetWeather(city);
            FullDayWeather day = new FullDayWeather(MainPage.pageState, dayWeather);

            dailyWeatherDesc.Text = String.Format("'{0}'",ToTitleCase(day.Description));
            dailyIcon.Source = day.IconSource;
            dailyDayName.Text = day.Date;
            dailyHumidity.Text = day.Humidity;
            dailyPressure.Text = day.Pressure;
            dailyMaxTemp.Text = day.Temp;
            ToolTipService.SetToolTip(dailyMaxTemp, day.Celsius); 
            dailyWindDir.Text = day.WindDirection;
            dailyWindSpeed.Text = day.WindSpeed;
            dailyClouds.Text = day.Clouds;
        }
    
        //navigation funtionality to allow user to return to main page
        private void BackToMain_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage),null);           
        }
    }
}
