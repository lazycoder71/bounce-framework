using DG.Tweening;
using UnityEngine;

namespace Bounce.Framework
{
    public class AnimationSequenceStepScale : AnimationSequenceStepTransform
    {
        public override string displayName { get { return $"{(_isSelf ? "Transform (This)" : _owner)}: Scale To {(_isUseTarget ? (_target == null ? "Null" : _target.name) : _value)}"; } }

        protected override Tween GetTween(AnimationSequence animationSequence)
        {
            Transform owner = _isSelf ? animationSequence.transformCached : _owner;

            Tween tween;

            if (_isUseTarget)
            {
                float duration = _isSpeedBased ? Vector3.Distance(_target.localScale, owner.localScale) / _duration : _duration;
                Vector3 start = owner.localScale;
                Vector3 end = _target.localScale;

                if (_isFrom)
                {
                    tween = owner.DOScale(start, duration)
                                 .ChangeStartValue(end);
                }
                else
                {
                    tween = owner.DOScale(end, duration)
                                 .ChangeStartValue(start);
                }
            }
            else
            {
                float duration = _isSpeedBased ? Vector3.Distance(_value, owner.localScale) / _duration : _duration;
                Vector3 start = owner.localScale;
                Vector3 end = _relative ? owner.localScale + _value : _value;

                if (_isFrom)
                {
                    tween = owner.DOScale(start, duration)
                                 .ChangeStartValue(end);
                }
                else
                {
                    tween = owner.DOScale(end, duration)
                                 .ChangeStartValue(start);
                }
            }

            return tween;
        }

        protected override Tween GetResetTween(AnimationSequence animationSequence)
        {
            Transform owner = _isSelf ? animationSequence.transformCached : _owner;

            return owner.DOScale(owner.localScale, 0.0f);
        }
    }
}