using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqLabs.作業
{
    class Student
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Chi { get; set; }
        public int Eng { get; internal set; }
        public int Math { get; set; }
        public string Gender { get; set; }
    }
}
