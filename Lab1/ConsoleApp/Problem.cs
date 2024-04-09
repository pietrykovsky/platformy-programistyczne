using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public class Problem : BaseItemsContainer
    {
        private int _itemsCount;
        private int _seed;

        public Problem(int itemsCount, int seed)
        {
            _seed = seed;
            _itemsCount = itemsCount;
            Items = _getRandomItems();
        }

        private List<Item> _getRandomItems()
        {
            Random random = new Random(_seed);
            var items = new List<Item>(_itemsCount);
            for (int i = 1; i <= _itemsCount; i++)
            {
                var item = new Item(i, random.Next(1, 11), random.Next(1, 11));
                items.Add(item);
            }
            return items;
        }

        public Result Solve(int capacity)
        {
            int[,] dp = new int[Items.Count + 1, capacity + 1];

            for (int i = 1; i <= Items.Count; i++)
            {
                for (int w = 1; w <= capacity; w++)
                {
                    if (Items[i - 1].Weight <= w)
                    {
                        dp[i, w] = Math.Max(dp[i - 1, w], dp[i - 1, w - Items[i - 1].Weight] + Items[i - 1].Value);
                    }
                    else
                    {
                        dp[i, w] = dp[i - 1, w];
                    }
                }
            }

            // Backtrack to find which items to include
            List<Item> resultItems = new List<Item>();
            int capacityLeft = capacity;
            for (int i = Items.Count; i > 0 && capacityLeft > 0; i--)
            {
                if (dp[i, capacityLeft] != dp[i - 1, capacityLeft])
                {
                    resultItems.Add(Items[i - 1]);
                    capacityLeft -= Items[i - 1].Weight;
                }
            }
            resultItems = resultItems.OrderBy(i => i.Id).ToList();

            return new Result(resultItems);
        }
    }
}
