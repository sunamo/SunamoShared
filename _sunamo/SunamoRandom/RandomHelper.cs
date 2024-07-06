namespace SunamoShared._sunamo.SunamoRandom;
internal class RandomHelper
{
    private static Random s_rnd = new Random(Guid.NewGuid().GetHashCode());

    internal static byte[] RandomBytes(int kolik)
    {
        byte[] b = new byte[kolik];
        for (int i = 0; i < kolik; i++)
        {
            b[i] = (byte)s_rnd.Next(0, byte.MaxValue);
        }
        return b;
    }
}
