using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Word!!!");
            Employee e = new Employee();
            e.GetInfo();

            DerivedClass d = new DerivedClass();
            dynamic a = d.Calculation(1.5, 5.6);
            a = d.Calculation(1, 5);
            a = d.Calculation("Hi", "Hello");
            d.iMethod();

            DerivedClass dd = new DerivedClass(9);
            dd.normalMethod();
            dd.prop = 7;

            Iinterface test = new DerivedClass(5);
            Console.WriteLine(test.prop);

            List<Simple> lstP1 = new List<Simple>();
            Simple p1 = new Simple();
            lstP1.Add(p1);
            List<Complex> lstC1 = new List<Complex>();
            Complex c1 = new Complex();
            lstC1.Add(c1);

            Simple p2 = lstP1[0];
            Complex c2 = lstC1[0];
            p2.x = 10;
            c2.x = 10;
            Console.WriteLine(lstP1[0].x * c2.x + lstC1[0].x * p2.x); //Output : 100 lstC1[0].x=10, p2.x=10, c2.x=10, lstP1[0].x=0

            Dynamic d1 = new Dynamic();
            Dynamic d2 = new Dynamic();
            d1 = new Dynamic();
            Console.WriteLine(Dynamic.Count);

            Console.ReadLine();
        }

        struct Simple { public int x, y; }
        class Complex { public int x, y; }

        class Dynamic
        {
            public static int Count = 10;
            static Dynamic()
            {
                ++Count;
            }
        }

        protected class Person
        {
            protected string ssn = "1234-52-5847";
            protected string name = "John L Malgrine";

            public virtual void GetInfo()
            {
                Console.WriteLine("SSN:{0}", ssn);
                Console.WriteLine("Name:{0}", name);
            }
        }

        class Employee : Person
        {
            public string id = "5568";
            public override void GetInfo()
            {
                base.GetInfo();
                Console.WriteLine("Employee Id:{0}", id);
            }
        }

        public class normalClass
        {
            protected void concreteMethod(ref List<string> list)
            {
                list.Add("ConcreteMethod");
            }
        }

        public sealed class sealedClass : normalClass
        {
            public static List<string> GetStaticNameList()
            {
                List<string> list = new List<string>();
                list.Add("Varun");
                list.Add("Dinesh");
                list.Add("Suresh");
                list.Add("Ramesh");
                list.Add("Limesh");
                return list;
            }

            public List<string> GetNonStaticNameList()
            {
                List<string> list = new List<string>();
                concreteMethod(ref list);
                list.Add("Varun");
                list.Add("Dinesh");
                list.Add("Suresh");
                list.Add("Ramesh");
                list.Add("Limesh");
                return list;
            }
        }

        #region Compiler Error: Circular base class dependency involving 'Program.A' and 'Program.B'
        //public class A : B
        //{
        //    public void h1()
        //    {

        //    }
        //}

        //public class B : A
        //{
        //    public void h2()
        //    {

        //    }
        //}
        #endregion

        //Interface cannot contains fields. It only contains method declaration.
        interface Iinterface
        {
            int prop { get; set; }
            void iMethod();
            string sMethod();
        }

        public abstract class absClass
        {
            public abstract int pop();
        }

        public abstract class abstractClass : absClass, Iinterface
        {
            int i = 0;
            private readonly int x = 0;
            public int prop
            {
                get { return i + x; }
                set
                {
                    i = value;
                }
            }

            //Abstract class can have constructor declaration while interface does not have it
            public abstractClass(int z)
            {
                x = z;
            }

            public dynamic Calculation(dynamic a, dynamic b)
            {
                return a + b;
            }

            public abstract int intCalculation(int a, int b);
            public abstract float fltCalculation(float a, float b);

            public virtual void iMethod()
            {
                Console.WriteLine("Base Class: iMethod");
            }

            public string sMethod()
            {
                throw new NotImplementedException();
            }
        }

        //Static class cannot implement interfaces
        //Static class cannot be derived from instance class or other static class
        //In simple word, we cannot implement interface to static class, static class cannot be a base class and cannot be a derived class
        //Inheritance and polymorphism cannot achived by static class
        public static class staticClass
        {
            //protected static List<string> GetNameList() --static class cannot contains protected members
            public static List<string> GetNameList()
            {
                List<string> list = new List<string>();
                list.Add("Varun");
                list.Add("Dinesh");
                list.Add("Suresh");
                list.Add("Ramesh");
                list.Add("Limesh");
                return list;
            }
        }

        public static List<string> GeStringList()
        {
            List<string> list = new List<string>();
            list.Add("Varun");
            list.Add("Dinesh");
            list.Add("Suresh");
            list.Add("Ramesh");
            list.Add("Limesh");
            return list;
        }

        public class DerivedClass : abstractClass
        {

            public DerivedClass() : base(0)
            {
            }

            //Specify which base class contructor would be called when creating the instance of the derived class
            public DerivedClass(int z) : base(z)
            {

            }
            public override float fltCalculation(float a, float b)
            {
                throw new NotImplementedException();
            }

            public override int intCalculation(int a, int b)
            {
                throw new NotImplementedException();
            }

            //Below is the abtract method which was defined in absClass which is the base class of abstractClass
            public override int pop()
            {
                throw new NotImplementedException();
            }

            //Below method is defined in an Iinterface and used in abstractClass [base class of this class] but it was explicitly declared as virtual one hence here it can be override.
            public override void iMethod()
            {
                base.iMethod();//Calling base class iMethod. Call a method on the base class that has been overriden by the another method e.g. iMethod of derived class
                Console.WriteLine("Derived Class: iMethod");
            }

            public void normalMethod()
            {
                GeStringList();
                sealedClass.GetStaticNameList();
                sealedClass ss = new sealedClass();
                ss.GetNonStaticNameList();
                staticClass.GetNameList();
            }
        }
    }
}
