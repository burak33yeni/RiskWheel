using System;

namespace Design.Patterns.ServiceLocator
{
    internal class WrongRegistrationMethodException : Exception
    {
        internal WrongRegistrationMethodException() : base("Subcontexts cannot register the context with Register method, RegisterContext method should be used instead.")
        {
        }
    }
}