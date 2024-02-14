using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Design.Patterns.ServiceLocator
{
    public class PrefabObjectServiceInitializer<TService> : ServiceInitializer<PrefabObjectServiceInitializer<TService>, TService> where TService : Object
    {
        internal PrefabObjectServiceInitializer() { }
        
        internal Object prefab;
    
        private Transform _parent;
        private Vector3? _position;
        private Quaternion? _rotation;

        internal override object CreateInstance()
        {
            bool hasPosition = _position != null && _rotation != null;
            bool hasParent = _parent != null;
            Object objInstance;
        
            if (hasParent && hasPosition)
                objInstance = Object.Instantiate(prefab, _position.Value, _rotation.Value, _parent);
            else if (hasPosition) 
                objInstance = Object.Instantiate(prefab, _position.Value, _rotation.Value);
            else if (hasParent) 
                objInstance = Object.Instantiate(prefab, _parent);
            else
                objInstance = Object.Instantiate(prefab);
        
            TService instance = null;
            if (objInstance is TService t)
                instance = t;
            else if (objInstance is GameObject go)
                instance = go.GetComponent<TService>();
            else if (objInstance is Component component)
                instance = component.GetComponent<TService>();
        
            if(instance is null)
                throw new ComponentNotFoundInPrefabException(prefab.name, typeof(TService));
        
            return instance;
        }

        public PrefabObjectServiceInitializer<TService> WithParent(Transform parent)
        {
            _parent = parent;
            return this;
        }
    }
}