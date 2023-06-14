using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace PFramework.Runtime
{
    public class UIGraphicScale : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Reference")]
        [SerializeField] Transform _tfTarget;

        [Header("Config")]
        [SerializeField] Vector2 _scaleValue = new Vector2(1f, 0.9f);
        [Min(0.1f)]
        [SerializeField] float _scaleSpeed = 1f;

        bool _isDown = false;

        Tween _tween;

        #region Monobehaviour

        void Awake()
        {
            if (_tfTarget == null)
                _tfTarget = transform;
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        private void OnDisable()
        {
            _tween?.Restart();
            _tween?.Kill();
            _tween = null;
        }

        #endregion

        #region Play Tween

        void InitTween()
        {
            if (_tween != null)
                return;

            float duration = Mathf.Abs(_scaleValue.x - _scaleValue.y) / _scaleSpeed;

            _tween = _tfTarget.DOScale(_scaleValue.y, duration)
                              .ChangeStartValue(Vector3.one * _scaleValue.x)
                              .SetAutoKill(false)
                              .SetUpdate(true);

            _tween.Restart();
            _tween.Pause();
        }

        void ScaleUp()
        {
            InitTween();

            _tween.PlayForward();
        }

        void ScaleDown()
        {
            InitTween();

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