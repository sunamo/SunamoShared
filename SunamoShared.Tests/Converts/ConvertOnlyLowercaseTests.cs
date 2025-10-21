// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

public class ConvertOnlyLowercaseTests
{
    [Fact]
    public void ToTest()
    {
        var input = "nY-aVdc_qu4";
        var expected = "n*y-a*vdc_qu4";

        ConvertOnlyLowercase.nextUpper = '*';

        var text = ConvertOnlyLowercase.To(input);
        Assert.Equal(expected, text);
    }

    [Fact]
    public void FromTest2()
    {
        var input = "n*y-a*vdc_qu4";
        var expected = "nY-aVdc_qu4";

        ConvertOnlyLowercase.nextUpper = '*';

        var text = ConvertOnlyLowercase.From(input);
        Assert.Equal(expected, text);
    }

}
