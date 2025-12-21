namespace SunamoShared;

public class WindowsSecurityHelper
{
    public static bool IsUserAdministrator()
    {
        bool isAdmin;
        try
        {
            var user = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(user);
            isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        catch (UnauthorizedAccessException)
        {
            isAdmin = false;
        }
        catch (Exception ex)
        {
            isAdmin = false;
        }

        return isAdmin;
    }
}