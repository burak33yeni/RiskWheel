using System;

namespace Design.Patterns.ServiceLocator
{
    internal class AlreadyInitializedException : Exception
    {
        internal AlreadyInitializedException() : base("Cannot register service after context initialization.")
        {
        }
    }
}