namespace Bounce.Framework
{
    class MainThreadDispatcherRuntime : MainThreadDispatcherBase
    {
        public override void Init()
        {
            MonoCallback.instance.eventUpdate += Update;
        }
    }
}
