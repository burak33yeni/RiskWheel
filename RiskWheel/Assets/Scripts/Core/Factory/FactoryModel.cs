using Design.Patterns.ServiceLocator;

namespace Design.Patterns.Factory
{
    public abstract class FactoryModel
    {
        internal FactoryModel() { }
        
        public Context OwnerContext;
    }
}