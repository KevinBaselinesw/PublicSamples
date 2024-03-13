using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFSampleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(WCFSampleApp.WCFSampleService));
            host.Open();
            Console.WriteLine("The service is ready at {0}", host.BaseAddresses[0]);
            Console.Read();
        }
    }
}
