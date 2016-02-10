using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrackMeKeygen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Class0 class0 = new Class0();
            NetworkInterface networkInterface = Enumerable.FirstOrDefault<NetworkInterface>((IEnumerable<NetworkInterface>)NetworkInterface.GetAllNetworkInterfaces());
            var addressBytes = networkInterface.GetPhysicalAddress().GetAddressBytes();

            var date = BitConverter.GetBytes(DateTime.Now.Date.ToBinary());

            int[] numArray = addressBytes.Select((byte_1, int_1) => (int)byte_1 ^ (int)date[int_1]).Select(x => x < 999 ? x * 10 : x).ToArray();

            numArray.ToList().ForEach(x => Console.Write(x + "-"));

            var result = numArray.ToList().Select(x => x.ToString()).Aggregate((x,y)=>x+"-"+y);

            textBox1.Text = result;
        }
    }

    public sealed class C
    {

        internal int method_0(int int_0)
        {
            if (int_0 <= 999)
            {
                return int_0 * 10;
            }
            return int_0;
        }

        internal bool method_1(int int_0)
        {
            return int_0 == 0;

        }
    }

    public sealed class Class0
    {
        public byte[] byte_0;
        public int[] int_0;

        internal int method_0(byte byte_1, int int_1)
        {
            return (int)byte_1 ^ (int)this.byte_0[int_1];
        }

        internal int method_1(int int_1, int int_2)
        {
            return int_1 - this.int_0[int_2];

        }
    }
}
