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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Console.SetOut(new FeedbackWriter(this.textBox1));
            Thread delet = new Thread(new ThreadStart(classes.deleter.delete));
            delet.IsBackground = true;
            //delet.Start();
           /* classes.compare compr = new compare();
            Thread comp = new Thread(new ThreadStart(compr.compar));
            comp.Start();*/

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //List<List<int>> a = new List<List<int>>();
            //List<int> b;
            //for (int i = 1; i < 5; i++)
            //{
            //    b = new List<int>();
            //    for (int j = 1; j < 5; j++)
            //    {
            //        b.Add(i * j);

            //    }
            //    a.Add(b);
            //}

            //foreach (List<int> aa in a)
            //{
            //    foreach (int aaaa in aa)
            //        Console.WriteLine("{0}", aaaa);
            //}
            //int d = 0;
            //foreach (List<int> aa in a)
            //{
            //    if (d == 1)
            //    {
            //        a.Remove(aa);
            //        break;
            //    }
            //    d++;
            //    //foreach (int aaaa in aa)
            //        //Console.WriteLine("{0}", aaaa);
            //}
            //foreach (List<int> aa in a)
            //{
            //    foreach (int aaaa in aa)
            //        Console.WriteLine("{0}", aaaa);
            //}

            compare a = new compare();
            a.analyse_host("jatinga.iitg.ernet.in.");
            /*try
            {
                // Stop the capturing process
                classes.sniffer.device.StopCapture();
            }
            catch { }
            // Close the pcap device
           classes.sniffer.device.Close();*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sniffer a = new sniffer();
            a.sniff();
        }
    }
}
