using System;
using System.Collections.Generic;
using System.Reflection;
using Design.Patterns.ServiceLocator;

internal static class TypeInfoExtensions
{
    private static readonly string _CoreAssembly = typeof(TypeInfoExtensions).Assembly.FullName;
    
    private static Dictionary<Assembly, bool> _ReferenceCache = new()
    {
        {typeof(TypeInfoExtensions).Assembly, true}
    };
    
    private const BindingFlags BINDING_FLAGS =
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

    internal static List<FieldInfo> GetResolvingFields(this Type type)
    {
        if (!type.Assembly.IsReferencingAssembly(type)) return null;
        
        List<FieldInfo> resolvingFields = new();
        AddResolvingFields(type, resolvingFields);
        
        type = type.BaseType;
        while(type != null)
        {
            if (!type.Assembly.IsReferencingAssembly(type)) break;
            AddResolvingFields(type, resolvingFields);
            type = type.BaseType;
        }
        
        return resolvingFields;
    }

    private static void AddResolvingFields(Type type, List<FieldInfo> resolvingFields)
    {
        FieldInfo[] fields = type.GetFields(BINDING_FLAGS);
        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfo fieldInfo = fields[i];
            if (!fieldInfo.IsDefined(typeof(ResolveAttribute))) continue;
            resolvingFields.Add(fieldInfo);
        }
    }

    private static bool IsReferencingAssembly(this Assembly assembly, Type type)
    {
        if (_ReferenceCache.TryGetValue(assembly, out bool referencing)) return referencing;
        
        AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
        for (int i = 0; i < referencedAssemblies.Length; i++)
        {
            if (referencedAssemblies[i].FullName != _CoreAssembly) continue;
            _ReferenceCache.Add(assembly, true);
            return true;
        }

        _ReferenceCache.Add(assembly, false);
        return false;
    }
}