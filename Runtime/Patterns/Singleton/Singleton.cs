namespace Bounce.Framework
{
    /// <summary>
    /// Singleton pattern implementation.
    /// </summary>
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        static T s_instance;

        /// <summary>
        /// Returns a singleton class instance.
        /// </summary>
        public static T instance => s_instance ?? (s_instance = new T());
    }
}