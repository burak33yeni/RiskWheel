using System.Collections.Generic;
using Design.Patterns.ServiceLocator;

namespace Design.Patterns.Pool
{
    public abstract class Collector<TItem, TPoolModel> where TItem : class where TPoolModel : PoolModel
    {
        internal Collector() { }

        private int _autoShrinkPeriod;
        internal int minimumCount { get; private set; }
        internal HashSet<TItem> allObjects { get; private set; }
        internal Queue<TItem> inactiveItems { get; private set; }
        
        internal Context ownerContext { get; private set; }

        internal virtual void Build(TPoolModel model)
        {
            _autoShrinkPeriod = model.AutoShrinkPeriod;
            minimumCount = model.MinimumCount;
            ownerContext = model.OwnerContext;
            allObjects = new HashSet<TItem>();
            inactiveItems = new Queue<TItem>();

            if (_autoShrinkPeriod != 0)
            {
            }
        }

        public void Despawn(TItem obj)
        {
            if (obj == null) return;

            if (!allObjects.Contains(obj))
            {
                Dispose(obj);
            }
            else
            {
                inactiveItems.Enqueue(obj);
                Deactivate(obj);
                if (obj is IDespawnable spawnable)
                    spawnable.OnDespawn();
            }
        }

        public void Clear(bool ignoreMinimumCount = true)
        {
            long cleanCount = ignoreMinimumCount ? inactiveItems.Count : inactiveItems.Count - minimumCount;

            for (int i = 0; i < cleanCount; i++)
            {
                Dispose(inactiveItems.Dequeue());
            }
        }

        protected void ConfigureSpawn(TItem obj)
        {
            allObjects.Add(obj);
            ownerContext.FulfillDependencies(obj);
            if (obj is ISpawnable spawnable)
                spawnable.OnSpawn();
        }

        protected void ConfigureRespawn(TItem obj)
        {
            if (obj is ISpawnable respawnable)
                respawnable.OnSpawn();
        }
        
        protected virtual void Deactivate(TItem obj) { }
        protected virtual void Dispose(TItem obj) { }
    }
}