using DG.Tweening;
using UnityEngine;

namespace Bounce.Framework
{
    public class UIPopupTransitionMove : UIPopupTransition
    {
        [Header("Reference")]
        [SerializeField] RectTransform _target = null;

        [Header("Config")]
        [SerializeField] Vector2 _startPos = Vector2.zero;
        [SerializeField] Vector2 _endPos = Vector2.zero;
        [SerializeField] Ease _ease = Ease.OutBack;
        [Range(0.1f, 1f)]
        [SerializeField] float _durationRatio = 1f;

        public override Tween ConstructTransition(UIPopupBehaviour popup)
        {
            if (_target == null)
                _target = GetComponent<RectTransform>();

            _target.anchoredPosition = _startPos;

            return _target.DOAnchorPos(_endPos, popup.openDuration * _durationRatio)
                .SetEase(_ease);
        }
    }
}