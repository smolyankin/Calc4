using System;

namespace Calc4
{
    class Program
    {
        static void Main(string[] args)
        {
            var example = "3*(1.25+4.25)/5-0.3-2*(18.6-17.6)";
            Console.Write($"Example: {example}\r\n");

            while (true)
            {
                Console.Write($"Press \"E\" to use example or any key to skip\r\n");
                var key = Console.ReadKey();
                Console.Write("\r\n");

                var input = key.Key == ConsoleKey.E
                    ? example
                    : Console.ReadLine();

                try
                {
                    var result = Calculator.Execute(input);

                    Console.WriteLine("Result: " + result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
