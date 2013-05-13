using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDnsSniffer.classes
{
    class host_ip
    {
       
        int ri;
        String hn;
        String ipr;
        
        public host_ip()
        {
           
            ri = 0;
            hn = "";
            ipr = "";
           
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
    }
}
