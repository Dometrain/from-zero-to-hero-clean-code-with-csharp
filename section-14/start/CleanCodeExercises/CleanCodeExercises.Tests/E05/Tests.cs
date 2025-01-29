namespace CleanCodeExercises.Tests.E05;

public class Tests
{
    [Fact]
    public void Should_offer_200_for_Class_A_Plus()
    {
        var offerValue = Evaluate(EquipmentGrade.ClassAPlus);

        Assert.Equal(200, offerValue);
    }
    
    [Fact]
    public void Should_offer_100_for_Class_A()
    {
        var offerValue = Evaluate(EquipmentGrade.ClassA);

        Assert.Equal(100, offerValue);
    }
    
    [Fact]
    public void Should_offer_20_for_Class_B()
    {
        var offerValue = Evaluate(EquipmentGrade.ClassB);

        Assert.Equal(20, offerValue);
    }
    
    [Fact]
    public void Should_offer_20_for_Class_C()
    {
        var offerValue = Evaluate(EquipmentGrade.ClassC);

        Assert.Equal(20, offerValue);
    }

    private decimal Evaluate(EquipmentGrade grade)
    {
        switch (grade)
        {
            case EquipmentGrade.ClassAPlus:
                return 200;
                break;
            case EquipmentGrade.ClassA:
                return 100;
                break;
            default:
                return 20;
        }
    }

    private enum EquipmentGrade
    {
        ClassAPlus,
        ClassA,
        ClassB,
        ClassC,
    }
}