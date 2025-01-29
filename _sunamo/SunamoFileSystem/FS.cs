namespace SunamoShared._sunamo.SunamoFileSystem;

internal class FS
{





    //protected readonly static List<char> invalidFileNameChars = 
    internal static List<char> s_invalidFileNameChars = Path.GetInvalidFileNameChars().ToList();


    #region MakeUncLongPath

    #endregion




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