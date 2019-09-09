using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace json
{
    public class Hoge
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "ags")]
        public int Age { get; set; }
    }
}
