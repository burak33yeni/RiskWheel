using System;

namespace Design.Patterns.ServiceLocator
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ResolveAttribute : Attribute
    {
    }
}