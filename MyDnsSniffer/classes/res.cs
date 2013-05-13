using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDnsSniffer.classes
{
    class res
    {
        int i;
        Decimal ti;
        Decimal p;
        String ip;
        int ri;
        String hn;
        String ipr;
        DateTime tm;
        public res()
        {
            i = 0;
            ti = 0;
            p = 0;
            ip = "";
            ri = 0;
            hn = "";
            ipr = "";
            tm = null;
        }
        public int Id
        {
            get
            {
                return i;
            }
            set
            {
                i = value;
            }
        }
        public Decimal t_id_res
        {
            get
            {
                return ti;
            }
            set
            {
                ti = value;
            }
        }

        public Decimal port_res
        {
            get
            {
                return p;
            }
            set
            {
                p = value;
            }
        }
        public String src_ip_res
        {
            get
            {
                return ip;
            }
            set
            {
                ip = value;
            }
        }
        public int res_id
        {
            get
            {
                return ri;
            }
            set
            {
                ri = value;
            }
        }
        public String host_name_res
        {
            get
            {
                return hn;
            }
            set
            {
                hn = value;
            }
        }
        public String ip_res
        {
            get
            {
                return ipr;
            }
            set
            {
                ipr = value;
            }
        }
        public DateTime timestamp
        {
            get
            {
                return tm;
            }
            set
            {
                tm = value;
            }
        }
    }
}
