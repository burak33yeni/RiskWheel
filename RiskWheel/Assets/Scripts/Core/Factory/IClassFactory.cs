namespace Design.Patterns.Factory
{
    public interface IClassFactory<TModel, TItem>
    {
        TItem Spawn(TModel model);
    }

    public interface IClassFactory<TItem>
    {
        TItem Spawn();
    }
}