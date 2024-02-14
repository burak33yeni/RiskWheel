namespace Design.Patterns.Pool
{
    public class ArgumentClassPoolModel<TModel> : PoolModel
    {
        internal ArgumentClassPoolModel() { }

        public TModel DefaultModel;
    }
}