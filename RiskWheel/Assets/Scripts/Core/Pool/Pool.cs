namespace Design.Patterns.Pool
{
    public abstract class Pool<TItem, TPoolModel> : Collector<TItem, TPoolModel> where TItem : class where TPoolModel : PoolModel
    {
        internal Pool() { }
        
        public TItem Spawn()
        {
            if (inactiveItems.Count > 0)
            {
                TItem obj = inactiveItems.Dequeue();
                Recreate(obj);
                ConfigureRespawn(obj);
                return obj;
            }
            else
            {
                TItem obj = Create();
                ConfigureSpawn(obj);
                return obj;
            }
        }

        private protected void Prespawn(int count)
        {
            for (int i = 0; i < count; i++)
            {
                TItem obj = Create();
                Deactivate(obj);
                inactiveItems.Enqueue(obj);
                allObjects.Add(obj);
                ownerContext.FulfillDependencies(obj);
            }
        }
        
        protected abstract TItem Create();
        protected abstract void Recreate(TItem obj);
    }
    
    public abstract class Pool<TModel, TItem, TPoolModel> : Collector<TItem, TPoolModel> where TItem : class where TPoolModel : PoolModel
    {
        internal Pool() { }
        
        public TItem Spawn(TModel arg)
        {
            if (inactiveItems.Count > 0)
            {
                TItem obj = inactiveItems.Dequeue();
                Recreate(obj, arg);
                ConfigureRespawn(obj);
                return obj;
            }
            else
            {
                TItem obj = Create(arg);
                ConfigureSpawn(obj);
                return obj;
            }
        }

        private protected void Prespawn(int count, TModel model)
        {
            for (int i = 0; i < count; i++)
            {
                TItem obj = Create(model);
                Deactivate(obj);
                inactiveItems.Enqueue(obj);
                allObjects.Add(obj);
                ownerContext.FulfillDependencies(obj);
            }
        }
        
        protected abstract TItem Create(TModel model);
        protected abstract void Recreate(TItem obj, TModel model);
    }
}