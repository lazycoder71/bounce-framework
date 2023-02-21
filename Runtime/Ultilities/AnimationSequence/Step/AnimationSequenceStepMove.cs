using DG.Tweening;
using UnityEngine;

namespace Bounce.Framework
{
    public class AnimationSequenceStepMove : AnimationSequenceStepTransform
    {
        [SerializeField]
        private bool _snapping = false;

        public override string displayName { get { return $"{(_isSelf ? "Transform (This)" : _owner)}: Move To {(_isUseTarget ? (_target == null ? "Null" : _target.name) : _value)}"; } }

        protected override Tween GetTween(AnimationSequence animationSequence)
        {
            Transform owner = _isSelf ? animationSequence.transformCached : _owner;

            Tween tween;

            if (_isUseTarget)
            {
                float duration = _isSpeedBased ? Vector3.Distance(_target.localPosition, owner.localPosition) / _duration : _duration;
                Vector3 start = owner.localPosition;
                Vector3 end = _target.localPosition;

                if (_isFrom)
                {
                    tween = owner.DOLocalMove(start, duration, _snapping)
                                 .ChangeStartValue(end);
                }
                else
                {
                    tween = owner.DOLocalMove(end, duration, _snapping)
                                 .ChangeStartValue(start);
                }
            }
            else
            {
                float duration = _isSpeedBased ? Vector3.Distance(_value, owner.localPosition) / _duration : _duration;
                Vector3 start = owner.localPosition;
                Vector3 end = _relative ? owner.localPosition + _value : _value;

                if (_isFrom)
                {
                    tween = owner.DOLocalMove(start, duration, _snapping)
                                 .ChangeStartValue(end);
                }
                else
                {
                    tween = owner.DOLocalMove(end, duration, _snapping)
                                 .ChangeStartValue(start);
                }
            }

            return tween;
        }

        protected override Tween GetResetTween(AnimationSequence animationSequence)
        {
            Transform owner = _isSelf ? animationSequence.transformCached : _owner;

            return owner.DOLocalMove(owner.localPosition, 0.0f);
        }
    }
}