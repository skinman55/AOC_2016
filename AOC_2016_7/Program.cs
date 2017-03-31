using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace AOC_2016_7
{
    class Program
    {
        static void Main(string[] args)
        {
            var ipv = new IpValidator();
            ipv.Run();
        }
    }

    class IpValidator
    {
        

        public void Run()
        {
            var TLSCount = 0;
            var SSLCount = 0;
            var input = File.ReadAllLines("input.txt");
            
            foreach (var address in input)
            {
                var hypernets = GetHypernets(address);
                var supernets = Getsupernets(hypernets, address);

                if (DoesSupportTLS(hypernets, supernets))
                {
                    TLSCount++;
                }

                if (DoesSupportSSL(hypernets,supernets))
                {
                    SSLCount++;
                }
            }

            Console.WriteLine("TLS: " + TLSCount);
            Console.WriteLine("SSL: " + SSLCount);
            Console.ReadLine();
        }

        private bool DoesSupportTLS(List<string> hypernets, List<string> supernets)
        {          
            foreach (var hn in hypernets)
            {
                if (DoesStringContainABBA(hn))
                {
                    Trace.WriteLine("FALSE - ABBA IN HYPERNET");
                    return false;
                }
            }

            foreach (var sn in supernets)
            {
                if (DoesStringContainABBA(sn))
                {
                    Trace.WriteLine("TRUE");
                    return true;
                }
            }

            Trace.WriteLine("FALSE - NO ABBA");
            return false;
        }

        private List<string> Getsupernets(IEnumerable<string> hypernets, string address)
        {
            var supernets = new List<string>();
            var tempAddress = address;

            foreach (var hn in hypernets)
            {
                var idx = tempAddress.IndexOf(hn);
                if (idx == 0)
                {
                    idx = tempAddress.IndexOf(hn, hn.Length);
                    supernets.Add(tempAddress.Substring(0, idx > 0 ? idx - 1 : 0));
                }
                else
                {
                    supernets.Add(tempAddress.Substring(0, idx > 0 ? idx - 1 : 0));
                }

                tempAddress = tempAddress.Remove(0, idx + hn.Length + 1);
            }
            supernets.Add(tempAddress);
            return supernets;
        }

        private List<string> GetHypernets(string address)
        {
            var hypernetindexes = address.Where((x => x == '['));
            var startIndex = 0;
            var hypernets = new List<string>();
            foreach (var idx in hypernetindexes)
            {
                var lb = address.IndexOf("[", startIndex) + 1;
                var rb = address.IndexOf("]", startIndex) + 1;
                startIndex = rb;

                var hypernet = address.Substring(lb, rb - lb - 1);
              
                hypernets.Add(hypernet);
            }
            return hypernets;
        }

        private bool DoesSupportSSL(List<string> hypernets, List<string> supernets)
        {
            var ABAs = new List<string>();
            foreach (var sn in supernets)
            {
                ABAs.AddRange(GetABAMatches(sn).ToList()); 
            }

            foreach (var ABA in ABAs)
            {
                if (ABA.Length != 3)
                {
                    throw new Exception("Fail!");
                }
              
                var BAB = new string(new [] {ABA[1],ABA[0],ABA[1]});

                foreach (var hn in hypernets)
                {
                    if (hn.Contains(BAB))
                    {
                        return true;
                    }
                }
            }


            Trace.WriteLine("FALSE - NO ABBA");
            return false;

        }
        
        private bool DoesStringContainABBA(string value)
        {
            
            var regex = new Regex(@"(\w+)(\w+)\2\1");

            var match = regex.Match(value);

            if (!match.Success)
                return false;

            foreach (var capture in match.Captures)
            {
                if (capture.ToString().Distinct().Count() < 2)
                    return false;
            }

            ///gotcha! lgkklg
            if (match.Value.Length > 4)
                return false;

            Trace.WriteLine(match.Value);
            return true;
        }

        private IEnumerable<string> GetABAMatches(string value)
        {
            var matches = new List<string>();
            var regex = new Regex(@"([a-z])(?!\1)([a-z])\1");

            var match = regex.Match(value);

            if (!match.Success)
                return matches;

            foreach (var capture in match.Captures)
            {
                matches.Add(capture.ToString());
                //if (capture.ToString().Distinct().Count() < 2)
                //    return false;
            }

            if (value.Length > 3)
            {
                matches.AddRange(GetABAMatches(value.Substring(1)));
            }

            ///gotcha! lgkklg
            //if (match.Value.Length > 4)
            //    return false;

           
            return matches.Distinct();
        }
    }
}
