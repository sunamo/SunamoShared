namespace SunamoShared;


#if ASYNC
internal delegate Task TaskVoid();
#else
internal delegate void TaskVoid();
#endif