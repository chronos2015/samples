using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift;
using Thrift.Server;
using Thrift.Transport;

namespace thrift_server
{
    class HelloWorldHandler : HelloWorld.Iface
    {
        public string hoge(string data)
        {
            Console.WriteLine(data);
            return "SendFromServer";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HelloWorldHandler handler = new HelloWorldHandler();
            HelloWorld.Processor processor = new HelloWorld.Processor(handler);

#if SOCKET
            TServerTransport serverTransport = new TServerSocket(8080);
#else
            TServerTransport serverTransport = new TNamedPipeServerTransport("MyPipeName");
#endif
            TServer server = new TSimpleServer(processor, serverTransport);
            server.Serve();
        }
    }
}
