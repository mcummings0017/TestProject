using MyTestProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestProject.Classes
{
    class Dog : Animal, IAnimal
    {
        public void Sound()
        {
            Debug.WriteLine("Bark");
        }
        public void Weight()
        {
            Debug.WriteLine("Weight from Subclass");
        }
    }
}
