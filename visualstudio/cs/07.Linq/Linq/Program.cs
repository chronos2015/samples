using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> data = new List<string>() { "1", "2", "2", "3", "3" };
            string ret = data.GroupBy(s => s)
                .Select(s => new { key = s.First(), size = s.Count() })
                .OrderByDescending(s => s.size)
                .ThenByDescending(s => s.key)
                .First().key;
        }
    }
}
