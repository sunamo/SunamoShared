namespace SunamoShared._sunamo.SunamoInterfaces.Interfaces;


internal interface IFSItem : IName, IPath, IIDParent
{
    long Length { get; set; }
}