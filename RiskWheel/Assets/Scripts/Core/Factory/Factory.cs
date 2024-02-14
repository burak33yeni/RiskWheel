using Design.Patterns.ServiceLocator;

namespace Design.Patterns.Factory
{
    public abstract class Factory<TItem, TFactoryModel> where TFactoryModel : FactoryModel
    {
        internal Factory() { }
        
        private Context _ownerContext;

        internal virtual void Build(TFactoryModel model)
        {
            _ownerContext = model.OwnerContext;
        }
        
        public TItem Spawn()
        {
            TItem obj = Create();
            ConfigureSpawn(obj);
            return obj;
        }
        
        protected abstract TItem Create();
        
        private void ConfigureSpawn<T>(T obj)
        {
            _ownerContext.FulfillDependencies(obj);
        }
    }
    
    public abstract class Factory<TModel, TItem, TFactoryModel> where TFactoryModel : FactoryModel
    {
        internal Factory() { }
        
        private Context _ownerContext;
        
        internal virtual void Build(TFactoryModel model)
        {
            _ownerContext = model.OwnerContext;
        }
        
        public TItem Spawn(TModel model)
        {
            TItem obj = Create(model);
            _ownerContext.FulfillDependencies(obj);
            return obj;
        }
        
        protected abstract TItem Create(TModel model);
    }
}