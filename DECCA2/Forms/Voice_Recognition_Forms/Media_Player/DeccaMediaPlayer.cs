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
using System.IO;

namespace DECCA2
{
    public partial class DeccaMediaPlayer : Form
    {
        SpeechRecognitionEngine speechRecognition = null;
        SpeechSynthesizer Decca = new SpeechSynthesizer();
        WMPLib.WindowsMediaPlayer WindowsMediaPlayer = new WMPLib.WindowsMediaPlayer();
        public static String userName = Environment.UserName;
        string[] files, paths;

        SpeechRecognitionEngine listen = new SpeechRecognitionEngine();

        int Top;
        int MoveX;
        int MoveY;

        public DeccaMediaPlayer()
        {
            InitializeComponent();
            DeccaPlayer.uiMode = "None";
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

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        

        private void LoadGrammarAndCommands()
        {
            try
            {
                Choices texts = new Choices();
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\MediaPlayerCommands.txt");

                texts.Add(lines);
                Grammar wordslist = new Grammar(new GrammarBuilder(texts));
                speechRecognition.LoadGrammar(wordslist);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = (e.Result.Text);
            switch (speech)
            {
                case "hello":
                    Decca.SpeakAsync("hello");
                    break;
                case "open play list":
                case "add play list":
                    Decca.SpeakAsync("choose, music file from your drives");
                    AddMedia_Btn.PerformClick();
                    break;
                case "minimize":
                case "hide media player":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Minimized;
                    TopMost = false;
                    break;
                case "show media player":
                case "show media player again":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Normal;
                    TopMost = true;
                    break;
                case "play":
                    Play_Btn.PerformClick();
                    break;
                case "fast forward":
                    QuickFFW_Btn.PerformClick();
                    break;
                case "next":
                    FFW_Btn.PerformClick();
                    break;
                case "previous":
                    RWD_Btn.PerformClick();
                    break;
                case "resume":
                    Pause_Btn.PerformClick();
                    break;
                case "pause":
                    Pause_Btn.PerformClick();
                    break;
                case "stop":
                    Stop_Btn.PerformClick();
                    break;
                case "rewind":
                    FRWD_Btn.PerformClick();
                    break;
                case "mute":
                    MuteBtn.PerformClick();
                    break;
                case "full screen":
                    FullScreenBtn.PerformClick();
                    break;
                    /*
                case "volume down":
                    while (VolumeSpeed.Value > 0)
                    {
                        VolumeSpeed.Value -= 10;
                    }                    
                    break;
                case "volume up":
                    while (VolumeSpeed.Value < 100)
                    {
                        VolumeSpeed.Value += 10;
                    }
                    break;
                    */
                case "exit full screen":
                    DeccaPlayer.Focus();
                    DeccaPlayer.fullScreen = false;
                    break;
                case "close media player":
                case "close":
                    Decca.Speak("Closing media player, goodbye.");
                    speechRecognition.RecognizeAsyncCancel();
                    speechRecognition.Dispose();
                    this.Close();
                    break;
            }
        }

        private SpeechRecognitionEngine createSpeechEngine(string preferredCulture)
        {

            foreach (RecognizerInfo config in SpeechRecognitionEngine.InstalledRecognizers())
            {
                if (config.Culture.ToString() == preferredCulture)
                {
                    speechRecognition = new SpeechRecognitionEngine(config);
                    break;
                }

                if (speechRecognition == null)
                {
                    MessageBox.Show("The desired culture is not installed on this machine, the " +
                        "speech-engine will use " + SpeechRecognitionEngine.InstalledRecognizers()[0]
                        .Culture.ToString() + " as the default Culture " + preferredCulture + " not " +
                        "found!");
                    speechRecognition = new SpeechRecognitionEngine(SpeechRecognitionEngine.InstalledRecognizers()[0]);
                }
            }
            return speechRecognition;
        }

        private void Min_Btn_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Minimized;
        }

        private void Max_Btn_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
        }

        private void Browse_btn_Click(object sender, EventArgs e)
        {
            string userName = System.Environment.UserName;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Users\" + userName + "\\Documents\\MyMusic";
            ofd.Filter = "(mp3,wav,mov,wmv,mpg,avi,mp4,3gp,flv)|*.mp3;*.wav;*.mov;*.wmv;*.mpg;*.avi;*.mp4;*.3gp;*.flv|all files (*.*)|*.*";
            ofd.Multiselect = true;
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                files = ofd.SafeFileNames;
                paths = ofd.FileNames;
                for (int i = 0; i< files.Length; i++)
                {
                    Playlistlb.Items.Add(files[i]);
                }
            }
        }

        private void FRWD_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                DeccaPlayer.Ctlcontrols.fastReverse();
            }
            catch
            {
                return;
            }            
        }

        private void RWD_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (DeccaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
                {
                    if (Playlistlb.SelectedIndex == 0)
                    {
                        Playlistlb.SelectedIndex = 0;
                        Playlistlb.Update();
                    }

                    else
                    {
                        DeccaPlayer.Ctlcontrols.previous();
                        Playlistlb.SelectedIndex -= 1;
                        Playlistlb.Update();
                    }
                }
            }
            catch
            {
                return;
            }            
        }

        private void Play_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (DeccaPlayer.playState == WMPLib.WMPPlayState.wmppsScanForward || DeccaPlayer.playState == WMPLib.WMPPlayState.wmppsScanReverse)
                {

                    DeccaPlayer.Ctlcontrols.play();
                }
                else
                {
                    Playlistlb.SelectedIndex = 0;
                    DeccaPlayer.Ctlcontrols.play();
                }
            }
            catch
            {
                return;
            }
            
        }
            

        private void Pause_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (DeccaPlayer.playState == WMPLib.WMPPlayState.wmppsPaused)
                {
                    DeccaPlayer.Ctlcontrols.play();
                }
                else
                {
                    DeccaPlayer.Ctlcontrols.pause();
                }
            }
            catch
            {
                return;
            }                        
        }

        private void Stop_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                DeccaPlayer.Ctlcontrols.stop();
            }
            catch
            {
                return;
            }            
        }

        private void FFW_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (DeccaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
                {
                    if (Playlistlb.SelectedIndex < (Playlistlb.Items.Count - 1))
                    {
                        DeccaPlayer.Ctlcontrols.next();
                        Playlistlb.SelectedIndex += 1;
                        Playlistlb.Update();
                    }
                }
                else
                {
                    Playlistlb.SelectedIndex = 0;
                    Playlistlb.Update();
                }
            }
            catch
            {
                return;
            }                       
        }

        private void AddMedia_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                Browse_btn.PerformClick();
            }
            catch
            {
                return;
            }            
        }

        private void QuickFFW_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                DeccaPlayer.Ctlcontrols.fastForward();
            }
            catch
            {
                return;
            }            
        }

        private void Playlistlb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DeccaPlayer.URL = paths[Playlistlb.SelectedIndex];
            }
            catch
            {
                return;
            }            
        }

        private void DeccaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                int rate = 100 * (VolumeSpeed.Value - 100);
                DeccaPlayer.settings.volume = VolumeSpeed.Value;
            }
            catch
            {
                return;
            }            
        }

        private void MuteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (DeccaPlayer.settings.volume == 100)
                {
                    DeccaPlayer.settings.volume = 0;
                }
                else
                {
                    DeccaPlayer.settings.volume = 100;
                }
            }
            catch
            {
                return;
            }            
        }

        private void FullScreenBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (DeccaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
                {
                    DeccaPlayer.fullScreen = true;
                }
                else
                {
                    DeccaPlayer.fullScreen = false;
                }
            }
            catch
            {
                return;
            }
            
        }

        private void picturebox_MouseDown(object sender, MouseEventArgs e)
        {
            Top = 1;
            MoveX = e.X;
            MoveY = e.Y;
        }

        private void picturebox_MouseUp(object sender, MouseEventArgs e)
        {
            Top = 0;
        }

        private void picturebox_MouseMove(object sender, MouseEventArgs e)
        {
            if(Top == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MoveX, MousePosition.Y - MoveY);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Top = 1;
            MoveX = e.X;
            MoveY = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Top == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MoveX, MousePosition.Y - MoveY);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Top = 0;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picturebox_Click(object sender, EventArgs e)
        {

        }

        private void Close_Btn_Click(object sender, EventArgs e)
        {
            DeccaPlayer.Dispose();
            speechRecognition.RecognizeAsyncCancel();
            speechRecognition.Dispose();
            Application.Exit();            
        }
    }
}
