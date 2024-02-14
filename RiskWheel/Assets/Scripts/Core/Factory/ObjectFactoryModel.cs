namespace Design.Patterns.Factory
{
    public class ObjectFactoryModel<TObject> : FactoryModel
    {
        internal ObjectFactoryModel() { }
        
        public TObject PrefabObject;
    }
}