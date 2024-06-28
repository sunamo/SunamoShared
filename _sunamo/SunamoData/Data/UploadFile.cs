namespace SunamoShared;


internal class UploadFile
{
    internal UploadFile()
    {
        ContentType = "application/octet-stream";
    }
    internal string Name { get; set; }
    internal string Filename { get; set; }
    internal string ContentType { get; set; }
    internal Stream Stream { get; set; }
    //internal long ContentLength { get; set; }
}