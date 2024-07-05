namespace SunamoShared._sunamo.SunamoCompare;


internal interface ISunamoComparer<T>
{
    int Desc(T x, T y);
    int Asc(T x, T y);
}