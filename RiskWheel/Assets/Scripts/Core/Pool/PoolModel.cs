using Design.Patterns.ServiceLocator;

namespace Design.Patterns.Pool
{
    public abstract class PoolModel
    {
        internal PoolModel() { }
        
        public int MinimumCount;
        public int AutoShrinkPeriod;
        public Context OwnerContext;
    }
}