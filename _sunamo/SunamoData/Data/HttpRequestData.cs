namespace SunamoShared;


internal class HttpRequestData
{
    internal Dictionary<string, string> headers = new Dictionary<string, string>();
    internal string contentType = null;
    internal string accept = null;
    internal Encoding encodingPostData;
    //internal int? timeout = null; // Není v třídě HttpKnownHeaderNames
    internal bool? keepAlive = null;
    /// <summary>
    /// Assign: StreamContent,ByteArrayContent,FormUrlEncodedContent,StringContent,MultipartContent,MultipartFormDataContent
    /// </summary>
    internal HttpContent content = null;
    internal int timeoutInS = 60;
    /// <summary>
    /// null for auto detect also when will be in headers available different value
    /// </summary>
    internal bool? forceEndoding = false;
    /// <summary>
    /// Must be set also forceEndoding = true
    /// </summary>
    internal Encoding forcedEncoding = null;
    internal bool throwEx = true;
}