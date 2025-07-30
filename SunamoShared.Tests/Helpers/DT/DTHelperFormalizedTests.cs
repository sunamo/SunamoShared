namespace sunamo.Tests.Helpers.DT;

public class DTHelperFormalizedTests
{
    [Fact]
    public void IsFormalizedDateTest()
    {
        bool b = DTHelperFormalized.IsFormalizedDate("2022-09-26T11:57:57.8410000Z");
    }
}
