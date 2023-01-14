using DG.Tweening;
using System;
using UnityEngine;

namespace Bounce.Framework
{
    public class AutoDestructObject : MonoCached
    {
        [Header("Config")]
        [SerializeField] float _delay = 0f;
        [SerializeField] bool _deactiveOnly = false;

        Tween _tween;

        public event Action OnDestruct;

        #region MonoBehaviour

        void OnEnable()
        {
            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(_delay, Destruct);
        }

        void OnDisable()
        {
            _tween?.Kill();
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #endregion

        void Destruct()
        {
            OnDestruct?.Invoke();

            if (_deactiveOnly)
                gameObjectCached.SetActive(false);
            else
                Destroy(gameObjectCached);
        }
    }
}