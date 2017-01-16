using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastarObsta
{
    class Class4
    {
        public string uuid { get; set; }
        public List<Rate> rates { get; set; }
        public int rate { get; set; }
    }

    public class Rate
    {
        public int marker { get; set; }
        public int rate { get; set; }
    }
}
