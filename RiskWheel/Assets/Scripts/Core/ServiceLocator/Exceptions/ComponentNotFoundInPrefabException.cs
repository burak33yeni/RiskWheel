using System;

namespace Design.Patterns.ServiceLocator
{
    internal class ComponentNotFoundInPrefabException : Exception
    {
        internal ComponentNotFoundInPrefabException(string prefabName, Type type) : base("Prefab " + prefabName + " does not contain component " + type + ".")
        {
        }
    }
}