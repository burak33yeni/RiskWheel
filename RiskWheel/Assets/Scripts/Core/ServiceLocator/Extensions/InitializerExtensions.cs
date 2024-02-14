using Design.Patterns.ServiceLocator;
using Object = UnityEngine.Object;

public static class InitializerExtensions
{
    public static ConstructorObjectServiceInitializer<TService> FromNew<TService>(this IncompleteServiceInitializer<TService> initializer)
    {
        ConstructorObjectServiceInitializer<TService> newInitializer = new()
        {
            isLazy = initializer.isLazy,
            initializersRef = initializer.initializersRef, 
            registerTargets = initializer.registerTargets
        };
        initializer.initializersRef[typeof(TService)] = newInitializer;
        return newInitializer;
    }

    public static InstancedObjectServiceInitializer<TService> FromInstance<TService>(this IncompleteServiceInitializer<TService> initializer, TService instance)
    {
        InstancedObjectServiceInitializer<TService> newInitializer = new()
        {
            isLazy = initializer.isLazy,
            initializersRef = initializer.initializersRef,
            registerTargets = initializer.registerTargets,
            instance = instance
        };
        initializer.initializersRef[typeof(TService)] = newInitializer;
        return newInitializer;
    }

    public static PrefabObjectServiceInitializer<TService> FromPrefab<TService>(this IncompleteServiceInitializer<TService> initializer, Object prefab) where TService : Object
    {
        PrefabObjectServiceInitializer<TService> newInitializer = new()
        {
            isLazy = initializer.isLazy,
            initializersRef = initializer.initializersRef,
            registerTargets = initializer.registerTargets,
            prefab = prefab
        };
        initializer.initializersRef[typeof(TService)] = newInitializer;
        return newInitializer;
    }

    public static PrefabObjectContextInitializer FromPrefab(this IncompleteContextInitializer initializer, Context prefab)
    {
        initializer.initializersRef.Remove(initializer);
        PrefabObjectContextInitializer newInitializer = new()
        {
            initializersRef = initializer.initializersRef,
            prefab = prefab
        };
        newInitializer.initializersRef.Add(newInitializer);
        return newInitializer;
    }
}