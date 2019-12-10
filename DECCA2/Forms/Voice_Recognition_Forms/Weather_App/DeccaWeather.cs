using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DECCA2
{
    public partial class DeccaWeather : Form
    {
        const string APPID = "6c3a7d84de2b2f640d6de828b3ae4244";
        string city = "Hsinchu";

        SpeechRecognitionEngine speechRecognition = null;
        SpeechSynthesizer Decca = new SpeechSynthesizer();

        public DeccaWeather()
        {
            InitializeComponent();

            try
            {
                //speechRecognition = createSpeechEngine("en-US");

                speechRecognition = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

                speechRecognition.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);

                LoadGrammarAndCommands();

                speechRecognition.SetInputToDefaultAudioDevice();

                speechRecognition.BabbleTimeout = TimeSpan.FromSeconds(3.0);

                speechRecognition.RecognizeAsync(RecognizeMode.Multiple);

                Decca.SelectVoice("Microsoft Zira Desktop");
                Decca.Rate = 0;

                GetWeather(city);
                GetForeCast(city);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void GetWeather(string city)
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q=" + city +
                    "&APPID="+APPID+"&units=metric");

                var json = web.DownloadString(url);

                var result = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                WeatherInfo.root output = result;

                Sunrise_lbl.Text = string.Format("Sunrise  {0}", getDate(output.sys.sunrise).ToString("H:mm:ss"));
                Sunset_lbl.Text = string.Format("Sunset {0}", getDate(output.sys.sunset).ToString("H:mm:ss"));

                lbl_Location.Text = string.Format("Longitude : {0} Latitude : {1}", output.coord.lon,output.coord.lat);
                lbl_Cityname.Text = string.Format("{0}", output.name);
                lbl_Country_Code.Text = string.Format("{0}", output.sys.country);
                lbl_Temperature.Text = string.Format("{0} \u00B0" +"C", output.main.temp);
                lbl_ConditionsMain.Text = string.Format("{0}", output.weather[0].main);
                lbl_DescriptionMain.Text = string.Format("{0}", output.weather[0].description);
                lbl_HumidityMain.Text = string.Format("{0} %", output.main.humidity);
                lbl_PressureMain.Text = string.Format("{0} millibars", output.main.pressure);
                lbl_WindSpeedMain.Text = string.Format("{0} km/h", output.wind.speed);
                pbox_MainPic.Image = setIcon(output.weather[0].icon);
            }
                
        }

        private void LoadGrammarAndCommands()
        {
            try
            {
                Choices texts = new Choices();
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\WeatherCommands.txt");

                texts.Add(lines);
                Grammar wordslist = new Grammar(new GrammarBuilder(texts));
                speechRecognition.LoadGrammar(wordslist);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = (e.Result.Text);
            switch (speech)
            {
                case "hello decca":
                    Decca.SpeakAsync("hello Trendon");
                    break;
                case "close weather" :
                    this.Dispose();
                    speechRecognition.RecognizeAsyncCancel();                    
                    speechRecognition.Dispose();
                    this.Close();
                    break;
                case "what is todays temperature":
                case "what is the temperature":
                case "whats the temperature":
                    Decca.SpeakAsync("It is " +lbl_Temperature.Text);
                    break;
                case "What is today like":
                case "what is todays condition":
                case "what is the condition today":
                case "what is the weather condition":
                    Decca.SpeakAsync("Temperature is " +lbl_Temperature .Text);
                    Decca.SpeakAsync("Humidity is " +lbl_HumidityMain.Text);
                    Decca.SpeakAsync("Condition " + lbl_DescriptionMain.Text );
                    Decca.SpeakAsync("Pressure is " +lbl_PressureMain.Text);
                    Decca.SpeakAsync("WInd Speed is " + lbl_WindSpeedMain.Text);
                    break;
            }
        }

        private DateTime getDate(double milliseconds)
        {
            DateTime day = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(milliseconds).ToLocalTime();

            return day;
        }

        private Image setIcon(string iconID)
        {
            string url = "http://openweathermap.org/img/w/"+iconID+".png";
            var request = WebRequest.Create(url);
            using (var response = request.GetResponse())
            {
                using (var weatherIcon = response.GetResponseStream())
                {
                    Image weatherImg = Bitmap.FromStream(weatherIcon);
                    return weatherImg;
                }
            }
        }

        private void GetForeCast(string city)
        {
            string url = string.Format("https://api.openweathermap.org/data/2.5/forecast?q="+city+"&APPID="+APPID+"&units=metric");
                  
            using (WebClient web = new WebClient())
            {
                var json = web.DownloadString(url);
                var Object = JsonConvert.DeserializeObject<WeatherForecast>(json);

                WeatherForecast forecast = Object;

                lbl_Days.Text = string.Format("{0}", getDate(forecast.list[1].dt).DayOfWeek);
                lbl_Conditions.Text = string.Format("{0}", forecast.list[1].weather[0].main);
                lbl_Description.Text = string.Format("{0}", forecast.list[1].weather[0].description);
                lbl_Temp2.Text = string.Format("{0} \u00B0" +"C", forecast.list[1].main.temp);
                lbl_WindSpeed.Text = string.Format("{0} km/h", forecast.list[1].wind.speed);
                pBox_WeatherPic.Image = setIcon(forecast.list[1].weather[0].icon);

                lbl_Days2.Text = string.Format("{0}", getDate(forecast.list[9].dt).DayOfWeek);
                lbl_Conditions2.Text = string.Format("{0}", forecast.list[9].weather[0].main);
                lbl_Description2.Text = string.Format("{0}", forecast.list[9].weather[0].description);
                lbl_Temp3.Text = string.Format("{0} \u00B0" + "C", forecast.list[9].main.temp);
                lbl_WindSpeed2.Text = string.Format("{0} km/h", forecast.list[9].wind.speed);
                pBox_WeatherPic2.Image = setIcon(forecast.list[9].weather[0].icon);

                Daylbl3.Text = string.Format("{0}", getDate(forecast.list[17].dt).DayOfWeek);
                Condlbl3.Text = string.Format("{0}", forecast.list[17].weather[0].main);
                Desclbl3.Text = string.Format("{0}", forecast.list[17].weather[0].description);
                Templbl3.Text = string.Format("{0} \u00B0" + "C", forecast.list[17].main.temp);
                Speedlbl3.Text = string.Format("{0} km/h", forecast.list[17].wind.speed);
                pBox_WeatherPic3.Image = setIcon(forecast.list[17].weather[0].icon);

                Daylbl4.Text = string.Format("{0}", getDate(forecast.list[25].dt).DayOfWeek);
                Condlbl4.Text = string.Format("{0}", forecast.list[25].weather[0].main);
                Desclbl4.Text = string.Format("{0}", forecast.list[25].weather[0].description);
                Templbl4.Text = string.Format("{0} \u00B0" + "C", forecast.list[25].main.temp);
                Speedlbl4.Text = string.Format("{0} km/h", forecast.list[25].wind.speed);
                pBox_WeatherPic4.Image = setIcon(forecast.list[25].weather[0].icon);

            }
        }

        private void DeccaWeather_Load(object sender, EventArgs e)
        {

        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if(txt_Search.Text != "")
            {
                GetWeather(txt_Search.Text);
                GetForeCast(txt_Search.Text);
            }
        }

        
    }
}
