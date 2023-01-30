using DG.Tweening;
using UnityEngine;

namespace Bounce.Framework
{
    public class AnimationSequenceStepInterval : AnimationSequenceStep
    {
        [SerializeField] float _duration;

        public override string displayName { get { return "Interval"; } }

        public override void AddTweenToSequence(AnimationSequence animationSequence)
        {
            animationSequence.sequence.AppendInterval(_duration);
        }
    }
}