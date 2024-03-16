using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoShared._sunamo;
internal class RH
{
    private static bool IsType(Type t1)
    {
        var t2 = typeof(Type);
        return t1.FullName == "System.RuntimeType" || t1 == t2;
    }

    internal static List<FieldInfo> GetFields(object carSAuto)
    {
        Type carSAutoType = null;
        var t1 = carSAuto.GetType();

        if (IsType(t1))
        {
            carSAutoType = carSAuto as Type;
        }
        else
        {
            carSAutoType = carSAuto.GetType();
        }
        var result = carSAutoType.GetFields().ToList();
        return result;
    }
}
