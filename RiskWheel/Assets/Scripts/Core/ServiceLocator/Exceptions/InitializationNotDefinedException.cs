using System;

namespace Design.Patterns.ServiceLocator
{
    internal class InitializationNotDefinedException : Exception
    {
        internal InitializationNotDefinedException() : base("Initialization method is not defined.")
        {
        }
    }
}