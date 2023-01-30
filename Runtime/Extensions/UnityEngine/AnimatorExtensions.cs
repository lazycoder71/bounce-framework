using UnityEngine;

namespace Bounce.Framework
{
    public static class AnimatorExtensions
    {
        public static float GetLength(this Animator animator, string clipName)
        {
            var controller = animator.runtimeAnimatorController;
            var clips = controller.animationClips;
            int count = clips.Length;

            for (int i = 0; i < count; i++)
            {
                var clip = clips[i];
                if (clip.name == clipName)
                {
                    return clip.length;
                }
            }

            BDebug.Log("Get clip lenght failed: Clip {0} doesn't exist!", clipName);
            return 0f;
        }
    }
}
