namespace SunamoShared;


internal interface IFSItem : IName, IPath, IIDParent
{
    long Length { get; set; }
}