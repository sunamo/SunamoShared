namespace SunamoShared.Threading;

//public delegate void WaitCallback();
public interface IThreadPool
{
    bool QueueUserWorkItem(WaitCallback callBack);
    bool SetPoolSize(int size);
    int PoolSize { get; }
}
