using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;

namespace thrift_client
{
    class Program
    {
        static void Main(string[] args)
        {
#if SOCKET
            TTransport transport = new TSocket("localhost", 8080);
#else
            TTransport transport = new TNamedPipeClientTransport("MyPipeName");
#endif
            TProtocol protocol = new TBinaryProtocol(transport);
            HelloWorld.Client client = new HelloWorld.Client(protocol);

            transport.Open();
            String res = client.hoge("Send to Server");
            Console.WriteLine(res);
        }
    }
}
