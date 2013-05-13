using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PacketDotNet;
using SharpPcap;
using Heijden.DNS;
using System.Net;
using DnsDig;
using System.Threading;

namespace MyDnsSniffer.classes
{
   class sniffer
    {

        dnsSnifferTableAdapters.req_tableTableAdapter reqta = new MyDnsSniffer.dnsSnifferTableAdapters.req_tableTableAdapter();
        dnsSnifferTableAdapters.additional_host_ipTableAdapter addta = new MyDnsSniffer.dnsSnifferTableAdapters.additional_host_ipTableAdapter();
        dnsSnifferTableAdapters.host_ipTableAdapter hostta = new MyDnsSniffer.dnsSnifferTableAdapters.host_ipTableAdapter();
        CaptureDeviceList devices; 
        public static ICaptureDevice device;
        public static int r_id;
        static Response response;

        public void sniff()
        {
            
            r_id = 1;
            // Print SharpPcap version 
            string ver = SharpPcap.Version.VersionString;
            Console.WriteLine("SharpPcap {0}, Example1.IfList.cs", ver);

            // Retrieve the device list
            devices = CaptureDeviceList.Instance;

            // If no devices were found print an error
            if (devices.Count < 1)
            {
                Console.WriteLine("No devices were found on this machine");
                return;
            }

            Console.WriteLine("\nThe following devices are available on this machine:");
            Console.WriteLine("----------------------------------------------------\n");

            // Print out the available network devices
            foreach (ICaptureDevice dev in devices)
                Console.WriteLine("{0}\n", dev.ToString());

            Console.Write("Hit 'Enter' to exit...");
            // Console.ReadLine();
            // Extract a device from the list
            device = devices[0];

            // Register our handler function to the
            // 'packet arrival' event
            device.OnPacketArrival += new SharpPcap.PacketArrivalEventHandler(device_OnPacketArrival);

            // Open the device for capturing
            int readTimeoutMilliseconds = 1000;
            device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);

            Console.WriteLine("-- Listening on {0}, hit 'Enter' to stop...", device.Description);

            // Start the capturing process
            device.StartCapture();

            // Wait for 'Enter' from the user.
            //while (true) { };
        }

   

        private static void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            
            DateTime time = e.Packet.Timeval.Date;

            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

            var udpPacket = PacketDotNet.UdpPacket.GetEncapsulated(packet); //PacketDotNet.TcpPacket.GetEncapsulated(packet);
            if (udpPacket != null)
            {
                var ipPacket = (PacketDotNet.IpPacket)udpPacket.ParentPacket;
                System.Net.IPAddress srcIp = ipPacket.SourceAddress;

                System.Net.IPAddress dstIp = ipPacket.DestinationAddress;

                // MessageBox.Show(srcIp.ToString() + "  " + dstIp.ToString());

                int srcPort = udpPacket.SourcePort;
                int dstPort = udpPacket.DestinationPort;
                if ((srcPort == 53) || (dstPort == 53))
                {
                    IPEndPoint a = new IPEndPoint(ipPacket.SourceAddress, srcPort);
                    response = new Response(a, udpPacket.PayloadData);
                   
                    if ((response.header.QR == false))// && (srcIp == IPAddress.Parse("10.12.11.74")))
                    {
                        Console.WriteLine("Got an Question...");
                        display();
                        
                        dnsSnifferTableAdapters.req_tableTableAdapter reqta = new MyDnsSniffer.dnsSnifferTableAdapters.req_tableTableAdapter();
                        reqta.Insert(Convert.ToDecimal(response.header.ID), Convert.ToDecimal(srcPort), dstIp.ToString(), response.Questions[0].QName.ToString(),time);
                    }
                    else if ((response.header.QR == true))// && (dstIp == IPAddress.Parse("10.12.11.74")))
                    {
                        Console.WriteLine("Got an Answer..");
                        if (response.Answers.Count > 0)
                        {
                            Console.WriteLine("Got an Answer..");
                            display();
                        }
                        dnsSnifferTableAdapters.res_tableTableAdapter resta = new MyDnsSniffer.dnsSnifferTableAdapters.res_tableTableAdapter();
                      
                        foreach (AnswerRR answerRR in response.Answers)
                        {
                          resta.Insert(Convert.ToDecimal(response.header.ID), Convert.ToDecimal(dstPort), srcIp.ToString(), r_id, answerRR.NAME.ToString(), answerRR.RECORD.ToString(), time);
                        }
                        dnsSnifferTableAdapters.additional_host_ipTableAdapter addta = new MyDnsSniffer.dnsSnifferTableAdapters.additional_host_ipTableAdapter();
                        foreach (AdditionalRR additionalRR in response.Additionals)
                        {
                            if(additionalRR.RECORD.ToString()!="not-used")
                            addta.Insert(r_id,additionalRR.NAME.ToString(), additionalRR.RECORD.ToString());
                        }

                        r_id++;
                    }


                }
            }

        }

        private static void display()
        {
            
            Console.WriteLine(";; Got answer:");

            Console.WriteLine(";; ->>HEADER<<- opcode: {0}, status: {1}, id: {2}",
                  response.header.OPCODE,
                  response.header.RCODE,
                  response.header.ID);
            Console.WriteLine(";; flags: {0}{1}{2}{3}; QUERY: {4}, ANSWER: {5}, AUTHORITY: {6}, ADDITIONAL: {7}",
                 response.header.QR ? " Question... " : "Answer ..",
                 response.header.AA ? " aa" : "",
                 response.header.RD ? " rd" : "",
                 response.header.RA ? " ra" : "",
                 response.header.QDCOUNT,
                 response.header.ANCOUNT,
                 response.header.NSCOUNT,
                 response.header.ARCOUNT);
            Console.WriteLine("");

            if (response.header.QDCOUNT > 0)
            {
                Console.WriteLine(";; QUESTION SECTION:");
                foreach (Question question in response.Questions)
                    Console.WriteLine(";{0}", question);
                Console.WriteLine("");
            }

            if (response.header.ANCOUNT > 0)
            {
                Console.WriteLine(";; ANSWER SECTION:");
                foreach (AnswerRR answerRR in response.Answers)
                    Console.WriteLine(answerRR);
                Console.WriteLine("");
            }

            if (response.header.NSCOUNT > 0)
            {
                Console.WriteLine(";; AUTHORITY SECTION:");
                foreach (AuthorityRR authorityRR in response.Authorities)
                    Console.WriteLine(authorityRR);
                Console.WriteLine("");
            }

            if (response.header.ARCOUNT > 0)
            {
                Console.WriteLine(";; ADDITIONAL SECTION:");
                foreach (AdditionalRR additionalRR in response.Additionals)
                    Console.WriteLine(additionalRR);
                Console.WriteLine("");
            }
        }
    }
}
