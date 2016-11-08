﻿using System;
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
            var forecastHttp = new HttpClient();
            var forecastResponse = await forecastHttp.GetAsync(String.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&units=imperial&appID=156ce1f2ff11af0169f79b186098b7a6", cityName));
            var forecastResult = await forecastResponse.Content.ReadAsStringAsync();
            var forecastSerializer = new DataContractJsonSerializer(typeof(RootObject));

            var forecastMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(forecastResult));
            var forecastData = (RootObject)forecastSerializer.ReadObject(forecastMemoryStream);

            return forecastData;

        }


        //public async static Task<RootObject> GetDayWeather(string cityName)
        //{
        //    var dailyHttp = new HttpClient();
        //    var dailyResponse = await dailyHttp.GetAsync(String.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=imperial&appID=156ce1f2ff11af0169f79b186098b7a6", cityName));
        //    var dailyResult = await dailyResponse.Content.ReadAsStringAsync();
        //    var dailySerializer = new DataContractJsonSerializer(typeof(RootObject));
        //    var dailyMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(dailyResult));
        //    var dailyData = (RootObject)dailySerializer.ReadObject(dailyMemoryStream);

        //    return dailyData;
        //}
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


    public class FullDayWeather
    {
        public RootObject DayWeather { get; set; }
        public int Day { get; set; }
            
        //populate controls
        public BitmapImage IconSource { get; set; }
        public string Date { get; set; }
        public string Temp { get; set; }
        public string Description { get; set; }
        public string Humidity { get; set; }
        public string Pressure { get; set; }
        public string WindSpeed { get; set; }
        public string WindDirection { get; set; }


        public FullDayWeather(int day, RootObject weather)
        {
            this.Day = day;
            this.DayWeather = weather;
            switch(day) 
            {
                case 0:
                    this.Date = DateTime.Parse(weather.list[0].dt_txt).ToString("ddd");
                    this.Temp = String.Format("{0}°F ", Math.Round(weather.list[0].main.temp));
                    this.Description = weather.list[0].weather[0].description;
                    this.Humidity = weather.list[0].main.humidity.ToString();
                    this.Pressure = weather.list[0].main.pressure.ToString();
                    this.WindSpeed = weather.list[0].wind.speed.ToString();
                    this.WindDirection = weather.list[0].wind.deg.ToString();
                    this.IconSource = new BitmapImage(new Uri(String.Format("http://openweathermap.org/img/w/{0}.png", weather.list[0].weather[0].icon)));         
                    break;
                case 1:
                    this.Date = DateTime.Parse(weather.list[8].dt_txt).ToString("ddd");
                    this.Temp = String.Format("{0}°F ", Math.Round(weather.list[8].main.temp));
                    this.Description = weather.list[8].weather[0].description;
                    this.Humidity = weather.list[8].main.humidity.ToString();
                    this.Pressure = weather.list[8].main.pressure.ToString();
                    this.WindSpeed = weather.list[8].wind.speed.ToString();
                    this.WindDirection = weather.list[8].wind.deg.ToString();
                    this.IconSource = new BitmapImage(new Uri(String.Format("http://openweathermap.org/img/w/{0}.png", weather.list[8].weather[0].icon)));
                    break;
                case 2:
                    this.Date = DateTime.Parse(weather.list[16].dt_txt).ToString("ddd");
                    this.Temp = String.Format("{0}°F ", Math.Round(weather.list[16].main.temp));
                    this.Description = weather.list[16].weather[0].description;
                    this.Humidity = weather.list[16].main.humidity.ToString();
                    this.Pressure = weather.list[16].main.pressure.ToString();
                    this.WindSpeed = weather.list[16].wind.speed.ToString();
                    this.WindDirection = weather.list[16].wind.deg.ToString();
                    this.IconSource = new BitmapImage(new Uri(String.Format("http://openweathermap.org/img/w/{0}.png", weather.list[16].weather[0].icon)));
                    break;
                case 3:
                    this.Date = DateTime.Parse(weather.list[24].dt_txt).ToString("ddd");
                    this.Temp = String.Format("{0}°F ", Math.Round(weather.list[24].main.temp));
                    this.Description = weather.list[24].weather[0].description;
                    this.Humidity = weather.list[24].main.humidity.ToString();
                    this.Pressure = weather.list[24].main.pressure.ToString();
                    this.WindSpeed = weather.list[24].wind.speed.ToString();
                    this.WindDirection = weather.list[24].wind.deg.ToString();
                    this.IconSource = new BitmapImage(new Uri(String.Format("http://openweathermap.org/img/w/{0}.png", weather.list[24].weather[0].icon)));
                    break;
                case 4:
                    this.Date = DateTime.Parse(weather.list[32].dt_txt).ToString("ddd");
                    this.Temp = String.Format("{0}°F ", Math.Round(weather.list[32].main.temp));
                    this.Description = weather.list[32].weather[0].description;
                    this.Humidity = weather.list[32].main.humidity.ToString();
                    this.Pressure = weather.list[32].main.pressure.ToString();
                    this.WindSpeed = weather.list[32].wind.speed.ToString();
                    this.WindDirection = weather.list[32].wind.deg.ToString();
                    this.IconSource = new BitmapImage(new Uri(String.Format("http://openweathermap.org/img/w/{0}.png", weather.list[32].weather[0].icon)));
                    break;
                

              }
            
                
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
