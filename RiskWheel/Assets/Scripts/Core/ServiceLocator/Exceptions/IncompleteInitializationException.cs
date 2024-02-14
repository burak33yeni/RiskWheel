using System;

namespace Design.Patterns.ServiceLocator
{
    internal class IncompleteInitializationException : Exception
    {
        internal IncompleteInitializationException() : base("The method or operation is not implemented.")
        {
        }
    }
}