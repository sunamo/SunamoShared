namespace SunamoShared._public.SunamoExceptions.InSunamoIsDerivedFrom;


public class HttpUtilitySE
{
    
    
    
    
    
    
    
    
    public static NameValueCollection ParseQueryString(string queryString)
    {
        NameValueCollection queryParameters = new();
        var querySegments = queryString.Split('&');
        foreach (var segment in querySegments)
        {
            var parts = segment.Split('=');
            if (parts.Length > 0)
            {
                var key = parts[0].Trim('?', ' ');
                var val = parts[1].Trim();
                queryParameters.Add(key, val);
            }
        }
        return queryParameters;
    }
}
