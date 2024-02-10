using System;

namespace Bounce.Framework
{
    interface IMainThreadDispatcher
    {
        void Init();
        void Enqueue(Action action);
    }
}
