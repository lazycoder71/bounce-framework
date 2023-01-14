using DG.Tweening;
using Bounce.Framework;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bounce.Framework
{
    public class AutoSequence : MonoCached
    {
        [Header("Config")]
        [SerializeReference] List<AutoStep> _steps = new List<AutoStep>();

        [HorizontalGroup("AutoSequence_Loop")]
        [SerializeField] int _loopTime;

        [HorizontalGroup("AutoSequence_Loop")]
        [ShowIf("@_loopTime != 0")]
        [LabelWidth(100f)]
        [SerializeField] LoopType _loopType;

        [Min(0f)]
        [SerializeField] float _delay;
        [SerializeField] UpdateType _updateType;
        [SerializeField] bool _ignoreTimeScale = false;
        [SerializeField] bool _autoKill = true;
        [SerializeField] bool _playOnAwake;

        Sequence _sequence;
        Tween _tween;
        RectTransform _rectTransform;
        Graphic _graphic;
        Rigidbody2D _rigidbody2D;
        CanvasGroup _canvasGroup;

        public Tween Tween { get { return _tween == null ? InitTween() : _tween; } }
        public List<AutoStep> Steps { get { return _steps; } }

        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        public Graphic Graphic
        {
            get
            {
                if (_graphic == null)
                    _graphic = GetComponent<Graphic>();
                return _graphic;
            }
        }

        public Rigidbody2D Rigidbody2D
        {
            get
            {
                if (_rigidbody2D == null)
                    _rigidbody2D = GetComponent<Rigidbody2D>();
                return _rigidbody2D;
            }
        }

        public CanvasGroup CanvasGroup
        {
            get
            {
                if (_canvasGroup == null)
                    _canvasGroup = GetComponent<CanvasGroup>();
                return _canvasGroup;
            }
        }

        #region MonoBehaviour

        void Start()
        {
            if (_playOnAwake)
                Tween.Play();
        }

        void OnDestroy()
        {
            _tween?.Kill();
            _sequence?.Kill();
        }

        /*
#if UNITY_EDITOR

        void OnDrawGizmos()
        {
            if (Application.isPlaying || !this.enabled)
                return;

            Vector3 lastPos = transformCached.localPosition;

            for (int i = 0; i < _steps.Count; i++)
            {
                Vector3 deltaPos = _steps[i].GetDeltaPosition();

                if (deltaPos != Vector3.zero)
                {
                    if (transformCached.parent == null)
                    {
                        PGizmos.DrawLine(lastPos, lastPos + deltaPos, Color.yellow);
                        PGizmos.DrawWireSphere(lastPos + deltaPos, 0.2f, Color.red);
                    }
                    else
                    {
                        PGizmos.DrawLine(transformCached.parent.TransformPoint(lastPos), transformCached.parent.TransformPoint(lastPos + deltaPos), Color.yellow);
                        PGizmos.DrawWireSphere(transformCached.parent.TransformPoint(lastPos + deltaPos), 0.1f, Color.red);
                    }

                    lastPos += deltaPos;
                }
            }
        }

#endif
        */

        #endregion

        Tween InitTween()
        {
            if (_tween != null)
                return _tween;

            _sequence = DOTween.Sequence();

            for (int i = 0; i < _steps.Count; i++)
            {
                AutoStep step = _steps[i];

                step.Construct(this, _sequence);
            }

            _sequence.SetLoops(_loopTime, _loopType)
              .SetAutoKill(_autoKill)
              .SetUpdate(_updateType, _ignoreTimeScale)
              .Restart();

            _sequence.Pause();

            if (_delay > 0)
                _tween = DOVirtual.DelayedCall(_delay, () => { _sequence?.Play(); }, _ignoreTimeScale);
            else
                _tween = _sequence;

            return _tween;
        }
    }
}