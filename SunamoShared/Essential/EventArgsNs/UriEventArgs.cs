// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared.Essential.EventArgsNs;

public class UriEventArgs : EventArgs
{
    private Uri _uri = null;

    public UriEventArgs(Uri uri)
    {
        _uri = uri;
    }

    public Uri Uri { get { return _uri; } }
}
