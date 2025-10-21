// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

using SunamoShared.Crypting;

public class UtilsSharedTests
{
    [Fact]
    public void Base64Test()
    {
        byte[] buffer = [101,
224,
213,
172,
187,
31,
40,
224,
29,
94];
        var b64 = Utils.ToBase64(buffer.ToList());
        var text = Utils.FromBase64(b64);
        Assert.Equal<byte>(buffer, text);
    }
}
