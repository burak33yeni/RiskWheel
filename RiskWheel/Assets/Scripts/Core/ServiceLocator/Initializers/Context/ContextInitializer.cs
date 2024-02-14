using System.Collections.Generic;

namespace Design.Patterns.ServiceLocator
{
    public abstract class ContextInitializer
    {
        internal ContextInitializer() { }
        
        internal HashSet<ContextInitializer> initializersRef;
    
        private Context _instance;
    
        internal Context Initialize()
        {
            _instance = CreateInstance();
            return _instance;
        }

        internal abstract Context CreateInstance();
    }
}