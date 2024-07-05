
namespace SunamoShared.Http;

public static class HttpRequestHelper
{
    /// <summary>
    /// In earlier time return ext
    /// Now return whether was downloaded
    /// 
    /// Musím předávat Uri a ne string na A2 kvůli jiné metodě která na na A2 cacheFolder
    /// </summary>
    /// <param name="path"></param>
    /// <param name="uri"></param>
    /// <returns></returns>
    public static
#if ASYNC
        async Task<string>
#else
    string
#endif
        DownloadOrRead(string path, Uri uri, DownloadOrReadArgs a = null)
    {
        if (a == null)
        {
            a = new DownloadOrReadArgs();
        }

        string html = null;

        if (!File.Exists(path) || a.forceDownload)
        {
            Download(uri.ToString(), null, path);
        }

        html =
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(path);

        return html;
    }

    /*
     * Měl jsem tu i metodu jež vracela fn
     *
     */
    //public static string DownloadOrRead(string uri)
    //{
    //    string fn = null;
    //    return DownloadOrRead(ref fn, uri);
    //}

    /// <summary>
    /// As folder is use Cache
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="uri"></param>
    public static
#if ASYNC
        async Task<string>
#else
    string
#endif
        DownloadOrRead(/*ref string fn,*/ string uri, string cacheFolder, DownloadOrReadArgs a = null)
    {
        #region Část kódu kretá se používala když jsem vracel fn
        //var v = UH.GetFileNameWithoutExtension(uri);
        //var qs = UH.GetQueryAsHttpRequest(new Uri( uri));
        //fn = FS.ReplaceInvalidFileNameChars(v + qs);
        #endregion

        var uriShort = ShortPathFromUri(uri);

        var fn = Path.Combine(cacheFolder, SH.AppendIfDontEndingWith(uriShort, AllExtensions.html));
        return
#if ASYNC
            await
#endif
                DownloadOrRead(fn, uri, a);
    }

    public static bool ExistsPage(string url)
    {
        try
        {
            //Creating the HttpWebRequest
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //Setting the Request method HEAD, you can also use GET too.
            request.Method = "HEAD";
            //Getting the Web Response.
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //Returns TRUE if the Status code == 200
            response.Close();
            return (response.StatusCode == HttpStatusCode.OK);
        }
        catch
        {
            //Any exception will returns false.
            return false;
        }
    }

    /// <summary>
    /// Is not async coz t.Result
    /// </summary>
    /// <param name="address"></param>
    public static
#if ASYNC
        async Task<string>
#else
        string
#endif
        GetResponseText(string address)
    {
        var request = (HttpWebRequest)WebRequest.CreateHttp(address);
        request.Timeout = int.MaxValue;
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11";
        var t =

            request.GetResponseAsync();
        //HttpWebResponse response = null;
        // var response = (HttpWebResponse)t.Result;
        WebResponse result2 =
#if ASYNC
            await t;
#else
            AsyncHelper.ci.GetResult<WebResponse>(t);
#endif


        if (false)
        {
            //Encoding encoding = null;

            //if (response.CharacterSet == "")
            //{
            //    //encoding = Encoding.UTF8;
            //}
            //else
            //{
            //    encoding = Encoding.GetEncoding(response.CharacterSet);
            //}

            //using (var responseStream = response.GetResponseStream())
            //{
            //    StreamReader reader = null;
            //    if (encoding == null)
            //    {
            //        reader = new StreamReader(responseStream, true);
            //    }
            //    else
            //    {
            //        reader = new StreamReader(responseStream, encoding);
            //    }
            //    string vr = reader.ReadToEnd();

            //    //response.Dispose();
            //    result2.Dispose();

            //    return vr;
            //}
        }

        var ts = result2.ToString();
        return ts;

    }

    /// <summary>
    /// Same url:
    /// HttpClientHelper.GetResponseText - Exception: The remote server returned an error: (400) Bad Request., response is null
    /// HttpClientHelper.GetResponseText - really xml, exists response
    /// Pros: Better is HttpClientHelper because I can parse error
    /// Cons: HttpClientHelper.GetResponseText not return HttpWebResponse object, only HttpResponseMessage

    /// </summary>
    /// <param name="address"></param>
    /// <param name="method"></param>
    /// <param name="hrd"></param>
    public static string GetResponseText(string address, HttpMethod method, HttpRequestData hrd = null)
    {
        HttpWebResponse response;
        return GetResponseText(address, method, hrd, out response);
    }



    /// <param name = "address"></param>
    public static Stream GetResponseStream(string address, HttpMethod method)
    {
        var request = (HttpWebRequest)WebRequest.Create(address);
        request.Method = method.Method;
        HttpWebResponse response = null;
        try
        {
            response = (HttpWebResponse)request.GetResponse();
        }
        catch (System.Exception)
        {
            return null;
        }

        return response.GetResponseStream();
    }

    public static string GetResponseText(string address, HttpMethod method, HttpRequestData hrd, out HttpWebResponse response)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
        return GetResponseText(request, method, hrd, out response);
    }

    /// <summary>
    /// A3 can be null
    /// Dont forger Dispose on A4
    /// </summary>
    /// <param name = "address"></param>
    /// <param name = "method"></param>
    /// <param name = "hrd"></param>
    public static string GetResponseText(HttpWebRequest request, HttpMethod method, HttpRequestData hrd, out HttpWebResponse response)
    {
        NetHelperSunamo.NEVER_EAT_POISON_Disable_CertificateValidation();

        response = null;

        if (hrd == null)
        {
            hrd = new HttpRequestData();
        }

        var address = request.Address.ToString();

        int dex = address.IndexOf(AllChars.q);
        string adressCopy = address;
        if (method.Method.ToUpper() == "POST")
        {
            if (dex != -1)
            {
                address = address.Substring(0, dex);
            }
        }

        // Cant create new instance, in A1 can be setted up property which is not allowed in Headers
        //request.Address = address;

        string result = null;

        request.Method = method.Method;

        if (method == HttpMethod.Post)
        {
            string query = adressCopy.Substring(dex + 1);
            Encoding encoder = null;
            if (hrd.encodingPostData == null)
            {
                encoder = new ASCIIEncoding();
            }
            else
            {
                encoder = hrd.encodingPostData;
            }

            byte[] data = encoder.GetBytes(query);
            request.ContentType = "application/x-www-urlencoded";
            request.ContentLength = data.Length;
            request.GetRequestStream().Write(data, 0, data.Length);
        }

        //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11";
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36";
        if (hrd.contentType != null)
        {
            request.ContentType = hrd.contentType;
        }

        if (hrd.accept != null)
        {
            request.Accept = hrd.accept;
        }

        if (hrd != null)
        {
            foreach (var item in hrd.headers)
            {
                request.Headers.Add(item.Key, item.Value);
            }
        }

        WebResponse wr = null;

        try
        {
            wr = request.GetResponse();
            response = (HttpWebResponse)wr;

            Encoding encoding = null;
            if (response.CharacterSet == "")
            {
                //encoding = Encoding.UTF8;
            }
            else
            {
                encoding = Encoding.GetEncoding(response.CharacterSet);
            }

            using (var responseStream = response.GetResponseStream())
            {
                StreamReader reader = null;
                if (hrd.forceEndoding.HasValue)
                {
                    if (hrd.forceEndoding.GetValueOrDefault())
                    {
                        reader = new StreamReader(responseStream, hrd.forcedEncoding);
                    }
                    else
                    {
                        if (encoding == null)
                        {
                            reader = new StreamReader(responseStream, true);
                        }
                        else
                        {
                            reader = new StreamReader(responseStream, encoding);
                        }
                    }
                }
                else
                {
                    reader = new StreamReader(responseStream, true);
                }

                result = reader.ReadToEnd();
            }

        }
        catch (System.Exception ex)
        {
            if (hrd.throwEx)
            {
                result = Exceptions.TextOfExceptions(ex) + AllStrings.space + request.RequestUri.ToString();
            }
        }

        return result;
    }

    /// <summary>
    /// If return empty array, SharedAlgorithms.lastError contains HttpError
    /// </summary>
    /// <param name = "address"></param>
    public static byte[] GetResponseBytes(string address, HttpMethod method, int timeoutInMs = 30000)
    {
        var request = (HttpWebRequest)WebRequest.Create(address);
        request.Method = method.Method;
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11";
        WebResponse r = null;

        int times = 5;


        r = SharedAlgorithms.RepeatAfterTimeXTimes<WebResponse>(times, timeoutInMs, new Func<WebResponse>(request.GetResponse));
        if (EqualityComparer<WebResponse>.Default.Equals(r, default(WebResponse)))
        {
            var before = ThrowEx.FullNameOfExecutedCode(type, Exc.CallingMethod());
            ThisApp.Warning(Exceptions.RepeatAfterTimeXTimesFailed(before, times, timeoutInMs, address, SharedAlgorithms.lastError));
            return new byte[0];
        }
        else
        {
            HttpWebResponse response = (HttpWebResponse)r;
            using (response)
            {
                Encoding encoding = null;
                if (response.CharacterSet == "")
                {
                    encoding = Encoding.UTF8;
                }
                else
                {
                    encoding = Encoding.GetEncoding(response.CharacterSet);
                }

                using (var responseStream = response.GetResponseStream())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        responseStream.CopyTo(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        using (var reader = new StreamReader(ms, encoding))
                        {
                            using (BinaryReader br = new BinaryReader(reader.BaseStream))
                            {
                                return br.ReadBytes((int)ms.Length);
                            }
                        }
                    }
                }
            }
        }
    }



    public static string BeforeTestingIpAddress(string vr)
    {
        if (vr == "::1")
        {
            vr = Consts.sunamoNetIp;
        }

        return vr;
    }

    public static IProgressBarShared clpb = null;

    public static bool IsNotFound(object uri)
    {
        HttpWebResponse r;
        var test = GetResponseText(uri.ToString(), HttpMethod.Get, null, out r);

        return HttpResponseHelper.IsNotFound(r);
    }

    public static bool SomeError(object uri)
    {
        HttpWebResponse r;
        var test = GetResponseText(uri.ToString(), HttpMethod.Get, null, out r);

        return HttpResponseHelper.SomeError(r);
    }

    static Type type = typeof(HttpRequestHelper);

    /// <summary>
    /// A2 can be null (if dont have duplicated extension, set null)
    /// </summary>
    /// <param name="hrefs"></param>
    /// <param name="DontHaveAllowedExtension"></param>
    /// <param name="folder2"></param>
    /// <param name="co"></param>
    /// <param name="ext"></param>
    public static int DownloadAll(List<string> hrefs, Func<string, bool> DontHaveAllowedExtension, string folder2, FileMoveCollisionOptionShared co, string ext = "")
    {
        int reallyDownloaded = 0;

        clpb.LyricsHelper_OverallSongs(hrefs.Count);

        foreach (var item in hrefs)
        {
            clpb.LyricsHelper_AnotherSong();

            var tempPath = FS.GetTempFilePath();

            var partsUri = SHSplit.Split(item, AllStrings.slash);

            var to = Path.Combine(folder2, string.Join(AllStrings.bs, partsUri.TakeLast(2)) + ext);

            switch (co)
            {
                case FileMoveCollisionOptionShared.AddSerie:
                case FileMoveCollisionOptionShared.AddFileSize:
                case FileMoveCollisionOptionShared.Overwrite:
                case FileMoveCollisionOptionShared.DiscardFrom:
                case FileMoveCollisionOptionShared.LeaveLarger:
                    break;
                case FileMoveCollisionOptionShared.DontManipulate:
                    if (File.Exists(to))
                    {
                        continue;
                    }
                    break;
                default:
                    ThrowEx.NotImplementedCase(co);
                    break;
            }

            Download(item, DontHaveAllowedExtension, tempPath);
            reallyDownloaded++;

            FS.MoveFile(tempPath, to, co);
        }

        clpb.LyricsHelper_WriteProgressBarEnd();

        return reallyDownloaded;
    }

    /// <summary>
    /// A2 can be null (if dont have duplicated extension, set null)
    /// In earlier time return ext
    /// Now return whether was downloaded
    /// </summary>
    /// <param name = "href"></param>
    /// <param name = "DontHaveAllowedExtension"></param>
    /// <param name = "folder2"></param>
    /// <param name = "fn"></param>
    /// <param name = "ext"></param>
    public static async Task<bool> Download(string href, Func<string, bool> DontHaveAllowedExtension, string folder2, string fn, int timeoutInMs, string ext = null)
    {
        // TODO: měl jsem tu arg , string fullPathForCompare
        // zkontrolovat zda se tu ta cesta skládá správně
        // přidání nového arg do prostřed to zurví spoustu dalšího kódu

        if (DontHaveAllowedExtension != null)
        {
            if (DontHaveAllowedExtension(ext))
            {
                ext += ".jpeg";
            }
        }

        if (string.IsNullOrWhiteSpace(ext))
        {
            ext = Path.GetExtension(href);
            ext = SHParts.RemoveAfterFirst(ext, AllChars.q);
        }

        fn = SHParts.RemoveAfterFirst(fn, AllChars.q);
        string path = Path.Combine(folder2, fn + ext);

        //if (path != fullPathForCompare)
        //{
        //    System.Diagnostics.Debugger.Break();
        //}

        FS.CreateFoldersPsysicallyUnlessThere(folder2);

        if (!File.Exists(path) || new FileInfo(path).Length == 0)
        {
            var c = HttpRequestHelper.GetResponseBytes(href, HttpMethod.Get, timeoutInMs);

            if (c.Length != 0)
            {
                await File.WriteAllBytesAsync(path, c);
                return true;
            }
        }

        return false;
    }



    static string ShortPathFromUri(string s)
    {
        #region Nefungovalo, furt to bylo příliš dlouhé
        //s = SHParts.KeepAfterFirst(s, "://");
        //s = SHParts.KeepAfterFirst(s, "www.");
        //// Abych ušetřil ještě nějaké místo, nebudu vkládat ani host
        //s = SHParts.KeepAfterFirst(s, "/");

        /*
         * Z nějakého důvodu, když to dekóduji, tak mi C# nedokáže zapsat soubor s tímto názvem
         * Ale VSCode to zvládne v pohodě: \\?\D:\Documents\sunamo\ConsoleApp1\Cache\sprodejdomycena-do-1000000moravskoslezsky-krajs-qc[usableAreaMin]=40&s-qc[ownership][0]=personal&s-qc[condition][0]=new&s-qc[condition][1]=good-condition&s-qc[condition][2]=maintained&s-qc[condition][3]=after-reconstruction.html
         * Píše to že část cesty neexistuje ale žádné \ ani / v tom není a D:\Documents\sunamo\ConsoleApp1\Cache\ existuje
         *
         * Kdybych měl s tím znovu problemy, udělat to že string se převede na hash
         * Případně že se zapíšou jen hodnoty parametrů, nikoliv jejich názvy, oddělené ,
         * */
        //s = UH.UrlDecode(s);

        //s = FS.ReplaceInvalidFileNameChars(s);
        #endregion

        #region Část kódu kretá se používala když jsem vracel fn
        var v = UH.GetFileNameWithoutExtension(s);
        var qs = UH.GetQueryAsHttpRequest(new Uri(s));

        StringBuilder sb = new StringBuilder();
        var p = SHSplit.Split(qs, "&");
        foreach (var item in p)
        {
            sb.Append(SHSplit.Split(item, "=")[1] + ",");
        }

        s = FS.ReplaceInvalidFileNameChars(v + sb.ToString());
        #endregion

        return s;
    }

    /// <summary>
    /// In earlier time return ext
    /// Now return whether was downloaded
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="DontHaveAllowedExtension"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static async Task<bool> Download(string uri, Func<string, bool> DontHaveAllowedExtension, string path)
    {
        string p, fn, ext;

        FS.GetPathAndFileNameWithoutExtension(path, out p, out fn, out ext);

        var ext2 = Path.GetExtension(path);
        var downloaded = await Download(uri, /*path,*/ null, p, fn, 1000, ext2);

        // TODO: měl jsem tu arg , string fullPathForCompare
        // zkontrolovat zda se tu ta cesta skládá správně
        // přidání nového arg do prostřed to zurví spoustu dalšího kódu

        return downloaded;
    }
}
