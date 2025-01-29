namespace CleanCodeExercises.Tests.E12;

public class Order
{
    public Order(DiscountCoupon coupon, IReadOnlyCollection<OrderLine> lines)
        : this(Guid.NewGuid(), coupon, DateTime.UtcNow, lines)
    {
    }

    public Order(Guid id, DiscountCoupon coupon, DateTime createdOn, IReadOnlyCollection<OrderLine> lines)
    {
        Id = id;
        Coupon = coupon;
        CreatedOn = createdOn;
        Lines = lines;
    }

    public Guid Id { get; private set; }
    public DiscountCoupon Coupon { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public IReadOnlyCollection<OrderLine> Lines { get; private set; }


    public decimal GetTotal()
    {
        if (Coupon == null)
            return Lines.Sum(line => line.Price * line.Quantity);
        else
        {
            return Lines.Sum(line => line.Price * line.Quantity) * (1 - Coupon.Rate);
        }
    }
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