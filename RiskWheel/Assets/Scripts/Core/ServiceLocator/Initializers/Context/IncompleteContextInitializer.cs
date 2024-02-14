namespace Design.Patterns.ServiceLocator
{
    public class IncompleteContextInitializer : ContextInitializer
    {
        internal IncompleteContextInitializer() { }
        
        internal override Context CreateInstance()
        {
            throw new IncompleteInitializationException();
        }
    }
}