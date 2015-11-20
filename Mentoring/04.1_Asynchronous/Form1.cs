using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _04._1_Asynchronous
{
    public partial class Form1 : Form
    {
        private delegate void AsyncMethodCaller(int callDuration);

        public Form1()
        {
            InitializeComponent();
        }

        private void AsyncMethod(int callDuration)
        {
            MessageBox.Show("Started.");
            Thread.Sleep(callDuration);
            MessageBox.Show("Finished.");
        }

        private static void CallbackMethod(IAsyncResult ar)
        {
            MessageBox.Show("Callback started.");
            AsyncResult result = (AsyncResult)ar;
            AsyncMethodCaller caller = (AsyncMethodCaller)result.AsyncDelegate;
            
            string formatString = (string)ar.AsyncState;
            
            int threadId = 0;

            MessageBox.Show("Callback waiting started.");
            caller.EndInvoke(ar);
            MessageBox.Show("Callback finished.");

        }
        private void button1_Click(object sender, EventArgs e)
        {
            var task = new Task(() =>AsyncMethod(2000));

            MessageBox.Show("Task created.");
            task.Start();
            MessageBox.Show("Task started.");
            while (task.IsCompleted);
            MessageBox.Show("Task completed.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var caller = new AsyncMethodCaller(AsyncMethod);
            MessageBox.Show("Task Created.");
            IAsyncResult result = caller.BeginInvoke(2000, CallbackMethod, null);
            MessageBox.Show("Task BeginInvoke.");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var caller = new AsyncMethodCaller(AsyncMethod);
            MessageBox.Show("Task Created.");
            IAsyncResult result = caller.BeginInvoke(2000, null ,null);
            MessageBox.Show("Task BeginInvoke.");
            Thread.Sleep(100);

            caller.EndInvoke(result);
            MessageBox.Show("Task EndInvoke.");

        }
    }
}