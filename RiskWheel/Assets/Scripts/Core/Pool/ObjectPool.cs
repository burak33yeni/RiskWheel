using UnityEngine;

namespace Design.Patterns.Pool
{
    public abstract class ObjectPool : BaseObjectPool<GameObject>
    {
        private protected sealed override GameObject GetGameObject(GameObject obj)
        {
            return obj;
        }
    }

    public abstract class ObjectPool<TComponent> : BaseObjectPool<TComponent> where TComponent : Component
    {
        private protected sealed override GameObject GetGameObject(TComponent obj)
        {
            return obj.gameObject;
        }
    }
}