using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public class Result
    {
        public List<Item> Items { get; set; }

        public Result(List<Item> items)
        {
            Items = items;
        }

        public int TotalValue() => Items.Sum(i => i.Value);

        public int TotalWeight() => Items.Sum(i => i.Weight);

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Items:");
            foreach (var item in Items)
            {
                sb.AppendLine($"\tId: {item.Id}, Value: {item.Value}, Weight: {item.Weight}");
            }
            sb.AppendLine($"Total value: {TotalValue()}");
            sb.AppendLine($"Total weight: {TotalWeight()}");
            return sb.ToString();
        }
    }
}
