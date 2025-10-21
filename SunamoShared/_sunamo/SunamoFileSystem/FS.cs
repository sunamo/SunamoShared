// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoShared._sunamo.SunamoFileSystem;

internal class FS
{





    //protected readonly static List<char> invalidFileNameChars = 
    internal static List<char> s_invalidFileNameChars = Path.GetInvalidFileNameChars().ToList();


    #region MakeUncLongPath

    #endregion




    internal static Action<string> DeleteFileMaybeLocked;










    internal static string DeleteWrongCharsInFileName(string filenameOrPath, bool isPath)
    {
        //return string.Concat(filename.Split(Path.GetInvalidFileNameChars()));

        List<char> invalidFileNameChars2 = null;

        if (isPath)
        {
            invalidFileNameChars2 = Path.GetInvalidPathChars().ToList();
        }
        else
        {
            invalidFileNameChars2 = Path.GetInvalidFileNameChars().ToList();
        }

        StringBuilder stringBuilder = new StringBuilder();
        foreach (char item in filenameOrPath)
        {
            if (!invalidFileNameChars2.Contains(item))
            {
                stringBuilder.Append(item);
            }
        }

        var result = stringBuilder.ToString();
        SH.FirstCharUpper(ref result);
        return result;
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