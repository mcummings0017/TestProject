using MyTestProject.Classes;
using MyTestProject.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestProject.Tests
{
    [TestFixture]
    class InterfaceTest
    {
        [Test]
        public void Test1()
        {
            List<IAnimal> al = new List<IAnimal>();
            List<Animal> al1 = new List<Animal>();
            List<Mammal> al2 = new List<Mammal>();

            IAnimal c = new Cat();
            Cat c1 = new Cat();
            Mammal c2 = new Cat();
            IAnimal d = new Dog();
            Dog d2 = new Dog();
            Animal d3 = new Dog();
            Animal d4 = new Animal();

            Debug.WriteLine(Animal.speed);

            Type d2t = d2.GetType();
            Type d3t = d3.GetType();

            al.Add(c);
            al.Add(c1);
            al.Add(c2);
            al.Add(d);
            al.Add(d2);
            al.Add(d3);
            al.Add(d4);

            al1.Add(d2);
            al1.Add(d3);
            al1.Add(d4);

            al2.Add(c1);
            al2.Add(c2);

            d2.Weight();
            d3.Weight();
            d4.Weight();
            

            foreach (IAnimal x in al)
            {
                Debug.WriteLine("Type: " + x.GetType() + " ToString: " + x.ToString());
                x.Sound();
            }

            foreach(Animal x in al1)
            {
                Debug.WriteLine("Type: " + x.GetType() + " ToString: " + x.ToString());
                x.Sound();
            }
            d3.Sound();

            foreach (Mammal x in al2)
            {
                Debug.WriteLine("Type: " + x.GetType() + " ToString: " + x.ToString());
                x.Sound();
            }
        }

    }
}
