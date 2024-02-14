using System;

namespace Design.Patterns.ServiceLocator
{
    internal class ServiceNotAssignableException : Exception
    {
        internal ServiceNotAssignableException(Type registerType, Type serviceType) : base("Register type " + registerType +
                                                                                           " must be assignable from service type " + serviceType + ".")
        {
        }
    }
}