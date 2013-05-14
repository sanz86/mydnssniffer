using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;

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
            Console.WriteLine("hello");
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
               Console.WriteLine("Why");
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

        public void analyse_host(String host)
        {
            DateTime timestamp_leg, timestamp_ano;
            String ip_leg, ip_ano;
            int count = 0;
            List<int> r_i = new List<int>();

            List<res> R = new List<res>();

            dnsSniffer.res_tableDataTable resdt = resta.GetDataByHost(host);
            
            foreach (dnsSniffer.res_tableRow rs_r in resdt)
            {

                res res_t = new res();
                res_t.Id = rs_r.Id;
                res_t.ip_res = rs_r.ip_res;
                res_t.port_res = rs_r.port_res;
                res_t.res_id = rs_r.res_id;
                res_t.src_ip_res = rs_r.src_ip_res;
                res_t.t_id_res = rs_r.t_id_res;
                res_t.host_name_res = rs_r.host_name_res;
                res_t.timestamp = rs_r.timestamp;
                R.Add(res_t);
                if (r_i.Find(delegate(int bk) { return bk == res_t.res_id; })==0)
                    r_i.Add(res_t.res_id);
            }

            
            List<req> B = new List<req>();
            dnsSniffer.req_tableDataTable reqdt = reqta.GetDataByHostName(host);
            foreach (dnsSniffer.req_tableRow rq_r in reqdt)
            {
                req req_t = new req();
                req_t.dest_ip = rq_r.dest_ip;
                req_t.Id = rq_r.Id;
                req_t.port = rq_r.port;
                req_t.t_id = rq_r.t_id;
                req_t.host_name = rq_r.host_name;
                B.Add(req_t);
            }
           
            
            List<List<res>> A = new List<List<res>>();
            foreach(int rr in r_i)
            A.Add(get_res_r_id(rr,R));

            List<List<List<res>>> AA = new List<List<List<res>>>();
            AA = get_group(A);
            
            foreach (List<List<res>> Ai in AA)
            {
                Console.WriteLine("{0}", Ai.Count);
                Console.ReadLine();
                foreach (List<res> Aa in Ai)
                {
                    foreach (res Aaa in Aa)
                    {
                        Console.WriteLine("{0} .. {1} .. {2}", Aaa.host_name_res, Aaa.res_id, Aaa.ip_res);
                    }
                    Console.WriteLine(" .... ");
                }
                Console.WriteLine(" ####### ");

            }
          
           
        }
        public List<res> get_res_r_id(int r_id,List<res> A )
        {
            List<res> result = new List<res>();
            foreach (res r in A)
            {
                if (r.res_id == r_id)
                {
                    result.Add(r);
                }
            }
            return result;
        }

        public List<List<List<res>>> get_group(List<List<res>> AA)
        {
            List<List<List<res>>> result = new List<List<List<res>>>();
            List<List<res>> A;
            List<res> temp = new List<res>();
            while (AA.Count > 0)
            {
                A = new List<List<res>>();
                foreach (List<res> Aa in AA)
                {
                    temp = Aa;
                    A.Add(temp);
                    AA.Remove(Aa);
                    break;
                }

                List<List<res>> temp2 = new List<List<res>>();
                foreach (List<res> Aa in AA)
                {

                    if (compare_Aa(Aa, temp))
                    {
                        A.Add(Aa);
                        temp2.Add(Aa);

                    }
                }
                foreach (List<res> tm in temp2)
                {
                    AA.Remove(tm);
                }
                Console.WriteLine("{0}", A.Count);
                result.Add(A);
            }
            return result;
        }

        bool compare_Aa(List<res> a, List<res> b)
        {
            if (a.Count == b.Count)
            {
                for (int j = 0; j < a.Count; j++)
                {
                    if (a[j].ip_res != b[j].ip_res)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
                return false;
        }
      
    }
}
