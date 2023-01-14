using System;
using UnityEngine;

namespace Bounce.Framework
{
    [Serializable]
    public class BValue<T>
    {
        [SerializeField] T _value;

        public T value
        {
            get { return _value; }
            set
            {
                if (!_value.Equals(value))
                {
                    _value = value;
                    eventValueChanged?.Invoke(_value);
                }
            }
        }

        public event Action<T> eventValueChanged;

        public BValue(T defaultValue)
        {
            _value = defaultValue;
        }
    }
}