using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_lab_recursion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 0;
            string strNum = Console.ReadLine();
            n = Int32.Parse(strNum);

            HelloWorld(n);
        }

        public static void HelloWorld(int n)
        {
            if (n == 0)
            {
                return;
            }

            Console.WriteLine("HelloWorld");
            HelloWorld(n - 1);
        }
    }
}
