using DG.Tweening;
using System;

namespace Bounce.Framework
{
    [Serializable]
    public abstract class AnimationSequenceStep
    {
        [Serializable]
        public enum AddType
        {
            Append = 0,
            Join = 1,
            Insert = 2,
        }

        public abstract string displayName { get; }

        public abstract void AddTweenToSequence(AnimationSequence animationSequence);
    }
}