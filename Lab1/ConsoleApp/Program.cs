using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number of items: ");
            var itemsCount = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter seed: ");
            var seed = int.Parse(Console.ReadLine());

            var problem = new Problem(itemsCount, seed);
            PrintItemList(problem.Items);

            Console.WriteLine("Enter capacity: ");
            var capacity = int.Parse(Console.ReadLine());

            var result = problem.Solve(capacity);
            Console.Write(result.ToString());
        }

        static void PrintItemList(List<Item> items)
        {
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
