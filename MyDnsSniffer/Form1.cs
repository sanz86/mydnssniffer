using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PacketDotNet;
using SharpPcap;
using Heijden.DNS;
using System.Net;
using DnsDig;
using MyDnsSniffer.classes;

namespace MyDnsSniffer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Console.SetOut(new FeedbackWriter(this.textBox1));
            r_id = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Stop the capturing process
            device.StopCapture();
            
            // Close the pcap device
            device.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }
    }
}
