using MyTestProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestProject.Classes
{
    class Cat : Mammal
    {
        public override void Sound()
        {
            Debug.WriteLine("Meow");
        }

    }
}
