namespace CleanCodeExercises.Tests.E10;

public class Order
{
    public Order(IReadOnlyCollection<OrderLine> lines)
        :this (Guid.NewGuid(), null, DateTime.UtcNow, lines)
    {
    }
    
    public Order(string description, IReadOnlyCollection<OrderLine> lines)
    :this (Guid.NewGuid(), description, DateTime.UtcNow, lines)
    {
    }
    
    public Order(Guid id, string description, DateTime createdOn, IReadOnlyCollection<OrderLine> lines)
    {
        Id = id;
        Description = description;
        CreatedOn = createdOn;
        Lines = lines;
    }

    public Guid Id { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public IReadOnlyCollection<OrderLine> Lines { get; private set; }

    
    
}

public class OrderLine
{
    public OrderLine(string product, int quantity, decimal price)
    {
        Product = product;
        Quantity = quantity;
        Price = price;
    }

    public string Product { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
}
