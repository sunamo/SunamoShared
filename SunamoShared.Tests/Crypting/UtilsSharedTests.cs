using SunamoShared.Crypting;

public class UtilsSharedTests
{
    [Fact]
    public void Base64Test()
    {
        var b = CAG.ToList<byte>(101,
224,
213,
172,
187,
31,
40,
224,
29,
94);
        var b64 = Utils.ToBase64(b);
        var s = Utils.FromBase64(b64);
        Assert.Equal<byte>(b, s);
    }
}
