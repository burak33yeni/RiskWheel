using System;

namespace Design.Patterns.ServiceLocator
{
    internal class AlreadyRegisteredException : Exception
    {
        internal AlreadyRegisteredException(Type type) : base("Service of " + type + " is already registered.")
        {
        }
    }
}