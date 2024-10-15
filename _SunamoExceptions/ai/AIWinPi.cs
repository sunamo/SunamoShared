using SunamoExceptions.Interfaces;

namespace SunamoExceptions.ai;
public class AIWinPi : IAIWinPi
{
    public Action<string> PHWinPiRunAsDesktopUserNoAdmin { get; set; }
}
