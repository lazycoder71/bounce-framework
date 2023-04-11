using Sirenix.OdinInspector;
using UnityEngine;

namespace Bounce.Framework
{
    [System.Serializable]
    public enum AudioType
    {
        Sound,
        Music,
    }

    [System.Serializable]
    public class AudioConfig : ScriptableObject
    {
        [BoxGroup("Config")]
        [HorizontalGroup("Config/Clip")]
        [LabelWidth(50f)]
        [SerializeField] AudioClip _clip;

        [BoxGroup("Config")]
        [HorizontalGroup("Config/Clip")]
        [LabelWidth(50f)]
        [SerializeField] AudioType _type;

        [BoxGroup("Config")]
        [HorizontalGroup("Config/Type", LabelWidth = 75f)]
        [SerializeField] bool _is3D;

        [BoxGroup("Config")]
        [HorizontalGroup("Config/Type")]
        [ShowIf("@_is3D == true")]
        [MinMaxSlider(0f, 200f, ShowFields = true)]
        [SerializeField] Vector2 _distance = new Vector2(1f, 10f);

        [BoxGroup("Config")]
        [Range(0f, 1f)]
        [SerializeField] float _volumeScale = 1f;

        public AudioClip clip { get { return _clip; } }
        public AudioType type { get { return _type; } }
        public bool is3D { get { return _is3D; } }
        public Vector2 distance { get { return _distance; } }
        public float volumeScale { get { return _volumeScale; } }
    }
}