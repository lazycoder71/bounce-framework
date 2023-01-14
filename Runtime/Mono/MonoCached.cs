using UnityEngine;

namespace Bounce.Framework
{
    public class MonoCached : MonoBehaviour
    {
        private GameObject _gameObject;
        private Transform _transform;

        public Transform transformCached
        {
            get
            {
                if (_transform == null)
                    _transform = transform;

                return _transform;
            }
        }

        public GameObject gameObjectCached
        {
            get
            {
                if (_gameObject == null)
                    _gameObject = gameObject;

                return _gameObject;
            }
        }
    }
}