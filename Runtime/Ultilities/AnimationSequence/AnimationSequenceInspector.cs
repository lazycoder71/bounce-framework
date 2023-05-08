#if UNITY_EDITOR

using Bounce.Framework.Editor;
using UnityEditor;

namespace Bounce.Framework
{
    [CustomEditor(typeof(AnimationSequence))]
    public class AnimationSequenceInspector : Sirenix.OdinInspector.Editor.OdinEditor
    {
        protected override void OnDisable()
        {
            base.OnDisable();

            AnimationSequenceEditor.Stop();
        }
    }
}

#endif