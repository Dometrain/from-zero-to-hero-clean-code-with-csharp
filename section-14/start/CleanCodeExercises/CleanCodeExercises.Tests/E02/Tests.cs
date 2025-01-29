namespace CleanCodeExercises.Tests.E02;

public class Tests
{
    [Theory]
    [InlineData(1, "I")]
    [InlineData(3, "III")]
    [InlineData(4, "IV")]
    [InlineData(5, "V")]
    [InlineData(8, "VIII")]
    [InlineData(10, "X")]
    public void Should_return_roman_number_for_arabic_number(int number, string expectedRomanNumber)
    {
        Assert.Equal(expectedRomanNumber, ToRoman(number));
    }


    static readonly Rule[] Rules = new Rule[]
    {
        new Rule(1000, "M"),
        new Rule(900, "CM"),
        new Rule(500, "D"),
        new Rule(400, "CD"),
        new Rule(100, "C"),
        new Rule(90, "XC"),
        new Rule(50, "L"),
        new Rule(40, "XL"),
        new Rule(10, "X"),
        new Rule(9, "IX"),
        new Rule(5, "V"),
        new Rule(4, "IV"),
        new Rule(1, "I"),
    };

    /// <summary>
    /// Convert Number to Roman number
    /// </summary>
    /// <param name="number">arabic number</param>
    /// <returns>roman number</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string ToRoman(int number)
    {
        /*
         * 2024-02-28: Gui - Created
         * 2024-03-03: Gui - Fix: add exception
         */

        if (number == 0) return ""; // Recursive termination

        foreach (var rule in Rules) // Rules are in descending order
        {
            if (rule.Number <= number)
            {
            return rule.Symbol + ToRoman(number - rule.Number); // Recurse
            } //end if
        } // end foreach

        // If this line is reached then n < 0
        throw new ArgumentOutOfRangeException();
    }

    // This Rule represents a substitution rule for a roman-numerals like numerical system
    // Number is equal to string 'Symbol' in the system.
    record Rule(int Number, string Symbol);
}