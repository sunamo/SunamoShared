namespace SunamoShared;


internal interface IDW
{
    string SelectOfFolder(string rootFolder);
    string SelectOfFolder(Environment.SpecialFolder rootFolder);
}