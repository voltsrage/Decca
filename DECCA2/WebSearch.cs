using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows.Forms;
using System.IO;

namespace DECCA2
{
    public partial class WebSearch : Form
    {
        SpeechRecognitionEngine Recog_speech = null;
        SpeechSynthesizer Decca = new SpeechSynthesizer();

        string[] ArrayWebSearchCommands;
        string[] ArrayWebKeywordSearch;

        public WebSearch()
        {            
            InitializeComponent();
            /*
            try
            {
                InitializeComponent();
                Recog_speech = SetLanguage("en-US");
                Recog_speech.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_Recognized);
                Recog_speech.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(WebSearch_Recognized);

                LoadGrammarandCommands();

                Recog_speech.SetInputToDefaultAudioDevice();

                Recog_speech.RecognizeAsync(RecognizeMode.Multiple);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        private void txt_Back_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void txt_Forward_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string url = txt_Search.Text;
            webBrowser1.Navigate("https://www.google.com/search?q=" + url);
        }

        private void WebSearch_Recognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;

            int i = 0;
            try
            {
                foreach (string line in ArrayWebSearchCommands)
                {
                    if (line == speech)
                    {
                        Decca.SpeakAsync(ArrayWebKeywordSearch[i]);
                        //txt_Search.Text = ArrayWebKeywordSearch[i];
                        //btn_Search.PerformClick();
                    }
                    i += 1;
                }
            }
            catch (Exception ex)
            {
                i += 1;
                Decca.SpeakAsync("Please check the " + speech + " web command on line " + i + ". It appears to be missing a proper response or web key words. " + ex.Message);
            }
        }

        private void engine_Recognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;

            switch (speech)
            {
                /*
                case "start reading":
                    CopyScreenBtn.PerformClick();
                    StartBtn.PerformClick();
                    break;
                case "read the result":
                case "read the resuls":
                case "whats the result":
                case "whats the results":
                    GetResult();
                    break;
                    */
                case "search":
                    btn_Search.PerformClick();
                    break;
                    /*
                case "pause":
                    PauseBtn.PerformClick();
                    break;
                case "resume":
                    PauseBtn.PerformClick();
                    break;
                case "stop":
                    StopBtn.PerformClick();
                    break;
                case "close website search":
                    CloseBtn.PerformClick();
                    break;
                    */
                case "hide website reader":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Minimized;
                    TopMost = false;
                    break;
                case "show website reader":
                case "show website reader again":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Normal;
                    TopMost = true;
                    break;
            }
        }


        private void LoadGrammarandCommands()
        {
            try
            {
                Choices texts = new Choices();
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + @"\DefaultCommands.txt");

                texts.Add(lines);
                Grammar wordslist = new Grammar(new GrammarBuilder(texts));
                Recog_speech.LoadGrammar(wordslist);
                try
                {
                    ArrayWebSearchCommands = File.ReadAllLines(Environment.CurrentDirectory + @"\WebSearchCommands.txt");
                    ArrayWebKeywordSearch = File.ReadAllLines(Environment.CurrentDirectory + @"\WebKeySearchCommands.txt");
                    Grammar webSearchcommandgrammar = new Grammar(new GrammarBuilder(new Choices(ArrayWebSearchCommands)));
                    Recog_speech.LoadGrammarAsync(webSearchcommandgrammar);
                }
                catch
                {
                    Decca.SpeakAsync("I've detected an invalid entry in your web commands, possibly a blank line. " +
                    "Web commmands will cease to work until the problem is fixed.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

                if (Recog_speech == null)
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
    }
}
