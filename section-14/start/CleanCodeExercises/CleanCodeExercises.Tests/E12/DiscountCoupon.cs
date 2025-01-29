namespace CleanCodeExercises.Tests.E12;

public class DiscountCoupon
{

    public decimal Rate { get;private set; }

    public DiscountCoupon(decimal rate)
    {
        Rate = rate;
    }

}