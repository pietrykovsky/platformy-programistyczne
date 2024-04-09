using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    public class BaseItemsContainer
    {
        public List<Item> Items { get; set; }

        public string ItemsToString()
        {
            var sb = new StringBuilder();
            var items = Items.OrderBy(i => i.Value/i.Weight);
            foreach (var item in items)
            {
                sb.AppendLine($"Id: {item.Id}, Value: {item.Value}, Weight: {item.Weight}");
            }
            return sb.ToString();
        }

        public List<string> ItemsToListOfStrings()
        {
            var items = new List<string>();
            foreach (var item in Items)
            {
                items.Add(item.ToString());
            }
            return items;
        }
    }
}
