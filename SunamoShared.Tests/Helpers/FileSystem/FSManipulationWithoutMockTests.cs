namespace sunamo.Tests.Helpers.FileSystem;

/// <summary>
/// 3 možnosti:
/// 1/ zabalit do csproj archívy 
/// 2/ mockovat 
/// 3/ úplně tyhle testy smazat
/// </summary>
public class FSManipulationWithoutMockTests
{
    //[Fact]
    public async Task GetFilesAsyncTest()
    {
        TestHelper.Init();

        var files = await FSGetFiles.GetFilesAsync(@"D:\_Test\sunamo\sunamo\Helpers\FileSystem\FS\GetFiles\", "*", SearchOption.AllDirectories);
        var f = 0;
    }

    //[Fact]
    public void GetFilesEveryFolder()
    {
        TestHelper.Init();

        var path = @"D:\_Test\EveryLine\EveryLine\SearchCodeElementsUC\";

        var mask = "*.csproj,*.cs";

        var d = FSGetFiles.GetFilesEveryFolder(logger, path, mask, SearchOption.AllDirectories);
        int i = 0;
    }

    //[Fact]
    public void GetFilesTest()
    {
        TestHelper.Init();

        var files = FSGetFiles.GetFilesEveryFolder(logger, @"D:\_Test\sunamo\sunamo\Helpers\FileSystem\FS\GetFiles\", "*", true);
        var f = 0;
    }

    //[Fact]
    public void GetFilesEveryFolderAsyncTest()
    {
        FS.TryDeleteDirectoryOrFile(@"E:\vs\Projects\PlatformIndependentNuGetPackages.cz\apps.sunamo.cz\_\Content");

        var folder = @"E:\vs\Projects\PlatformIndependentNuGetPackages.cz\";
        string mask = AllStrings.ast;
        var so = SearchOption.AllDirectories;
        var gfmo = new GetFilesEveryFolderArgs { deleteFromDriveWhenCannotBeResolved = true };

        var f = FSGetFiles.GetFilesEveryFolder(folder, mask, so);
        //var r = Task.Run<List<string>>(async () => FSGetFiles.GetFilesEveryFolder(folder, mask, so));
        //var f = r.Result;
        f.Sort();
        int i = 0;
    }



    //[Fact]
    public void DeleteSerieDirectoryOrCreateNewTest()
    {
        string folder = @"D:\_Test\sunamo\Helpers\FileSystem\DeleteSerieDirectoryOrCreateNew\";
        FS.DeleteSerieDirectoryOrCreateNew(folder);
    }

    //[Fact]
    public void AllExtensionsInFolders()
    {
        string folder = @"D:\_Test\sunamo\Helpers\FileSystem\FS\AllExtensionsInFolders\";
        var excepted = CA.ToListString(".html", ".bowerrc", ".php");
        var actual = FS.AllExtensionsInFolders(System.IO.SearchOption.TopDirectoryOnly, folder);
        Assert.Equal<string>(excepted, actual);
    }

    //[Fact]
    public void DeleteEmptyFilesTest()
    {
        string folder = @"D:\_Test\sunamo\Helpers\FileSystem\FS\DeleteEmptyFiles\";
        FS.DeleteEmptyFiles(folder, System.IO.SearchOption.TopDirectoryOnly);
        List<string> actual = FS.OnlyNamesNoDirectEdit(FSGetFiles.GetFilesEveryFolder(logger, folder));
        List<string> excepted = CA.ToListString("ab.txt", "DeleteEmptyFiles.zip");
        Assert.Equal(excepted, actual);

    }

    //[Fact]
    public void DeleteFilesWithSameContent()
    {
        string folder = @"D:\_Test\sunamo\Helpers\FileSystem\FS\DeleteFilesWithSameContent\";

        var files = FSGetFiles.GetFilesEveryFolder(logger, folder, "*.txt", System.IO.SearchOption.AllDirectories, new GetFilesEveryFolderArgs { _trimA1AndLeadingBs = true });
        FS.DeleteFilesWithSameContent(files);

        files = FSGetFiles.GetFilesEveryFolder(logger, folder, "*.txt", System.IO.SearchOption.AllDirectories, new GetFilesEveryFolderArgs { _trimA1AndLeadingBs = true });

        var filesExcepted = CA.ToListString(TestDataTxt.a, TestDataTxt.ab);
        Assert.Equal<string>(filesExcepted, files);
    }

    //[Fact]
    public void DeleteFilesWithSameContentBytes()
    {
        string folder = @"D:\_Test\sunamo\Helpers\FileSystem\FS\DeleteFilesWithSameContentBytes\";

        var files = FSGetFiles.GetFilesEveryFolder(logger, folder, "*.txt", System.IO.SearchOption.AllDirectories, new GetFilesEveryFolderArgs { _trimA1AndLeadingBs = false });
        FS.DeleteFilesWithSameContentBytes(files);

        files = FSGetFiles.GetFilesEveryFolder(logger, folder, "*.txt", System.IO.SearchOption.AllDirectories, new GetFilesEveryFolderArgs { _trimA1AndLeadingBs = true });

        var filesExcepted = CA.ToListString(TestDataTxt.a, TestDataTxt.ab);
        Assert.Equal<string>(filesExcepted, files);
    }

    //[Fact]
    public void DeleteAllEmptyDirectoriesTest()
    {
        string folder = @"D:\_Test\sunamo\sunamo\Helpers\FileSystem\FS\DeleteAllEmptyDirectories\";

        FS.DeleteAllEmptyDirectories(folder);


        int actual = FSGetFolders.GetFoldersEveryFolder(logger, folder, SearchOption.AllDirectories).Count;
        Assert.Equal(2, actual);
    }
}