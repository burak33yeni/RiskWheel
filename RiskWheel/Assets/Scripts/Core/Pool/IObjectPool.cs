using UnityEngine;

namespace Design.Patterns.Factory
{
    public interface IObjectPool<TItem>
    {
        TItem Spawn(Transform model);
        void Despawn(TItem item);
    }
}