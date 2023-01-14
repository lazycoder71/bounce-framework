namespace Bounce.Framework
{
    public interface IStateMachine
    {
        void Init();
        void OnStart();
        void OnUpdate();
        void OnStop();
    }
}