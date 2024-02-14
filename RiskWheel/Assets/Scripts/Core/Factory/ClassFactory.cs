namespace Design.Patterns.Factory
{
    public abstract class ClassFactory<TItem> : Factory<TItem, ClassFactoryModel> where TItem : class
    {
    }
    
    public abstract class ClassFactory<TModel, TItem> : Factory<TModel, TItem, ClassFactoryModel> where TItem : class
    {
    }
}