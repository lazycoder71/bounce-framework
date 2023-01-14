using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Bounce.Framework
{
    public class UIGraphicsScale : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Reference")]
        [SerializeField] Transform _target;

        [Header("Config")]
        [SerializeField] Vector2 _scaleValue = new Vector2(1f, 0.9f);
        [Min(0.1f)]
        [SerializeField] float _scaleSpeed = 1f;

        bool _isDown = false;
        Tween _tween;

        #region Monobehaviour

        void Awake()
        {
            if (_target == null)
                _target = transform;

            _target.SetScaleXY(_scaleValue.x);

            InitTween();
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #endregion

        #region Play Tween

        void InitTween()
        {
            float duration = Mathf.Abs(_scaleValue.x - _scaleValue.y) / _scaleSpeed;

            _tween = _target.DOScale(_scaleValue.y, duration)
                .SetAutoKill(false)
                .SetUpdate(true);

            _tween.Restart();
            _tween.Pause();
        }

        void ScaleUp()
        {
            _tween.PlayForward();
        }

        void ScaleDown()
        {
            _tween.PlayBackwards();
        }

        #endregion

        #region Pointer events

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            _isDown = true;

            ScaleUp();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (_isDown)
                ScaleDown();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (_isDown)
                ScaleUp();
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            _isDown = false;

            ScaleDown();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
        }

        #endregion
    }
}