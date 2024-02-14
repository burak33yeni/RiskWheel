using UnityEngine;

namespace Design.Patterns.ServiceLocator
{
    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        public abstract void Install(Context context);
    }
}