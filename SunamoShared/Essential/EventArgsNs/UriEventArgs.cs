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