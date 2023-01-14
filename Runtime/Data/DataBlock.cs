using System;

namespace Bounce.Framework
{
    public class DataBlock<T> where T : DataBlock<T>
    {
        static T s_instance;

        public static T instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = DataHelper.Load<T>(typeof(T).ToString());
                    if (s_instance == null)
                        s_instance = (T)Activator.CreateInstance(typeof(T));

                    s_instance.Init();
                }

                return s_instance;
            }
        }

        protected virtual void Init()
        {
            MonoCallback.instance.eventApplicationPause += MonoCallback_ApplicationOnPause;
            MonoCallback.instance.eventApplicationQuit += MonoCallback_ApplicationOnQuit;
        }

        void MonoCallback_ApplicationOnQuit()
        {
            Save();
        }

        void MonoCallback_ApplicationOnPause(bool paused)
        {
            if (paused)
                Save();
        }

        public static void Save()
        {
            DataHelper.Save(instance, typeof(T).ToString());
        }

        public static void Delete()
        {
            s_instance = null;

            DataHelper.Delete(typeof(T).ToString());
        }
    }
}