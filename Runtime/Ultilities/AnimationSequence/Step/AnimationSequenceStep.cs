using System;

namespace Bounce.Framework
{
    [Serializable]
    public abstract class AnimationSequenceStep
    {
        [Serializable, Flags]
        public enum Callback
        {
            OnStart = 1 << 1,
            OnPlay = 1 << 2,
            OnUpdate = 1 << 3,
            OnStep = 1 << 4,
            OnComplete = 1 << 5,
            OnRewind = 1 << 6,
        }

        [Serializable]
        public enum AddType
        {
            Append = 0,
            Join = 1,
            Insert = 2,
        }

        public abstract string displayName { get; }

        public abstract void AddToSequence(AnimationSequence animationSequence);
    }
}