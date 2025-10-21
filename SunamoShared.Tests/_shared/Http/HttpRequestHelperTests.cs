// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

public class HttpRequestHelperTests
{
    [Fact]
    public void GetResponseTextTest()
    {

        var temp = HttpClientHelper.GetResponseText(@"https://ws.audioscrobbler.com/2.0/?method=artist.gettoptags&artist=Soundtrack+Ledov%C3%A9+kr%C3%A1lovstv%C3%AD+II&user=sunamoDevProg&api_key=68ae15739cd690ce04679a15b5583fd4", HttpMethod.Get, null);

        int i = 0;
    }
}
