namespace SunamoShared.Helpers.Resource;

public partial class PicturesSunamo
{

    public static List<string> GetPicturesFiles(string path)
    {
        var masc = string.Join(AllStrings.semi, AllLists.BasicImageExtensions);
        return Directory.GetFiles(path, masc, SearchOption.TopDirectoryOnly).ToList();
    }


}
