namespace ConsoleApp;

public class Item
{
    private int _id;
    public int Id { get => _id; }
    public int Value { get; set; }
    public int Weight { get; set; }

    public Item(int id, int value, int weight)
    {
        _id = id;
        Value = value;
        Weight = weight;
    }

    public void Print()
    {
        Console.WriteLine($"Id: {Id}, Value: {Value}, Weight: {Weight}");
    }
}
