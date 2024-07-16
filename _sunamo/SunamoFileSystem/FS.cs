namespace SunamoShared._sunamo.SunamoFileSystem;

internal class FS
{
    internal static byte[] StreamToArrayBytes(System.IO.Stream stream)
    {
        if (stream == null)
        {
            return new byte[0];
        }

        long originalPosition = 0;

        if (stream.CanSeek)
        {
            originalPosition = stream.Position;
            stream.Position = 0;
        }

        try
        {
            byte[] readBuffer = new byte[4096];

            int totalBytesRead = 0;
            int bytesRead;

            while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
            {
                totalBytesRead += bytesRead;

                if (totalBytesRead == readBuffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    if (nextByte != -1)
                    {
                        byte[] temp = new byte[readBuffer.Length * 2];
                        Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                        Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                        readBuffer = temp;
                        totalBytesRead++;
                    }
                }
            }

            byte[] buffer = readBuffer;
            if (readBuffer.Length != totalBytesRead)
            {
                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
            }
            return buffer;
        }
        finally
        {
            if (stream.CanSeek)
            {
                stream.Position = originalPosition;
            }
        }
    }
    internal static string GetTempFilePath()
    {
        return Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.GetTempFileName());
    }

    internal static void GetPathAndFileNameWithoutExtension(string fn, out string path, out string file, out string ext)
    {
        path = Path.GetDirectoryName(fn) + AllChars.bs;
        file = GetFileNameWithoutExtensions(fn);
        ext = Path.GetExtension(fn);
    }



    internal static string GetFileNameWithoutExtensions(string item)
    {
        while (Path.HasExtension(item))
        {
            item = Path.GetFileNameWithoutExtension(item);
        }
        return item;
    }

    //protected readonly static List<char> invalidFileNameChars = 
    internal static List<char> s_invalidFileNameChars = Path.GetInvalidFileNameChars().ToList();

    internal static string ReplaceInvalidFileNameChars(string filename, params char[] ch)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in filename)
        {
            if (!s_invalidFileNameChars.Contains(item) || ch.Contains(item))
            {
                sb.Append(item);
            }
        }
        return sb.ToString();
    }

    #region MakeUncLongPath
    internal static string MakeUncLongPath(string path)
    {
        return MakeUncLongPath(ref path);
    }

    internal static string MakeUncLongPath(ref string path)
    {
        if (!path.StartsWith(Consts.UncLongPath))
        {
            // V ASP.net mi vrátilo u každé directory.exists false. Byl jsem pod ApplicationPoolIdentity v IIS a bylo nastaveno Full Control pro IIS AppPool\DefaultAppPool
#if !ASPNET
            //  asp.net / vps nefunguje, ve windows store apps taktéž, NECHAT TO TRVALE ZAKOMENTOVANÉ
            // v asp.net toto způsobí akorát zacyklení, IIS začne vyhazovat 0xc00000fd, pak už nejde načíst jediná stránka
            //path = Consts.UncLongPath + path;
#endif
        }

        return path;
    }
    #endregion

    internal static string InsertBetweenFileNameAndExtension(string orig, string whatInsert)
    {
        //return InsertBetweenFileNameAndExtension<string, string>(orig, whatInsert, null);

        // Cesta by se zde hodila kvůli FS.CiStorageFile
        // nicméně StorageFolder nevím zda se používá, takže to bude umět i bez toho

        var origS = orig.ToString();

        string fn = Path.GetFileNameWithoutExtension(origS);
        string e = Path.GetExtension(origS);

        if (origS.Contains(AllChars.slash) || origS.Contains(AllChars.bs))
        {
            string p = Path.GetDirectoryName(origS);

            return Path.Combine(p, fn + whatInsert + e);
        }
        return fn + whatInsert + e;
    }

    internal static void CreateUpfoldersPsysicallyUnlessThere(string nad)
    {
        CreateFoldersPsysicallyUnlessThere(Path.GetDirectoryName(nad));
    }

    internal static void CreateFoldersPsysicallyUnlessThere(string nad)
    {
        ThrowEx.IsNullOrEmpty("nad", nad);
        ThrowEx.IsNotWindowsPathFormat("nad", nad);


        if (Directory.Exists(nad))
        {
            return;
        }

        List<string> slozkyKVytvoreni = new List<string>
{
nad
};

        while (true)
        {
            nad = Path.GetDirectoryName(nad);

            // TODO: Tady to nefunguje pro UWP/UAP apps protoze nemaji pristup k celemu disku. Zjistit co to je UWP/UAP/... a jak v nem ziskat/overit jakoukoliv slozku na disku
            if (Directory.Exists(nad))
            {
                break;
            }

            string kopia = nad;
            slozkyKVytvoreni.Add(kopia);
        }

        slozkyKVytvoreni.Reverse();
        foreach (string item in slozkyKVytvoreni)
        {
            string folder = item;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
    }

    internal static Action<string> DeleteFileMaybeLocked;

    internal static bool CopyMoveFilePrepare(ref string item, ref string fileTo, FileMoveCollisionOptionShared co)
    {
        //var fileTo = fileTo2.ToString();
        item = Consts.UncLongPath + item;
        MakeUncLongPath(ref fileTo);
        FS.CreateUpfoldersPsysicallyUnlessThere(fileTo);

        // Toto tu je důležité, nevím který kokot to zakomentoval
        if (File.Exists(fileTo))
        {
            if (co == FileMoveCollisionOptionShared.AddFileSize)
            {
                var newFn = FS.InsertBetweenFileNameAndExtension(fileTo, AllStrings.space + new FileInfo(item).Length);
                if (File.Exists(newFn))
                {
                    File.Delete(item);
                    return true;
                }
                fileTo = newFn;
            }
            else if (co == FileMoveCollisionOptionShared.AddSerie)
            {
                int serie = 1;
                while (true)
                {
                    var newFn = FS.InsertBetweenFileNameAndExtension(fileTo, " (" + serie + AllStrings.rb);
                    if (!File.Exists(newFn))
                    {
                        fileTo = newFn;
                        break;
                    }
                    serie++;
                }
            }
            else if (co == FileMoveCollisionOptionShared.DiscardFrom)
            {
                // Cant delete from because then is file deleting
                if (DeleteFileMaybeLocked != null)
                {
                    DeleteFileMaybeLocked(item);
                }
                else
                {
                    File.Delete(item);
                }

            }
            else if (co == FileMoveCollisionOptionShared.Overwrite)
            {
                if (DeleteFileMaybeLocked != null)
                {
                    DeleteFileMaybeLocked(fileTo);
                }
                else
                {
                    File.Delete(fileTo);
                }
            }
            else if (co == FileMoveCollisionOptionShared.LeaveLarger)
            {
                long fsFrom = new FileInfo(item).Length;
                long fsTo = new FileInfo(fileTo).Length;
                if (fsFrom > fsTo)
                {
                    File.Delete(fileTo);
                }
                else //if (fsFrom < fsTo)
                {
                    File.Delete(item);
                }
            }
            else if (co == FileMoveCollisionOptionShared.DontManipulate)
            {
                if (File.Exists(fileTo))
                {
                    return false;
                }
            }
        }

        return true;
    }

    internal static void MoveFile(string item, string fileTo, FileMoveCollisionOptionShared co)
    {
        if (CopyMoveFilePrepare(ref item, ref fileTo, co))
        {
            try
            {
                item = FS.MakeUncLongPath(item);
                fileTo = FS.MakeUncLongPath(fileTo);

                if (co == FileMoveCollisionOptionShared.DontManipulate && File.Exists(fileTo))
                {
                    return;
                }
                File.Move(item, fileTo);
            }
            catch (Exception ex)
            {
                //ThisApp.Error(item + " : " + ex.Message);
            }
        }
        else
        {
        }
    }

    internal static string WithoutEndSlash(string v)
    {
        return WithoutEndSlash(ref v);
    }




    internal static string WithoutEndSlash(ref string v)
    {
        v = v.TrimEnd(AllChars.bs);
        return v;
    }

    internal static string WithEndSlash(string v)
    {
        return WithEndSlash(ref v);
    }

    /// <summary>
    ///     Usage: Exceptions.FileWasntFoundInDirectory
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    internal static string WithEndSlash(ref string v)
    {
        if (v != string.Empty)
        {
            v = v.TrimEnd(AllChars.bs) + AllChars.bs;
        }

        SH.FirstCharUpper(ref v);
        return v;
    }

    internal static string DeleteWrongCharsInFileName(string filename, bool isPath)
    {
        return string.Concat(filename.Split(Path.GetInvalidFileNameChars()));

        //List<char> invalidFileNameChars2 = null;

        //if (isPath)
        //{
        //    invalidFileNameChars2 = SHData. s_invalidFileNameCharsWithoutDelimiterOfFolders;
        //}
        //else
        //{
        //    invalidFileNameChars2 = SHData.s_invalidFileNameChars;
        //}

        //StringBuilder sb = new StringBuilder();
        //foreach (char item in p)
        //{
        //    if (!invalidFileNameChars2.Contains(item))
        //    {
        //        sb.Append(item);
        //    }
        //}

        //var result = sb.ToString();
        //SH.FirstCharUpper(ref result);
        //return result;
    }

    internal static bool TryDeleteFile(string item)
    {
        // TODO: To all code message logging as here

        try
        {
            // If file won't exists, wont throw any exception
            File.Delete(item);
            return true;
        }
        catch
        {
            //ThisApp.Error(sess.i18n(XlfKeys.FileCanTBeDeleted) + ": " + item);
            return false;
        }
    }
}
