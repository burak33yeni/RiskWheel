using UnityEngine;

namespace Design.Patterns.ServiceLocator
{
    public class PrefabObjectContextInitializer : ContextInitializer
    {
        internal PrefabObjectContextInitializer() { }
        
        internal Context prefab;
    
        private Transform _parent;
        private Vector3? _position;
        private Quaternion? _rotation;

        internal override Context CreateInstance()
        {
            bool hasPosition = _position != null && _rotation != null;
            bool hasParent = _parent != null;
        
            Context instance;
            if (hasParent && hasPosition)
                instance = Object.Instantiate(prefab, _position.Value, _rotation.Value, _parent);
            else if (hasPosition) 
                instance = Object.Instantiate(prefab, _position.Value, _rotation.Value);
            else if (hasParent) 
                instance = Object.Instantiate(prefab, _parent);
            else
                instance = Object.Instantiate(prefab);
        
            return instance;
        }

        public PrefabObjectContextInitializer WithParent(Transform parent)
        {
            _parent = parent;
            return this;
        }
    }
}