using UnityEngine;

namespace Bounce.Framework
{
    static class SingletonService
    {
        static Transform s_servicesObjectTransform;

        public static Transform parent
        {
            get
            {
                if (s_servicesObjectTransform == null)
                {
                    s_servicesObjectTransform = new GameObject("Singletons").transform;
                    Object.DontDestroyOnLoad(s_servicesObjectTransform);
                }

                return s_servicesObjectTransform;
            }
        }
    }
}