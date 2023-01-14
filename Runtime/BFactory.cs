using UnityEngine;

namespace Bounce.Framework
{
    public class BFactory : ScriptableObjectSingleton<BFactory>
    {
        [Header("Scene Transition")]
        [SerializeField] float _sceneTransitionFadeInDuration = 0.2f;
        [SerializeField] float _sceneTransitionLoadDuration = 0.1f;
        [SerializeField] float _sceneTransitionFadeOutDuration = 0.2f;
        [SerializeField] GameObject _sceneTransitionPrefab;

        public static float sceneTransitionFadeInDuration => instance._sceneTransitionFadeInDuration;
        public static float sceneTransitionLoadDuration => instance._sceneTransitionLoadDuration;
        public static float sceneTransitionFadeOutDuration => instance._sceneTransitionFadeOutDuration;
        public static GameObject sceneTransitionPrefab => instance._sceneTransitionPrefab;
    }
}