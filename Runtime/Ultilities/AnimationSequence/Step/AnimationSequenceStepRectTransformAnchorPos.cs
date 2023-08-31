using DG.Tweening;
using UnityEngine;

namespace Bounce.Framework
{
    public class AnimationSequenceStepRectTransformAnchorPos : AnimationSequenceStepRectTransform
    {
        [SerializeField]
        private bool _snapping = false;

        public override string displayName { get { return $"{(_isSelf ? "RectTransform (This)" : _owner)}: Move To {(_isUseTarget ? (_target == null ? "Null" : _target.name) : _value)}"; } }

        protected override Tween GetTween(AnimationSequence animationSequence)
        {
            RectTransform owner = _isSelf ? animationSequence.rectTransform : _owner;

            Tween tween;

            Vector2 start;
            Vector2 end;

            if (_isUseTarget)
            {
                float duration = _isSpeedBased ? Vector2.Distance(_target.anchoredPosition, owner.anchoredPosition) / _duration : _duration;
                start = owner.anchoredPosition;
                end = _target.anchoredPosition;

                tween = owner.DOAnchorPos(end, duration, _snapping)
                             .ChangeStartValue(start);
            }
            else
            {
                float duration = _isSpeedBased ? Vector2.Distance(_value, owner.anchoredPosition) / _duration : _duration;
                start = owner.anchoredPosition;
                end = _relative ? owner.anchoredPosition + (Vector2)_value : _value;

                tween = owner.DOAnchorPos(end, duration, _snapping)
                             .ChangeStartValue(start);
            }

            owner.anchoredPosition = end;

            return tween;
        }

        protected override Tween GetResetTween(AnimationSequence animationSequence)
        {
            RectTransform owner = _isSelf ? animationSequence.rectTransform : _owner;

            return owner.DOAnchorPos(owner.anchoredPosition, 0.0f);
        }
    }
}