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
using System.Threading;

namespace MyDnsSniffer
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
           // Console.SetOut(new FeedbackWriter(this.textBox_req));
           // Thread delet = new Thread(new ThreadStart(classes.deleter.delete));
           // delet.IsBackground = true;
           //// delet.Start();
           // classes.compare compr = new compare();
           // Thread comp = new Thread(new ThreadStart(compr.compar));
           // comp.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                // Stop the capturing process
                classes.sniffer.device.StopCapture();
            }
            catch { }
            // Close the pcap device
           classes.sniffer.device.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartTimer();
            //Thread tim = new Thread(new ThreadStart(current_time));
            //tim.IsBackground = true;
            //tim.Start();
           // current_time();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sniffer a = new sniffer();
            a.sniff();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        //void current_time()
        //{
        //    while (true)
        //    {
        //       Thread.Sleep(1000);
        //       String name = DateTime.Now.ToString("dd/mm/yyyy  hh:mm:ss tt");
        //       if (label_time.InvokeRequired)
        //       {
        //           label_time.Invoke(new MethodInvoker(delegate { name = label_time.Text; }));
        //       }
        //        else
        //       label_time.Text = DateTime.Now.ToString("dd/mm/yyyy  hh:mm:ss tt");
        //    }

        //}
        System.Windows.Forms.Timer tmr = null;
        private void StartTimer()
        {
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 1000;
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Enabled = true;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            label_time.Text = DateTime.Now.ToString("dd/mm/yyyy  hh:mm:ss tt");
        }

        private void button_stop_Click(object sender, EventArgs e)
        {

        }

        private void button_start_Click(object sender, EventArgs e)
        {

        }

        private void button_restart_Click(object sender, EventArgs e)
        {

        }
        
    }
}
