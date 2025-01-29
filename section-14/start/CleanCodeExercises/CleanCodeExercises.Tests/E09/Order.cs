namespace CleanCodeExercises.Tests.E09;

public class Order
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public List<OrderLine> Lines { get; set; }
    public class OrderLine
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}