using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using mshtml;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;
using HtmlAgilityPack;

namespace DECCA2
{
    public partial class DeccaWebSearch : Form
    {
        SpeechRecognitionEngine Recog_speech = null;
        SpeechSynthesizer Decca = new SpeechSynthesizer();

        Scraper scraper;

        string[] ArrayWebSearchCommands;
        string[] ArrayWebKeywordSearch;        

        public string Qevent;        

        public DeccaWebSearch()
        {
            InitializeComponent();

            scraper = new Scraper();

            try
            {
                
                //Recog_speech = SetLanguage("en-US");

                Recog_speech = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
                Recog_speech.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_Recognized);
                Recog_speech.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(WebSearch_Recognized);
               
                LoadGrammarandCommands();

                Recog_speech.SetInputToDefaultAudioDevice();

                Recog_speech.RecognizeAsync(RecognizeMode.Multiple);

                Decca.SelectVoice("Microsoft Zira Desktop");
                Decca.Rate = 0;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void engine_Recognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;

            switch (speech)
            {
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
                case "search":
                    SearchBtn.PerformClick();
                    break;
                case "pause":
                        PauseBtn.PerformClick();
                    break;
                case "resume":
                    PauseBtn.PerformClick();
                    break;
                case "back":
                    BackBtn.PerformClick();
                    break;
                case "next":
                    ForwardBtn.PerformClick();
                    break;
                case "stop":
                    StopBtn.PerformClick();
                    break;
                case "close website search":
                    Decca.Speak("Closing Website Reader");
                    CloseBtn.PerformClick();
                    break;
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
                default:
                    break;
            }
            if (speech == "find")
            {
                string Speech = e.Result.Text;
                Qevent = Speech;
                Speech = string.Empty;
                Decca.SpeakAsync("what do you want to search");
                speech = string.Empty;

                Process_OutputDataReceived();
            }                 
        }

        private void Process_OutputDataReceived()
        {
            var script = @"-u C:\Users\DECCACLONE\source\repos\Voice\Voice\Voice.py";
            var filename = @"C:\Users\DECCACLONE\AppData\Local\Programs\Python\Python37-32\python.exe";

            Process proc = new Process();
            proc.StartInfo = new ProcessStartInfo(filename, script)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            proc.Start();

            keywordstxt.Text = proc.StandardOutput.ReadToEnd();
            SearchBtn.PerformClick();
            //webbrowser1.Dock = DockStyle.Fill;
            //webbrowser1.Dock = DockStyle.None;
            //webbrowser1.Anchor = AnchorStyles.None;
            //webbrowser1.MaximumSize = new Size(800, 640);
            //Process.Start("https://www.google.com/search?q=" + keywordstxt.Text);
        }

        public string results;        

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
                    ArrayWebSearchCommands = File.ReadAllLines(Environment.CurrentDirectory + "\\WebSearchCommands.txt");
                    ArrayWebKeywordSearch = File.ReadAllLines(Environment.CurrentDirectory + "\\WebKeySearchCommands.txt");
                    Grammar webSearchcommandgrammar = new Grammar(new GrammarBuilder(new Choices(ArrayWebSearchCommands)));
                    Recog_speech.LoadGrammarAsync(webSearchcommandgrammar); 
                }
                catch
                {
                    Decca.SpeakAsync("I've detected an invalid entry in your web commands, possibly a blank line. " +
                    "Web commmands will cease to work until the problem is fixed.");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
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
                        keywordstxt.Text = ArrayWebKeywordSearch[i];
                        SearchBtn.PerformClick();
                    }
                    i += 1;
                }
            }
            catch(Exception ex)
            {
                i += 1;
                Decca.SpeakAsync("Please check the " + speech + " web command on line " + i + ". It appears to be missing a proper response or web key words. "+ ex.Message);
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

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Recog_speech.RecognizeAsyncCancel();
            Recog_speech.Dispose();
            this.Dispose();
            this.Close();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            string urladdress = converttxt.Text;   
            
            webbrowser1.Navigate("https://www.google.com/search?q=" + keywordstxt.Text);

            //scraper.ScrapeData("https://www.google.com/search?q=" + keywordstxt.Text);
        }

        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //WebBrowser webbrowser1 = sender as WebBrowser;
        }

        private void CopyScreenBtn_Click(object sender, EventArgs e)
        {
            IHTMLDocument2 hTMLDocument = webbrowser1.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = hTMLDocument.selection;
            IHTMLTxtRange range = currentSelection.createRange() as IHTMLTxtRange;
            if (currentSelection != null)
            {
                if (range != null)
                {
                    converttxt.Text = range.text;
                }
            }
        }

        private void GetResult()
        {            
            convertitems.Items.Clear();
            string urladdress = keywordstxt.Text;
            WebClient webc = new WebClient();
            webc.Encoding = Encoding.UTF8;
            string page = webc.DownloadString("https://www.bing.com/search?q=" + urladdress);
            string news = "<div> class=\"b_snippet\">(.*?)</div>";
            news = "<div> class=\"b_attribution\">(.*?)</div>";
            news = "<p>(.*?)</p>";
            foreach (Match match in Regex.Matches(page, news))
                convertitems.Items.Add(match.Groups[1].Value.Replace("<strong>", " ").Replace("&", " ").Replace("#", " "));
            foreach (string readitems in convertitems.Items)
            {
                Decca.SpeakAsync(readitems);
            }            
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            CopyScreenBtn.PerformClick();            
            StartBtn.Enabled = false;
            PauseBtn.Enabled = true;
            Decca.SpeakAsync(converttxt.Text);
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            if (Decca.State == SynthesizerState.Speaking)
            {
                Decca.Pause();
                PauseBtn.Text = "Resume";
            }
            else
            {
                if (Decca.State == SynthesizerState.Paused)
                {
                    Decca.Resume();
                    PauseBtn.Text = "Pause";
                }
            }
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            if (Decca.State == SynthesizerState.Paused)
            {
                Decca.Resume();                
            }
            Decca.SpeakAsyncCancelAll();

            StopBtn.Text = "Stop";
            StartBtn.Enabled = true;
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            webbrowser1.GoBack();
        }

        private void ForwardBtn_Click(object sender, EventArgs e)
        {
            webbrowser1.GoForward();
        }

        private void DeccaWebSearch_Load(object sender, EventArgs e)
        {
                                  
        }

        private void LinkClickBtn_Click(object sender, EventArgs e)
        {
            foreach(HtmlElement elem in webbrowser1.Document.GetElementsByTagName("h3"))
            {
                if(elem.GetAttribute("class") == "LC201b")
                {
                    ResultsCbx.Items.Add(elem);
                }
            }
        }
    }
}
