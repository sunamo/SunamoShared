namespace
#if SunamoFileSystem
SunamoFileSystem
#else
SunamoShared
#endif
;
public class GetFilesEveryFolder : GetFilesMoreMascArgs
{
    public bool _trimA1 = false;
}