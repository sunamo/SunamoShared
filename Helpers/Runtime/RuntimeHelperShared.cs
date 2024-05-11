namespace SunamoShared;

public partial class RuntimeHelper{ 
public static bool IsAdminUser()
    {
        return Directory.Exists(@"E:\vs\sunamo\");
    }

    
}
