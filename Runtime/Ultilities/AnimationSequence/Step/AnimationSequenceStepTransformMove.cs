using DG.Tweening;
using UnityEngine;

namespace Bounce.Framework
{
    public class AnimationSequenceStepTransformMove : AnimationSequenceStepTransform
    {
        [SerializeField]
        private bool _snapping = false;

        public override string displayName { get { return $"{(_isSelf ? "Transform (This)" : _owner)}: Move To {(_isUseTarget ? (_target == null ? "Null" : _target.name) : _value)}"; } }

        protected override Tween GetTween(AnimationSequence animationSequence)
        {
            Transform owner = _isSelf ? animationSequence.transformCached : _owner;

            Tween tween;

            Vector3 start;
            Vector3 end;

            if (_isUseTarget)
            {
                float duration = _isSpeedBased ? Vector3.Distance(_target.localPosition, owner.localPosition) / _duration : _duration;
                start = owner.localPosition;
                end = _target.localPosition;

                tween = owner.DOLocalMove(end, duration, _snapping)
                             .ChangeStartValue(start);
            }
            else
            {
                float duration = _isSpeedBased ? Vector3.Distance(_value, owner.localPosition) / _duration : _duration;
                start = owner.localPosition;
                end = _relative ? owner.localPosition + _value : _value;

                tween = owner.DOLocalMove(end, duration, _snapping)
                             .ChangeStartValue(start);
            }

            animationSequence.transformCached.localPosition = end;

            return tween;
        }

        protected override Tween GetResetTween(AnimationSequence animationSequence)
        {
            Transform owner = _isSelf ? animationSequence.transformCached : _owner;

            return owner.DOLocalMove(owner.localPosition, 0.0f);
        }
    }
}