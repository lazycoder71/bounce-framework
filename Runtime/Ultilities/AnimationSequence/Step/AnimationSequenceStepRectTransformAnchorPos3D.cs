using DG.Tweening;
using UnityEngine;

namespace Bounce.Framework
{
    public class AnimationSequenceStepRectTransformAnchorPos3D : AnimationSequenceStepRectTransform
    {
        [SerializeField]
        private bool _snapping = false;

        public override string displayName { get { return $"{(_isSelf ? "RectTransform (This)" : _owner)}: Move To {(_isUseTarget ? (_target == null ? "Null" : _target.name) : _value)}"; } }

        protected override Tween GetTween(AnimationSequence animationSequence)
        {
            RectTransform owner = _isSelf ? animationSequence.rectTransform : _owner;

            Tween tween;

            Vector3 start;
            Vector3 end;

            if (_isUseTarget)
            {
                float duration = _isSpeedBased ? Vector3.Distance(_target.anchoredPosition3D, owner.anchoredPosition3D) / _duration : _duration;
                start = owner.anchoredPosition3D;
                end = _target.anchoredPosition3D;

                tween = owner.DOAnchorPos3D(end, duration, _snapping)
                             .ChangeStartValue(start);
            }
            else
            {
                float duration = _isSpeedBased ? Vector2.Distance(_value, owner.anchoredPosition3D) / _duration : _duration;
                start = owner.anchoredPosition3D;
                end = _relative ? owner.anchoredPosition3D + _value : _value;

                tween = owner.DOAnchorPos3D(end, duration, _snapping)
                             .ChangeStartValue(start);
            }

            animationSequence.rectTransform.anchoredPosition3D = end;

            return tween;
        }

        protected override Tween GetResetTween(AnimationSequence animationSequence)
        {
            RectTransform owner = _isSelf ? animationSequence.rectTransform : _owner;

            return owner.DOAnchorPos3D(owner.anchoredPosition3D, 0.0f);
        }
    }
}