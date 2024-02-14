using System;

namespace Design.Patterns.ServiceLocator
{
    internal class EventNotFoundException : Exception
    {
        internal EventNotFoundException(Type type) : base("Event of " + type + "can not be found in any context.")
        {
        }
    }
}