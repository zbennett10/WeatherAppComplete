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

        
        //matches description of weather to a specific background photo
        Dictionary<string, string> ImageUrls = new Dictionary<string, string>()
        {
            {"clear", "ms-appx:///Assets/clearsky.jpeg" },
            {"few", "ms-appx:///Assets/brokenfewscatteredclouds.jpg"},
            {"broken", "ms-appx:///Assets/brokenfewscatteredclouds.jpg" },
            {"scattered", "ms-appx:///Assets/brokenfewscatteredclouds.jpg" },
            {"rain", "ms-appx:///Assets/rain.jpg" },
            {"overcast", "ms-appx:///Assets/overcastclouds.jpg" }
        };

        //creates background image that matches with description 
        //of the weather and attaches it to the background of the DailyWeather page's grid
        private void Create_Background(string description)
        {
            DailyWeatherGrid.Background = new ImageBrush
            {
                Stretch = Stretch.UniformToFill,
                ImageSource = new BitmapImage { UriSource = new Uri(ImageUrls[description]) }
            };
        }

        //global variable that is populated by the current day's weather description
        public static string weatherDescription; 

        //array that contains weather "buzz" words for the Set_Background() method
        //this array also contains the same values as our dictionary
        string[] descriptionTests = { "clear", "few", "scattered", "overcast", "broken", "rain" };

        private void Set_Background()
        {
            //Finds the first matched buzz word from our array that is within the global variable "weatherDescription"
            string result = descriptionTests.FirstOrDefault(x => weatherDescription.Contains(x));

            switch(result)
            {
                case "clear":                   
                    Create_Background("clear");
                    break;
                case "few":                    
                    Create_Background("few");
                    break;
                case "broken":                   
                    Create_Background("broken");
                    break;               
                case "scattered":                  
                    Create_Background("scattered");
                    break;
                case "overcast":                   
                    Create_Background("overcast");
                    break;
                case "rain":                   
                    Create_Background("rain");
                    break;
                default:                   
                    Create_Background("clear");
                    break;
            }       
        }

        //populates controls on this page with weather data
        private void Control_Populator(FullDayWeather day)
        {
            dailyWeatherDesc.Text = String.Format("'{0}'", ToTitleCase(day.Description));
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
        

        //Loads weather data of specific day depending on current pageState value
        private async void PageLoaded(object sender, RoutedEventArgs e)
        {
            string city = MainPage.cityName;        
            RootObject dayWeather = await Proxy.GetWeather(city, MainPage.unit);
            FullDayWeather day = new FullDayWeather(MainPage.pageState, dayWeather);
            weatherDescription = day.Description;
            Set_Background();
            Control_Populator(day);
        }
    
        //navigation funtionality to allow user to return to main page
        private void BackToMain_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage),null);           
        }
    }
}
