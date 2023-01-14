using UnityEngine;

namespace Bounce.Framework
{
    public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static readonly string SOSFolderName = "SingletonScriptableObjects";

        static T s_instance = null;

        public static T instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = Resources.Load<T>(string.Format("{0}/{1}", SOSFolderName, typeof(T)));

#if UNITY_EDITOR
                    if (s_instance == null)
                    {
                        string configPath = string.Format("Assets/Resources/{0}/", SOSFolderName);
                        if (!System.IO.Directory.Exists(configPath))
                            System.IO.Directory.CreateDirectory(configPath);

                        s_instance = Editor.ScriptableObjectHelper.CreateAsset<T>(configPath, typeof(T).ToString());
                    }
#endif
                }

                return s_instance;
            }
        }
    }
}