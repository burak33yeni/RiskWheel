using System;
using System.Collections.Generic;
using System.Linq;

namespace Design.Patterns.ServiceLocator
{
    internal abstract class Bus<TBase>
    {
        private Dictionary<Type, Dictionary<long, object>> _typeMapDict;
        
        internal Bus()
        {
            _typeMapDict = new Dictionary<Type, Dictionary<long, object>>();
        }

        internal void Register<T>() where T : TBase
        {
            if (HasType<T>())
                throw new Exception("Event already registered");
            _typeMapDict[typeof(T)] = new Dictionary<long, object>();
        }

        internal void Follow<T>(Action<T> action, int invocationOrder = 0) where T : TBase
        {
            Follow(typeof(T), action, invocationOrder);
        }

        protected void Follow(Type type, object action, int invocationOrder = 0)
        {
            Dictionary<long, object> actionsMap = GetTypeMap(type);

            long longInvocationOrder = invocationOrder;

            while (actionsMap.ContainsKey(longInvocationOrder))
                longInvocationOrder++;

            actionsMap.Add(longInvocationOrder, action);
        }

        protected void Unfollow(Type type, object action)
        {
            Dictionary<long, object> typeMap = GetTypeMap(type);

            KeyValuePair<long, object> keyValuePair = new();
            if (action is Action<TBase> typedActionParameter)
                keyValuePair = typeMap.FirstOrDefault(x => x.Value is Action<TBase> typedAction
                                                           && typedAction.Method == typedActionParameter.Method);
            else if (action is Action typelessActionParameter)
                keyValuePair = typeMap.FirstOrDefault(x => x.Value is Action typelessAction
                                                           && typelessAction.Method == typelessActionParameter.Method);

            if (keyValuePair.Value != null)
                typeMap.Remove(keyValuePair.Key);
        }

        internal void RemoveAllListeners<T>() where T : TBase
        {
            _typeMapDict[typeof(T)].Clear();
        }

        protected internal bool HasType<T>() where T : TBase
        {
            return _typeMapDict.ContainsKey(typeof(T));
        }

        protected Dictionary<long, object> GetTypeMap(Type type)
        {
            return _typeMapDict[type];
        }
    }
}