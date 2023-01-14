using System;

namespace Bounce.Framework
{
    /// <summary>
    /// The MonoCallback helper class is meant to be used when you need to have MonoBehaviour default Unity callbacks,
    /// but your model is not a MonoBehaviour and you dont want to convert it to the MonoBehaviour by design.
    ///
    /// Please note, that you will subscribe to the global MonoBehaviour singleton instance. Other parts of code may also use it.
    /// In case other callback users will throw and unhandled exception you may not received the callback you subscribed for.
    /// </summary>
    public class MonoCallback : MonoSingleton<MonoCallback>
    {
        /// <summary>
        /// Update is called every frame.
        /// Learn more: [MonoBehaviour.Update](https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html)
        /// </summary>
        public event Action eventUpdate;

        /// <summary>
        /// LateUpdate is called after all Update functions have been called. This is useful to order script execution.
        /// For example a follow camera should always be implemented in LateUpdate because it tracks objects that might have moved inside Update.
        /// Learn more: [MonoBehaviour.LateUpdate](https://docs.unity3d.com/ScriptReference/MonoBehaviour.LateUpdate.html)
        /// </summary>
        public event Action eventLateUpdate;

        /// <summary>
        /// In the editor this is called when the user stops playmode.
        /// Learn more: [MonoBehaviour.OnApplicationQuit](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationQuit.html)
        /// </summary>
        public event Action eventApplicationQuit;

        /// <summary>
        /// Sent to all GameObjects when the application pauses.
        /// Learn more: [MonoBehaviour.OnApplicationPause](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationPause.html)
        /// </summary>
        public event Action<bool> eventApplicationPause;

        /// <summary>
        /// Sent to all GameObjects when the player gets or loses focus.
        /// Learn more: [MonoBehaviour.OnApplicationFocus](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationFocus.html)
        /// </summary>
        public event Action<bool> eventApplicationFocus;

        /// <summary>
        /// Frame-rate independent MonoBehaviour.FixedUpdate message for physics calculations.
        /// Learn more: [MonoBehaviour.FixedUpdate](https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html)
        /// </summary>
        public event Action OnFixedUpdate;

        void Update() => eventUpdate?.Invoke();
        void LateUpdate() => eventLateUpdate?.Invoke();
        void FixedUpdate() => OnFixedUpdate?.Invoke();
        void OnApplicationPause(bool pauseStatus) => eventApplicationPause?.Invoke(pauseStatus);
        void OnApplicationFocus(bool hasFocus) => eventApplicationFocus?.Invoke(hasFocus);

        protected override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
            eventApplicationQuit?.Invoke();
        }
    }
}
