using SunamoShared.Crypting;

public class UtilsSharedTests
{
    [Fact]
    public void Base64Test()
    {
        byte[] b = [101,
224,
213,
172,
187,
31,
40,
224,
29,
94];
        var b64 = Utils.ToBase64(b.ToList());
        var s = Utils.FromBase64(b64);
        Assert.Equal<byte>(b, s);
    }
}
