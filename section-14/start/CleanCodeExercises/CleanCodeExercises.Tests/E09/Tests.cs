namespace CleanCodeExercises.Tests.E09;

public class Tests
{
    [Fact]
    public void Create_new_order()
    {
        var order = new Order()
        {
            Id = Guid.NewGuid(),
            CreatedOn = DateTime.UtcNow,
            Lines = new List<Order.OrderLine>()
        };
        order.Lines.Add(new Order.OrderLine()
        {
            Id = Guid.NewGuid(),
            Product = "P01",
            Price = 12.34M,
            Quantity = 3
        });
    }
}