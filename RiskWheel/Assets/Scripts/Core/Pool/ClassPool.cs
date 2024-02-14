namespace Design.Patterns.Pool
{
    public abstract class ClassPool<TItem> : Pool<TItem, ClassPoolModel> where TItem : class
    {
        internal override void Build(ClassPoolModel model)
        {
            base.Build(model);
            Prespawn(minimumCount);
        }
    }
    
    public abstract class ClassPool<TModel, TItem> : Pool<TModel, TItem, ArgumentClassPoolModel<TModel>> where TItem : class
    {
        internal override void Build(ArgumentClassPoolModel<TModel> model)
        {
            base.Build(model);
            Prespawn(minimumCount, model.DefaultModel);
        }
    }
}