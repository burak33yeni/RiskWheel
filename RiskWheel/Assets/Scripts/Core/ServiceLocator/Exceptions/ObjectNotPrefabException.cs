using System;

namespace Design.Patterns.ServiceLocator
{
    internal class ObjectNotPrefabException : Exception
    {
        internal ObjectNotPrefabException() : base("SceneContextCreator can only be used with prefabs")
        {
        }
    }
}