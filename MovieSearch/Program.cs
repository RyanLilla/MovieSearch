using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("welcome to Movie Search!");
            Console.WriteLine();
            StartingUserInterface.CommandLoop();

            Console.WriteLine();
            Console.WriteLine("Thank you for using Movie Search!");
        }
    }
}
