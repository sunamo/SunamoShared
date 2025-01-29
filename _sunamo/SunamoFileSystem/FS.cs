namespace SunamoShared._sunamo.SunamoFileSystem;

internal class FS
{





    //protected readonly static List<char> invalidFileNameChars = 
    internal static List<char> s_invalidFileNameChars = Path.GetInvalidFileNameChars().ToList();


    #region MakeUncLongPath

    #endregion



    internal static void CreateFoldersPsysicallyUnlessThere(string nad)
    {
        ThrowEx.IsNullOrEmpty("nad", nad);
        //ThrowEx.IsNotWindowsPathFormat("nad", nad);


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
            //ThisApp.Error(Translate.FromKey(XlfKeys.FileCanTBeDeleted) + ": " + item);
            return false;
        }
    }

}