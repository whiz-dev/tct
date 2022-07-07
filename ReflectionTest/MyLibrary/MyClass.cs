using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public delegate string MyDelegate(string id, string name);

    public class MyClass
    {
        public string MyMethod(string label, MyDelegate myDelegate)
        {
            return $"{label} : {myDelegate("1", "park")}";
        }
    }
}
