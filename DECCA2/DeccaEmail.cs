using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace DECCA2
{
    public partial class DeccaEmail : Form
    {
        SpeechRecognitionEngine speechRecognition = null;
        SpeechSynthesizer Decca = new SpeechSynthesizer();
        public static List<string> MsgList = new List<string>();
        public static List<string> MsgLint = new List<string>();

        public static String Qevent;

        int EmailNum = 0;

        string username;
        string password;

        public DeccaEmail()
        {
            InitializeComponent();            
            try
            {
                //speechRecognition = createSpeechEngine("en-US");

                speechRecognition = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

                speechRecognition.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);

                LoadGrammarAndCommands();

                GetUsernameAndPassword();

                speechRecognition.SetInputToDefaultAudioDevice();

                speechRecognition.BabbleTimeout = TimeSpan.FromSeconds(3.0);

                speechRecognition.RecognizeAsync(RecognizeMode.Multiple);

                Decca.SelectVoice("Microsoft Zira Desktop");
                Decca.Rate = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadGrammarAndCommands()
        {
            try
            {
                Choices texts = new Choices();
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\EmailReaderCommands.txt");

                texts.Add(lines);
                Grammar wordslist = new Grammar(new GrammarBuilder(texts));
                speechRecognition.LoadGrammar(wordslist);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void GetUsernameAndPassword()
        {
            try
            {
                //string MachineName = Environment.UserName;

                string constring = ConfigurationManager.ConnectionStrings["MyDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(constring);
                using (SqlCommand cmd = new SqlCommand("Select * FROM EmailInfo", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            username = sdr["Email"].ToString();
                            password = sdr["Password"].ToString();                            
                        }
                    }
                }
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
                case "get all emails":
                case "get all inbox emails":                    
                    GetAllEmails();
                    break;
                case "check for new emails":
                case "check new emails":
                    CheckEmails();
                    break;
                case "read email":
                    Decca.SpeakAsyncCancelAll();
                    try
                    {
                        Decca.SpeakAsync(MsgList[EmailNum]);
                    }
                    catch
                    {
                        Decca.SpeakAsync("There are no emails to read");
                    }
                    break;
                case "next email":
                    Decca.SpeakAsyncCancelAll();
                    try
                    {
                        EmailNum += 1;
                        Decca.SpeakAsync(MsgList[EmailNum]);
                    }
                    catch
                    {
                        EmailNum -= 1;
                        Decca.SpeakAsync("There are no further emails");
                    }
                    break;
                case "previous email":
                    Decca.SpeakAsyncCancelAll();
                    try
                    {
                        EmailNum -= 1;
                        Decca.SpeakAsync(MsgList[EmailNum]);
                    }
                    catch
                    {
                        EmailNum += 1;
                        Decca.SpeakAsync("There are no previous emails");
                    }
                    break;
                case "close email reader":
                    Decca.Speak("Closing email reader");
                    speechRecognition.RecognizeAsyncCancel();
                    speechRecognition.Dispose();
                    this.Close();
                    break;
            }
        }

        private void DeccaEmail_Load(object sender, EventArgs e)
        {
            
        }

        private void GetAllEmails()
        {
            try
            {
                WebClient objClient = new WebClient();
                string response;
                string title;
                string summary;
                string tagline;

                XmlDocument doc = new XmlDocument();

                objClient.Credentials = new NetworkCredential(username, password);

                response = Encoding.UTF8.GetString(objClient.DownloadData(@"https://mail.google.com/mail/feed/atom"));

                response = response.Replace(@"<feed version=""0.3"" xmlns=""http://purl.org/atom/ns#"">",@"<feed>");

                doc.LoadXml(response);

                string nr;

                nr = doc.SelectSingleNode(@"/feed/fullcount").InnerText;

                lbl_NumEmails.Text = nr;

                Decca.SpeakAsync("A total of " + nr + " emails are in your gmail inbox");

                tagline = doc.SelectSingleNode(@"/feed/tagline").InnerText;

                txt_MessTag.Text = tagline;

                Decca.SpeakAsync("Trendon, you have" + tagline);

                foreach (XmlNode node in doc.SelectNodes(@"/feed/entry"))
                {
                    txt_MessFrom.Text = node.SelectSingleNode("author").SelectSingleNode("name").InnerText;
                    Decca.SpeakAsync("Email from , " + txt_MessFrom.Text);

                    title = node.SelectSingleNode("title").InnerText;
                    txt_MessTit.Text = title;
                    Decca.SpeakAsync("It's about , " + txt_MessTit.Text);

                    summary = node.SelectSingleNode("summary").InnerText;
                    txt_MessSum.Text = summary;
                    Decca.SpeakAsync("Summary is , " + txt_MessSum.Text);

                }                
            }
            catch(Exception ex)
            {
                Decca.SpeakAsync("Please login to your gmail account and turn on less secure apps before the app can be used");
                MessageBox.Show("Login to your gmail account and turn on less secure apps before the app can be used ");
                //System.Diagnostics.Process.Start("https://support.google.com/accounts/answer/6010255?h1=en");
            }
        }

        private void CheckEmails()
        {
            string GmailAtomUrl = "https://mail.google.com/mail/feed/atom";

            XmlUrlResolver xmlUrlResolver = new XmlUrlResolver();
            xmlUrlResolver.Credentials = new NetworkCredential(username, password);

            XmlTextReader xmlTextReader = new XmlTextReader(GmailAtomUrl);

            xmlTextReader.XmlResolver = xmlUrlResolver;

            try
            {
                XNamespace ns = XNamespace.Get("http://purl.org/atom/ns#");
                XDocument xmlFeed = XDocument.Load(xmlTextReader);

                var emailItems = from item in xmlFeed.Descendants(ns + "entry")
                                 select new
                                 {
                                     Author = item.Element(ns + "author").Element(ns + "name").Value,
                                     Title = item.Element(ns + "title").Value,
                                     Link = item.Element(ns + "link").Attribute("href").Value,
                                     Summary = item.Element(ns + "summary").Value,
                                 };
                DeccaEmail.MsgList.Clear();
                DeccaEmail.MsgLint.Clear();

                foreach (var item in emailItems)
                {
                    if (item.Title == String.Empty)
                    {
                        DeccaEmail.MsgList.Add("Message from " + item.Author + ", There is no subject and summary");
                        DeccaEmail.MsgLint.Add(item.Link);
                    }
                    else
                    {
                        DeccaEmail.MsgList.Add("Message from " + item.Author + ", There is subject is "+item.Title+ ", and the summary is "+ item.Summary);
                        DeccaEmail.MsgLint.Add(item.Link);
                    }                    
                }

                if(emailItems.Count() > 0)
                {
                    if(emailItems.Count() == 1)
                    {
                        Decca.SpeakAsync("You have 1 new email");
                    }
                    else
                    {
                        Decca.SpeakAsync("You have " + emailItems.Count() + " new emails.");
                    }
                }
                else if (DeccaEmail.Qevent == "Checkfornewemails" && emailItems.Count() == 0)
                {
                    Decca.SpeakAsync("You have no new emails");
                    DeccaEmail.Qevent = String.Empty;
                }
            }
            catch (Exception ex)
            {
                Decca.SpeakAsync("You have submitted invalid log in information");
                Decca.SpeakAsync("Please login to your gmail account and turn on less secure apps before using this app." + ex.Message);
            }
        }
    }
}
