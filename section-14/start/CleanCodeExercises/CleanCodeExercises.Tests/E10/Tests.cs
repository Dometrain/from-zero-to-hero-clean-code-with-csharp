namespace CleanCodeExercises.Tests.E10;

public class Tests
{
    [Fact]
    public void Create_new_order_only_with_lines()
    {
        var _ = new Order(new[] { new OrderLine("P1", 31, 12) });
    }

    [Fact]
    public void Create_new_order_with_description()
    {
        var _ = new Order("Test Order", new[] { new OrderLine("P1", 31, 12) });
    }
}