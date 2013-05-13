using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MyDnsSniffer.classes
{
    class deleter
    {
        public static void delete()
        {
            while (true)
            {
                dnsSnifferTableAdapters.req_tableTableAdapter reqta = new MyDnsSniffer.dnsSnifferTableAdapters.req_tableTableAdapter();
                DateTime time = DateTime.Now;
                Thread.Sleep(5000);
                reqta.DeleteQueryTime(time);
            }
        }
    }
}
