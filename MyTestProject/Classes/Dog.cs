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
        public new void Sound()
        {
            Debug.WriteLine("Bark");
        }
        public override void Weight()
        {
            Debug.WriteLine("Weight from Subclass");
        }
        public void Height()
        {
            Debug.WriteLine("Height from Subclass");
        }
    }
}
