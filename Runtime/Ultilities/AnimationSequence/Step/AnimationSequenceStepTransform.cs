using Sirenix.OdinInspector;
using UnityEngine;

namespace Bounce.Framework
{
    public class AnimationSequenceStepTransform : AnimationSequenceStepAction<Transform>
    {
        [SerializeField]
        [HorizontalGroup("Target"), ShowIf("@_isUseTarget == false")]
        [InlineButton("@_isUseTarget = true", Label = "Value")]
        protected Vector3 _value;
    }
}