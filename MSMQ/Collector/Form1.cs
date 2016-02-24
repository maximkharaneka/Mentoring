using System;
using System.Threading;
using System.Windows.Forms;

namespace Collector
{
    public partial class Form1 : Form
    {
        private const string MessageQueueName = @".\private$\MyStringQueue";
        private FileWatcher fileWatcher;
        private bool isFileWatcherStarted;
        private bool isListenerStarted;
        private MSMQWrapper msmq;
        private Thread readThread;
        private string text;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileWatcher = new FileWatcher(textBox1.Text, MessageQueueName);
            fileWatcher.Start();
            isFileWatcherStarted = true;
            toolStripStatusLabel1.Text = "Started";
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fileWatcher.Stop();
            isFileWatcherStarted = false;
            toolStripStatusLabel1.Text = "Stoped";
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = fileWatcher.msmq.RecieveMessage();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            isListenerStarted = true;
            toolStripStatusLabel4.Text = "Started";
            button3.Enabled = false;
            button4.Enabled = true;
            msmq = new MSMQWrapper(MessageQueueName);
            readThread = new Thread(timer);
            readThread.Start();
            timer1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            isListenerStarted = false;
            toolStripStatusLabel1.Text = "Stoped";
            button3.Enabled = true;
            button4.Enabled = false;
            readThread.Join();
            timer1.Enabled = false;
        }

        private void timer()
        {
            var message = msmq.RecieveMessage();
            text += message + "\n";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (text != null)
            {
                listBox1.Items.Add(text);
                text = null;
            }
        }
    }
}