namespace SunamoShared._sunamo.SunamoReflection;

internal class RH
{
    private static bool IsType(Type t1)
    {
        var t2 = typeof(Type);
        return t1.FullName == "System.RuntimeType" || t1 == t2;
    }

}
