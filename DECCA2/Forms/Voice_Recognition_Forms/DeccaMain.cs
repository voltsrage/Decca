using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Diagnostics;


namespace DECCA2
{
    public partial class DeccaMain : Form
    {
        SpeechRecognitionEngine Recog_speech = null;
        SpeechSynthesizer Decca = new SpeechSynthesizer();
        SpeechRecognitionEngine listen = new SpeechRecognitionEngine();

        Random rnd = new Random();

        bool wake = true;

        public DeccaMain()
        {
            InitializeComponent();            

            //Recog_speech = SetLanguage("en-US");

            Recog_speech = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

            Recog_speech.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(MainEvent_Recognized);

            LoadGrammarandCommands();

            Recog_speech.SetInputToDefaultAudioDevice();

            Recog_speech.RecognizeAsync(RecognizeMode.Multiple);

            Decca.SelectVoice("Microsoft Zira Desktop");
            Decca.Rate = 0;

            listen.SetInputToDefaultAudioDevice();
            listen.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"DefaultCommands.txt")))));
            listen.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Listen_SpeechRecognized);
        }

        private void Listen_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;

            if (speech == "wake up")
            {
                listen.RecognizeAsyncCancel();
                Decca.SpeakAsync("Yes,I am here. How can I assist you Trendon");
                Recog_speech.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

        private void LoadGrammarandCommands()
        {
            try
            {
                string MachineName = Environment.UserName;

                string constring = ConfigurationManager.ConnectionStrings["MyDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(constring);
                using (SqlCommand cmd = new SqlCommand("Select * FROM DefaultTable",con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            var Loadcmd = sdr["DefaultCommands"].ToString();
                            Grammar commandgrammar = new Grammar(new GrammarBuilder(new Choices(Loadcmd)));
                            Recog_speech.LoadGrammarAsync(commandgrammar);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SayAsync(string input)
        {
            Decca.SpeakAsync(input);
            wake = false;
        }

        private void UnloadGrammarCommands()
        {
            try
            {
                string MachineName = Environment.UserName;

                string constring = ConfigurationManager.ConnectionStrings["MyDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(constring);
                using (SqlCommand cmd = new SqlCommand("Select * FROM DefaultTable", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            var Loadcmd = sdr["DefaultCommands"].ToString();
                            Grammar commandgrammar = new Grammar(new GrammarBuilder(new Choices(Loadcmd)));
                            Recog_speech.LoadGrammarAsync(commandgrammar);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainEvent_Recognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
            string Name = "Trendon";
            string AI_name = "Decca";
            string speech = e.Result.Text;
            int ranNum;

            if(speech == "Hey Decca")
            {
                wake = true;
            }
            if(speech == "stop talking")
            {
                Decca.SpeakAsyncCancelAll();
                ranNum = rnd.Next(1, 2);
                if (ranNum == 1)
                {
                    SayAsync("Yes sir");
                }
                if (ranNum == 2)
                {
                    SayAsync("I am sorry I will be quiet");
                }
            }   
            
            if (wake == true)
            {
                RoskamWeightEst Roskam = new RoskamWeightEst();
                SystemsEngApproachAircraftDesign systemsEng = new SystemsEngApproachAircraftDesign();
                GAAircraftDesign gA = new GAAircraftDesign();
                switch (speech)
                {
                    case "what's up":
                        //Decca.SpeakAsync("hello" + Name);
                        System.DateTime timenow = System.DateTime.Now;
                        if (timenow.Hour >= 5 && timenow.Hour < 12)
                        {
                            SayAsync("Goodmorning" + Name);
                        }
                        if (timenow.Hour >= 12 && timenow.Hour < 18)
                        {
                            SayAsync("Good afternoon" + Name);
                        }
                        if (timenow.Hour >= 18 && timenow.Hour < 24)
                        {
                            SayAsync("Good evening" + Name);
                        }
                        if (timenow.Hour < 5)
                        {
                            SayAsync("Hello" + Name + ", you are still awake, you should go to sleep, it's getting late.");
                        }
                        break;
                    case "stop listening":
                        SayAsync("If you need me ask");
                        Recog_speech.RecognizeAsyncCancel();
                        listen.RecognizeAsync(RecognizeMode.Multiple);
                        break;
                    case "stop talking":
                        Decca.SpeakAsyncCancelAll();
                        ranNum = rnd.Next(1, 2);
                        if (ranNum == 1)
                        {
                            SayAsync("Yes sir");
                        }
                        if (ranNum == 2)
                        {
                            SayAsync("I am sorry I will be quiet");
                        }
                        break;
                    case "What time is it":
                        System.DateTime now = System.DateTime.Now;
                        string time = now.GetDateTimeFormats('t')[0];
                        SayAsync(time);
                        break;
                    case "what day is it":
                        string dayis;
                        dayis = "Today is, " + System.DateTime.Now.ToString("dddd");
                        SayAsync(dayis);
                        break;
                    case "what is todays date":
                        string date = "Today is, " + System.DateTime.Now.ToString("dd MMM");
                        SayAsync(date);
                        date = "" + "in the year, " + System.DateTime.Now.ToString("yyyy");
                        SayAsync(date);
                        break;
                    case "slow down decca":
                        Decca.Rate -= 1;
                        break;
                    case "speed up decca":
                        Decca.Rate += 1;
                        break;
                    case "crossword":
                        SayAsync("Opening Teachers Corner");
                        Process.Start(@"https://worksheets.theteacherscorner.net/make-your-own/crossword/");
                        break;
                    case "coin market cap":
                        Process.Start(@"https://coinmarketcap.com/");
                        break;
                    case "youtube history":
                        SayAsync("Opening your youtube history");
                        Process.Start(@"https://www.youtube.com/feed/history");
                        break;
                    case "bitcoin chart":
                        Process.Start(@"https://www.tradingview.com/chart/Vu7Cuxnk/-");
                        break;
                    case "Professor Black":
                        SayAsync("Opening Professor Black");
                        Process.Start(@"https://www.youtube.com/channel/UCRteR6kiJwk-bTA5eWPqapg/videos");
                        break;
                    case "Jason Black":
                        SayAsync("Opening Jason Black");
                        Process.Start(@"https://www.youtube.com/channel/UCRteR6kiJwk-bTA5eWPqapg/videos");
                        break;
                    case "Doctor Mumbi":
                        SayAsync("Opening Doctor Mumbi");
                        Process.Start(@"https://www.youtube.com/channel/UCypMLiyzMFoBQFAu346lh7w/videos");
                        break;
                    case "Fox Learn":
                        SayAsync("Opening Fox Learn");
                        Process.Start(@"https://www.youtube.com/channel/UC8inCnD25Es0VLokfmhko5g/playlists");
                        break;
                    case "Money GPS":
                        SayAsync("Opening Money GPS");
                        Process.Start(@"https://www.youtube.com/user/TheMoneyGPS/videos");
                        break;
                    case "L A R Movement":
                        SayAsync("Opening L A R Movement");
                        Process.Start(@"https://www.youtube.com/user/larmovement/videos");
                        break;
                    case "Quizlet":
                        SayAsync("Opening Quizlet");
                        Process.Start(@"https://quizlet.com/latest");
                        break;
                    case "Influence Frame":
                        SayAsync("Opening Influence Frame");
                        Process.Start(@"https://www.youtube.com/channel/UCfhb_xrcYP-m2OQPybV-Khw/videos");
                        break;
                    case "Youtube Money Income Profit":
                        SayAsync("Opening Money Income Profit");
                        Process.Start(@"https://www.youtube.com/user/Glendon007/videos");
                        break;
                    case "Money Income Profit":
                        SayAsync("Opening Money Income Profit");
                        Process.Start(@"https://moneyincomeprofit.com/courses/enrolled/473974");
                        break;
                    case "School tests":
                    case "School comments":
                        SayAsync("Opening Lingoseed Teacher Portal");
                        Process.Start(@"https://drive.google.com/drive/folders/1G23irleFIGcCBQf7Y8Oh93xlGEAmI6hK");
                        break;
                    case "library":
                        LibraryOpen();
                        break;
                    case "who are you":
                        SayAsync("I am Decca, you're personal assistant in your creative design endeavors");
                        break;
                    case "what is your name":
                        SayAsync("My name is " + AI_name + ", How can I help you " + Name);
                        break;
                    case "open website reader":
                            Decca.Speak("Opening website reader");
                            DeccaWebSearch deccaWeb = new DeccaWebSearch();
                            deccaWeb.Show();
                            Recog_speech.RecognizeAsyncCancel();
                           // Recog_speech.Dispose();
                            listen.RecognizeAsync(RecognizeMode.Multiple);
                        break;
                    case "open weather":
                        Decca.Speak("Opening weather app");
                        DeccaWeather deccaWeather = new DeccaWeather();
                        deccaWeather.Show();
                        Recog_speech.RecognizeAsyncCancel();
                            //Recog_speech.Dispose();
                            listen.RecognizeAsync(RecognizeMode.Multiple);
                        break;
                    case "open media player":
                        Decca.Speak("Opening media player");
                        DeccaMediaPlayer deccaMediaPlayer = new DeccaMediaPlayer();
                        deccaMediaPlayer.Show();
                        Recog_speech.RecognizeAsyncCancel();
                            //Recog_speech.Dispose();
                            listen.RecognizeAsync(RecognizeMode.Multiple);
                        break;
                    case "close library":
                        killProg("LibraryManagement");
                        break;
                    case "Roskam Design":
                            Decca.Speak("Opening Roskam Design");
                            Roskam.Show();
                            Recog_speech.RecognizeAsyncCancel();
                            //Recog_speech.Dispose();
                            listen.RecognizeAsync(RecognizeMode.Multiple);
                            break;
                    case "G A Design":
                            Decca.Speak("Opening General Aviation Design");
                            gA.Show(); ;
                            Recog_speech.RecognizeAsyncCancel();
                            //wRecog_speech.Dispose();
                            listen.RecognizeAsync(RecognizeMode.Multiple);
                            break;
                    case "Systems Design":
                            Decca.Speak("Opening Systems Design");
                            systemsEng.Show();
                            Recog_speech.RecognizeAsyncCancel();
                            //Recog_speech.Dispose();
                            listen.RecognizeAsync(RecognizeMode.Multiple);
                            break;
                    case "Calculator":
                            Decca.Speak("Opening Altitude Calculator");
                            DeccaCalculator deccaCalculator = new DeccaCalculator();
                            deccaCalculator.Show(); ;
                            Recog_speech.RecognizeAsyncCancel();
                            //Recog_speech.Dispose();
                            listen.RecognizeAsync(RecognizeMode.Multiple);
                            break;
                    case "See you later Decca":
                    case "Bye Bye":
                    case "Later Decca":
                        System.DateTime timenow1 = System.DateTime.Now;
                        if (timenow1.Hour >= 5 && timenow1.Hour < 12)
                        {
                            Decca.Speak("Have a wonderful morning and spectacular day" + Name);
                        }
                        if (timenow1.Hour >= 12 && timenow1.Hour < 18)
                        {
                            Decca.Speak("Have a splendid afternoon" + Name);
                        }
                        if (timenow1.Hour >= 18 && timenow1.Hour < 24)
                        {
                            Decca.Speak("What a wonderful evening, do not forget to grab dinner" + Name);
                        }
                        if (timenow1.Hour < 5)
                        {
                            Decca.Speak(Name + ", you are still awake, you should go to sleep, it's getting late.");
                        }
                        Application.Exit();
                        break;
                }
            }

            }
            catch
            {
                return;
            }
        }

        private SpeechRecognitionEngine SetLanguage(string preferredCulture)
        {
            foreach (RecognizerInfo config in SpeechRecognitionEngine.InstalledRecognizers())
            {
                if (config.Culture.ToString() == preferredCulture)
                {
                    Recog_speech = new SpeechRecognitionEngine(config);
                    break;
                }
                
                if(Recog_speech == null)
                {
                    MessageBox.Show("The desired culture is not installed on this machine, the " +
                        "speech-engine will use " + SpeechRecognitionEngine.InstalledRecognizers()[0]
                        .Culture.ToString() + " as the default Culture " + preferredCulture + " not " +
                        "found!");
                    Recog_speech = new SpeechRecognitionEngine(SpeechRecognitionEngine.InstalledRecognizers()[0]);
                }
            }
            return Recog_speech;
        }

        public void LibraryOpen()
        {
            Process.Start(@"C:\Users\DECCACLONE\source\repos\LibraryManagement\LibraryManagement\bin\Debug\LibraryManagement.exe");
        }

        public static void killProg(string s)
        {
            System.Diagnostics.Process[] procs = null;

            try
            {
                procs = Process.GetProcessesByName(s);
                Process prog = procs[0];

                if (!prog.HasExited)
                {
                    prog.Kill();
                }
            }
            finally
            {
                if (procs != null)
                {
                    foreach (Process p in procs)
                    {
                        p.Dispose();
                    }
                }
            }
        }

        private void LeftSideMenuPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LeftSideMenuBtn_Click(object sender, EventArgs e)
        {
            if(LeftSideMenuPanel.Width == 40)
            {
                LeftSideMenuPanel.Width = 150;
                Title_lbl.Text = "Personal Assistant";
            }
            else
            {
                LeftSideMenuPanel.Width = 40;
                Title_lbl.Text = "PA";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LeftSideMenuPanel.Width = 40;
            RightSideMenuPanel.Width = 40;
            Title_lbl.Text = "PA";
            Debug_LiveText.SelectionIndent += 10;
        }

        private void RightSideMenuBtn_Click(object sender, EventArgs e)
        {
            if (RightSideMenuPanel.Width == 40)
            {
                RightSideMenuPanel.Width = 150;
            }
            else
            {
                RightSideMenuPanel.Width = 40;
            }
        }

        private void TopPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
