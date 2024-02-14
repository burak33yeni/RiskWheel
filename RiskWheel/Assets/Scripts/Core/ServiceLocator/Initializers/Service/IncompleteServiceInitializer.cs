using System;

namespace Design.Patterns.ServiceLocator
{
    public class IncompleteServiceInitializer<TService> : ServiceInitializer<IncompleteServiceInitializer<TService>, TService>
    {
        internal IncompleteServiceInitializer() { }
        
        internal override object CreateInstance()
        {
            throw new InitializationNotDefinedException();
        }
    }
}