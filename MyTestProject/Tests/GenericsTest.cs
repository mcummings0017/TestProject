using MyTestProject.Classes;
using NUnit.Framework;
using System;

namespace MyTestProject.Tests
{
    class GenericsTest
    {
        [Test]
        public void Test1()
        {
            MyGenerics g = new MyGenerics();

            int x = 5, y = 4;
            g.GetSum<int>(ref x, ref y);

            string strX = "5", strY = "4";
            g.GetSum(ref strX, ref strY);

            // Use the generic struct
            Rectangle<int> rec1 = new Rectangle<int>(20, 50);
            Console.WriteLine(rec1.GetArea());

            Rectangle<string> rec2 = new Rectangle<string>("20", "50");
            Console.WriteLine(rec2.GetArea());

            // Delegates allow you to reference methods
            // inside a delegate object. The delegate
            // object can then be passed to other 
            // methods that can call the methods assigned
            // to the delegate. It can also stack methods
            // that are called in the specified order

            // Create delegate objects
            Arithmetic add, sub, addSub;

            // Assign just the Add method
            add = new Arithmetic(Add);

            // Assign just the Subtract method
            sub = new Arithmetic(Subtract);

            // Assign Add and Sub
            addSub = add + sub;

            // You could also subtract a method
            // sub = addSub - add;

            // Print out results
            Console.WriteLine($"Add {6} & {10}");
            add(6, 10);

            // Call both methods
            Console.WriteLine($"Add & Subtract {10} & {4}");
            addSub(10, 4);

            Console.ReadLine();
        }

        public struct Rectangle<T>
        {
            // Generic fields
            private T width;
            private T length;

            // Generic properties
            public T Width
            {
                get { return width; }
                set { width = value; }
            }

            public T Length
            {
                get { return length; }
                set { length = value; }
            }

            // Generic constructor
            public Rectangle(T w, T l)
            {
                width = w;
                length = l;
            }

            public string GetArea()
            {
                double dblWidth = Convert.ToDouble(Width);
                double dblLength = Convert.ToDouble(Length);
                return string.Format($"{Width} * {Length} = {dblWidth * dblLength}");
            }
        }

        // Declare a delegate type that performs
        // arithmetic. It defines the return type
        // and the types for attributes 
        public delegate void Arithmetic(double num1, double num2);

        // Methods that will be assigned to the delegate

        public static void Add(double num1, double num2)
        {
            Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
        }

        public static void Subtract(double num1, double num2)
        {
            Console.WriteLine($"{num1} - {num2} = {num1 - num2}");
        }
    }
}
