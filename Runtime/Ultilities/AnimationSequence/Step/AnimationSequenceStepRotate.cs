using DG.Tweening;
using UnityEngine;

namespace Bounce.Framework
{
    public class AnimationSequenceStepRotate : AnimationSequenceStepTransform
    {
        [SerializeField]
        private RotateMode _rotateMode = RotateMode.Fast;

        public override string displayName { get { return $"{(_isSelf ? "Transform (This)" : _owner)}: Rotate To {(_isUseTarget ? (_target == null ? "Null" : _target.name) : _value)}"; } }

        protected override Tween GetTween(AnimationSequence animationSequence)
        {
            Transform owner = _isSelf ? animationSequence.transformCached : _owner;

            Tween tween;

            if (_isUseTarget)
            {
                float duration = _isSpeedBased ? Vector3.Angle(_target.localEulerAngles, owner.localEulerAngles) / _duration : _duration;
                Vector3 start = owner.localEulerAngles;
                Vector3 end = _target.localEulerAngles;

                if (_isFrom)
                {
                    tween = owner.DOLocalRotate(start, duration, _rotateMode)
                                 .ChangeStartValue(end);
                }
                else
                {
                    tween = owner.DORotate(end, duration, _rotateMode)
                                 .ChangeStartValue(start);
                }
            }
            else
            {
                float duration = _isSpeedBased ? Vector3.Angle(_value, owner.localEulerAngles) / _duration : _duration;
                Vector3 start = owner.localEulerAngles;
                Vector3 end = _relative ? owner.localEulerAngles + _value : _value;

                if (_isFrom)
                {
                    tween = owner.DORotate(start, duration, _rotateMode)
                                 .ChangeStartValue(end);
                }
                else
                {
                    tween = owner.DORotate(end, duration, _rotateMode)
                                 .ChangeStartValue(start);
                }
            }

            return tween;
        }

        protected override Tween GetResetTween(AnimationSequence animationSequence)
        {
            Transform owner = _isSelf ? animationSequence.transformCached : _owner;

            return owner.DORotate(owner.localEulerAngles, 0.0f);
        }
    }
}