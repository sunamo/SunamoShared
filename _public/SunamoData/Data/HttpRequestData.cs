namespace SunamoShared._public.SunamoData.Data;


public class HttpRequestData
{
    public Dictionary<string, string> headers = new Dictionary<string, string>();
    public string contentType = null;
    public string accept = null;
    public Encoding encodingPostData;
    
    public bool? keepAlive = null;
    
    
    
    public HttpContent content = null;
    public int timeoutInS = 60;
    
    
    
    public bool? forceEndoding = false;
    
    
    
    public Encoding forcedEncoding = null;
    public bool throwEx = true;
}