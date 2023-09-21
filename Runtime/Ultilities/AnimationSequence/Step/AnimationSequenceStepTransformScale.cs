using DG.Tweening;
using UnityEngine;

namespace Bounce.Framework
{
    public class AnimationSequenceStepTransformScale : AnimationSequenceStepTransform
    {
        public override string displayName { get { return $"{(_isSelf ? "Transform (This)" : _owner)}: DOScale"; } }

        protected override Tween GetTween(AnimationSequence animationSequence)
        {
            Transform owner = _isSelf ? animationSequence.transformCached : _owner;

            Tween tween;

            Vector3 start;
            Vector3 end;

            if (_isUseTarget)
            {
                float duration = _isSpeedBased ? Vector3.Distance(_target.localScale, owner.localScale) / _duration : _duration;
                start = owner.localScale;
                end = _target.localScale;

                tween = owner.DOScale(end, duration)
                             .ChangeStartValue(start);
            }
            else
            {
                float duration = _isSpeedBased ? Vector3.Distance(_value, owner.localScale) / _duration : _duration;
                start = owner.localScale;
                end = _relative ? owner.localScale + _value : _value;

                tween = owner.DOScale(end, duration)
                             .ChangeStartValue(start);
            }

            owner.localScale = end;

            return tween;
        }

        protected override Tween GetResetTween(AnimationSequence animationSequence)
        {
            Transform owner = _isSelf ? animationSequence.transformCached : _owner;

            return owner.DOScale(owner.localScale, 0.0f);
        }
    }
}