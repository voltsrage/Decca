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
using System.Threading;

namespace DECCA2
{
    public partial class Decca_SpeechToText : Form
    {
        public SpeechRecognitionEngine recognitionEngine;

        public Grammar grammar;

        public Thread RecThread;

        public Boolean RecognizerState = true;

        public Decca_SpeechToText()
        {
            InitializeComponent();           
        }

        public void RecThreadFunction()
        {
            while(true)
            {
                try
                {
                    recognitionEngine.Recognize();
                }
                catch
                {
                    return;
                }
            }
        }

        public void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (!RecognizerState)
                return;

            this.Invoke((MethodInvoker)delegate
            {
                richTextBox1.Text += (" " + e.Result.Text.ToLower());
            });
        }

        private void Decca_SpeechToText_Load(object sender, EventArgs e)
        {
            GrammarBuilder builder = new GrammarBuilder();
            builder.AppendDictation();

            grammar = new Grammar(builder);

            recognitionEngine = new SpeechRecognitionEngine();
            recognitionEngine.LoadGrammar(grammar);
            recognitionEngine.SetInputToDefaultAudioDevice();

            recognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

            RecognizerState = true;
            RecThread = new Thread(new ThreadStart(RecThreadFunction));
            RecThread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecognizerState = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RecognizerState = false;
        }

        private void Decca_SpeechToText_FormClosing(object sender, FormClosingEventArgs e)
        {
            RecThread.Abort();
            RecThread = null;

            recognitionEngine.UnloadAllGrammars();
            recognitionEngine.Dispose();

            grammar = null;
        }
    }
}
