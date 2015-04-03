using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Spider
{
    public partial class Form1 : Form
    {
        private SerialPort myport;
        public Form1()
        {
            InitializeComponent();
            init();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void On_btn_Click(object sender, EventArgs e)
        {
            myport.WriteLine("HIGH");

            On_btn.Enabled = false;
            Off_btn.Enabled = true;
        }
        private void Off_btn_Click(object sender, EventArgs e)
        {
            myport.WriteLine("LOW");

            On_btn.Enabled = true;
            Off_btn.Enabled = false;
        }
            private void init()
        {
            try
            {
                myport = new SerialPort();
                myport.BaudRate = 9600;
                myport.PortName = "COM4";
                myport.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong when trying to connect to the Arduino");
            }

                On_btn.Enabled = true;
                Off_btn.Enabled = false;
        }

    }
}
