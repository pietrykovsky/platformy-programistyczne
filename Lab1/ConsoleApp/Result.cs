using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public class Result : BaseItemsContainer
    {
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
            var items = Items.OrderBy(i => i.Value/i.Weight);
            foreach (var item in items)
            {
                sb.AppendLine($"\t{item.ToString()}");
            }
            sb.AppendLine($"Total value: {TotalValue()}");
            sb.AppendLine($"Total weight: {TotalWeight()}");
            return sb.ToString();
        }
    }
}
