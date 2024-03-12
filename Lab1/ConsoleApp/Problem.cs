namespace ConsoleApp;

public class Problem
{
    private int _itemsCount;
    private int _seed;
    public List<Item> Items { get; set; }

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
        var result = new Result(new List<Item>());
        return result;
    }
}
