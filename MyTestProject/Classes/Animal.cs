using MyTestProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestProject.Classes
{
    class Animal : IAnimal
    {
        static public int speed = 10;
        public void Sound()
        {
            Debug.WriteLine("Animal");
        }

        public void Weight()
        {
            Debug.WriteLine("Weight from Superclass");
        }
    }
}
