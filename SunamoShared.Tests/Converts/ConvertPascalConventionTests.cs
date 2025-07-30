

public class ConvertPascalConventionTests
{
    string[] tests = {
           "AutomaticTrackingSystem",
           "XMLEditor",
           "AnXMLAndXSLT2.0Tool",
        };


    Regex r = new Regex(
        @"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])"
      );

    //    List<string> y = new List<string>();
    //    foreach (string s in tests)
    //    {
    //        var replaced = r.Replace(s, " ");
    //        y.Add(replaced);
    //    }
    //}


    [Fact]
    public void IsPascalWithNumberTest()
    {
        var input = @"HelloWorld";
        var input2 = @"HelloWorld2";
        var input3 = @"Hello World3";

        Assert.True(ConvertPascalConvention.IsPascal(input));
        Assert.False(ConvertPascalConvention.IsPascal(input2));
        Assert.False(ConvertPascalConvention.IsPascal(input3));
    }

    [Fact]
    public void FromToPascalWithNumbersTest()
    {
        var input = "hello world";
        var to = ConvertPascalConvention.ToConvention(input);
        var from = ConvertPascalConvention.FromConvention(to);

        input = SH.FirstCharUpper(input);

        Assert.Equal(input, from);
    }

    [Fact]
    public void IsPascalTest()
    {
        var input = @"HelloWorld";
        var input2 = @"helloWorld";
        var input3 = @"Hello World";

        Assert.True(ConvertPascalConvention.IsPascal(input));
        Assert.False(ConvertPascalConvention.IsPascal(input2));
        Assert.False(ConvertPascalConvention.IsPascal(input3));
    }

    [Fact]
    public void ToConventionTest()
    {
        var input = @"custom Field 2 - Value";
        var expected = "CustomField2Value";

        var tc = ConvertPascalConvention.ToConvention(input);

        Assert.Equal(expected, tc);
    }
}
