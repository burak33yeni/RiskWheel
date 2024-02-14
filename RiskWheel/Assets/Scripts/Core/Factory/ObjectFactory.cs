using UnityEngine;

namespace Design.Patterns.Factory
{
    public abstract class ObjectFactory<TItem> where TItem : Object
    {
        private TItem _prefabObject;

        protected ObjectFactory(TItem prefabObject)
        {
            _prefabObject = prefabObject;
        }
        
        protected TItem Create(Transform parent)
        {
            return Object.Instantiate(_prefabObject, parent, true);
        }
    }
}