using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDnsSniffer.classes
{
    class compare
    {
        dnsSnifferTableAdapters.res_tableTableAdapter resta = new MyDnsSniffer.dnsSnifferTableAdapters.res_tableTableAdapter();
        dnsSnifferTableAdapters.req_tableTableAdapter reqta = new MyDnsSniffer.dnsSnifferTableAdapters.req_tableTableAdapter();
        dnsSnifferTableAdapters.additional_host_ipTableAdapter addta = new MyDnsSniffer.dnsSnifferTableAdapters.additional_host_ipTableAdapter();
        dnsSnifferTableAdapters.host_ipTableAdapter hostta = new MyDnsSniffer.dnsSnifferTableAdapters.host_ipTableAdapter();

        public void compar()
        {
            res t_res = new res();
           dnsSniffer.res_tableDataTable resdt = resta.GetDataByTop();
           foreach (dnsSniffer.res_tableRow dr in resdt)
           {
               t_res.Id = dr.Id;
               t_res.ip_res = dr.ip_res;
               t_res.port_res = dr.port_res;
               t_res.res_id = dr.res_id;
               t_res.src_ip_res = dr.src_ip_res;
               t_res.t_id_res = dr.t_id_res;
               t_res.timestamp = dr.timestamp;

           }
           dnsSniffer.req_tableDataTable reqdt = reqta.GetDataByHostName(t_res.host_name_res);
           if (reqdt.Rows.Count > 0)
           {
               foreach (dnsSniffer.req_tableRow dr in reqdt)
               {
                   if ((dr.t_id == t_res.t_id_res) && (dr.port == t_res.port_res) && (dr.dest_ip == t_res.src_ip_res))
                   {
                       dnsSniffer.host_ipDataTable hdt = hostta.GetDataByHost(t_res.host_name_res);
                       if (hdt.Rows.Count > 0)
                       {
                           foreach (dnsSniffer.host_ipRow hr in hdt)
                           {
                               if (hr.res_id != t_res.res_id)
                               {
                                   foreach (dnsSniffer.host_ipRow hri in hdt)
                                   {
                                       if (hri.ip_res != t_res.ip_res)
                                       {
                                           analyse_host(t_res.host_name_res);
                                       }

                                   }
                               }
                               else
                               {
                                   hostta.Insert(t_res.res_id, t_res.host_name_res, t_res.ip_res);
                               }
                           }
                       }
                       else
                       {
                           hostta.Insert(t_res.res_id,t_res.host_name_res,t_res.ip_res);
                       }
                   }
                   else
                   {
                       analyse_host(t_res.host_name_res);
                       break;
                   }
               }
           }
           else
           {
               Console.WriteLine("Not applicable...");
           }
        }

        private void analyse_host(String host)
        {
            DateTime timestamp_leg, timestamp_ano;
            String ip_leg, ip_ano;
            int count = 0;

            

            dnsSniffer.res_tableDataTable resdt = resta.GetDataByHost(host);
            res[] res_t = new res[resdt.Rows.Count];
            int i = 0;
            foreach (dnsSniffer.res_tableRow rs_r in resdt)
            {
                res_t[i] = new res();
                res_t[i].Id = rs_r.Id;
                res_t[i].ip_res = rs_r.ip_res;
                res_t[i].port_res = rs_r.port_res;
                res_t[i].res_id = rs_r.res_id;
                res_t[i].src_ip_res = rs_r.src_ip_res;
                res_t[i].t_id_res = rs_r.t_id_res;
                res_t[i].timestamp = rs_r.timestamp;
                i++;
            }

            
            dnsSniffer.req_tableDataTable reqdt = reqta.GetDataByHostName(host);
            req[] req_t = new req[reqdt.Rows.Count];
            i = 0;
            foreach (dnsSniffer.req_tableRow rq_r in reqdt)
            {
                req_t[i] = new req();
                req_t[i].dest_ip = rq_r.dest_ip;
                req_t[i].Id = rq_r.Id;
                req_t[i].port = rq_r.port;
                req_t[i].t_id = rq_r.t_id;
                req_t[i].host_name = rq_r.host_name;
                
            }


        }
    }
}
