using UnityEngine;

namespace Design.Patterns.Pool
{
    public class ObjectPoolModel<TObject> : PoolModel
    {
        internal ObjectPoolModel() { }
        
        public TObject PrefabObject;
        public Transform ParentTransform;
    }
}