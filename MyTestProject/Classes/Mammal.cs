using MyTestProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestProject.Classes
{
    abstract class Mammal : IAnimal
    {
        public abstract void Sound();
    }
}
