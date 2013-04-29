using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LikeARock
{
    class Manifest
    {
        public string Type { get; set; }
        public int Latest_sol { get; set; }
        public int Num_images { get; set; }
        public string Most_recent { get; set; }
        public List<SolDay> Sols { get; set; }

    }
}
