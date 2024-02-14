using UnityEngine;

namespace Design.Patterns.Factory
{
    public interface IObjectFactory<TItem>
    {
        TItem Spawn(Transform model);
    }
}