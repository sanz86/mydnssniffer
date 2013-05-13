using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDnsSniffer.classes
{
    class req
    {
        int i;
        Decimal ti;
        Decimal p;
        String ip;
       
        String hn;
        
        public req()
        {
            i = 0;
            ti = 0;
            p = 0;
            ip = "";
           
            hn = "";
           
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
        public Decimal t_id
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

        public Decimal port
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
        public String dest_ip
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
       
      
        public String host_name
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
       
    }
}
