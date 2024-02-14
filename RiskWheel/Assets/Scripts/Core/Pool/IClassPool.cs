namespace Design.Patterns.Factory
{
    public interface IClassPool<TItem>
    {
        TItem Spawn();
    }
    
    public interface IClassPool<TModel, TItem>
    {
        TItem Spawn(TModel model);
    }
}