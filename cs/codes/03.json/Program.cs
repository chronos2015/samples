using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace json
{
    class Program
    {
        static void Main(string[] args)
        {
            var hoge = new Hoge() { Name = "sample", Age = 16 };

            var stream = new MemoryStream();

            // シリアライズ
            new DataContractJsonSerializer(typeof(Hoge)).WriteObject(stream, hoge);

            stream.Seek(0, SeekOrigin.Begin);
            var sw = new StreamReader(stream);
            string str = sw.ReadToEnd();

            Console.WriteLine(str);
            stream.Seek(0, SeekOrigin.Begin);

            // デシリアライズ
            var hoge2 = new DataContractJsonSerializer(typeof(Hoge)).ReadObject(stream);

            Console.WriteLine(hoge2);

            return;
        }
    }
}
