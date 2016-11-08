using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;

namespace WeatherAppComplete
{
    class Proxy
    {
        public async static Task<RootObject> GetWeather(string cityName)
        {
            var http = new HttpClient();
            var response = await http.GetAsync(String.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&units=imperial&appID=156ce1f2ff11af0169f79b186098b7a6", cityName));
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(memoryStream);

            return data;

        }
    }



    public class FiveDayForecast 
    {

        public RootObject currentWeather { get; set; }
        public DailyForecast[] fiveDayForecastArr { get; set; }
  
        public FiveDayForecast(RootObject weather)
        {
            this.currentWeather = weather;
            this.fiveDayForecastArr = FiveDayForecaster();
        }

        public DailyForecast[] FiveDayForecaster()
        {
            
            DailyForecast dayOne = new DailyForecast(this.currentWeather.list[0].weather[0].icon, this.currentWeather.list[0].dt_txt, this.currentWeather.list[0].main.temp);
            DailyForecast dayTwo = new DailyForecast(this.currentWeather.list[8].weather[0].icon, this.currentWeather.list[8].dt_txt, this.currentWeather.list[8].main.temp);
            DailyForecast dayThree = new DailyForecast(this.currentWeather.list[16].weather[0].icon, this.currentWeather.list[16].dt_txt, this.currentWeather.list[16].main.temp);
            DailyForecast dayFour = new DailyForecast(this.currentWeather.list[24].weather[0].icon, this.currentWeather.list[24].dt_txt, this.currentWeather.list[24].main.temp);
            DailyForecast dayFive = new DailyForecast(this.currentWeather.list[32].weather[0].icon, this.currentWeather.list[32].dt_txt, this.currentWeather.list[32].main.temp);
            DailyForecast[] fiveDayForecast = { dayOne, dayTwo, dayThree, dayFour, dayFive };

            return fiveDayForecast;
        }

    }


    public class DailyForecast
    {
        public BitmapImage IconSource { get; set; }
        public string Date { get; set; }
        public string Temp { get; set; }    

        public DailyForecast(string icon, string date, double temp)
        {
            this.IconSource = new BitmapImage(new Uri(String.Format("http://openweathermap.org/img/w/{0}.png", icon)));
            this.Date = DateTime.Parse(date).ToString("ddd");
            this.Temp = String.Format("{0}°F ", Math.Round(temp).ToString());    
        }

    }






    /*[DataContract]
    public class Coord
    {
        [DataMember]
        public double lon { get; set; }
        [DataMember]
        public double lat { get; set; }
    }

    [DataContract]
    public class Sys
    {
        [DataMember]
        public int population { get; set; }
    }*/

    [DataContract]
    public class City
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string name { get; set; }

        /*[DataMember]
        public Coord coord { get; set; }*/

        [DataMember]
        public string country { get; set; }

        [DataMember]
        public int population { get; set; }

        /*[DataMember]
        public Sys sys { get; set; }*/
    }

    [DataContract]
    public class Main
    {
        [DataMember]
        public double temp { get; set; }

        [DataMember]
        public double temp_min { get; set; }

        [DataMember]
        public double temp_max { get; set; }

        [DataMember]
        public double pressure { get; set; }

        [DataMember]
        public double sea_level { get; set; }

        [DataMember]
        public double grnd_level { get; set; }

        [DataMember]
        public int humidity { get; set; }

        [DataMember]
        public double temp_kf { get; set; }
    }

    [DataContract]
    public class Weather
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string main { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string icon { get; set; }
    }

    [DataContract]
    public class Clouds
    {
        [DataMember]
        public int all { get; set; }
    }

    [DataContract]
    public class Wind
    {
        [DataMember]
        public double speed { get; set; }

        [DataMember]
        public double deg { get; set; }
    }

    [DataContract]
    public class Rain
    {

        [DataMember]
        public double __invalid_name__3h { get; set; }
    }

    /*[DataContract]
    public class Sys2
    {
        [DataMember]
        public string pod { get; set; }
    }*/

    [DataContract]
    public class List
    {
        [DataMember]
        public int dt { get; set; }

        [DataMember]
        public Main main { get; set; }

        [DataMember]
        public List<Weather> weather { get; set; }

        [DataMember]
        public Clouds clouds { get; set; }

        [DataMember]
        public Wind wind { get; set; }

        [DataMember]
        public Rain rain { get; set; }

        /*[DataMember]
        public Sys2 sys { get; set; }*/

        [DataMember]
        public string dt_txt { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public City city { get; set; }

        [DataMember]
        public string cod { get; set; }

        [DataMember]
        public double message { get; set; }

        [DataMember]
        public int cnt { get; set; }

        [DataMember]
        public List<List> list { get; set; }
    }



}
