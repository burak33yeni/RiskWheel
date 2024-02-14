using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Design.Patterns.ServiceLocator
{
    [DisallowMultipleComponent]
    public sealed class Context : MonoBehaviour
    {
        private static HashSet<Context> _ActiveContexts = new();
        private static Dictionary<object, Context> _tempDependentObjects = new();
    
        private ServiceLocator _serviceLocator;
        private EventBus _eventBus;
        
        [SerializeField]
        private MonoInstaller[] _monoInstallers;
        
        private Context _parentContext;
        private List<Context> _childContexts = new();
        private HashSet<ContextInitializer> _contextInitializers = new();

        public void Initialize(Context parent = null)
        {
            _serviceLocator = new ServiceLocator(this);
            _eventBus = new EventBus();
            _ActiveContexts.Add(this);
        
            _parentContext = parent;
            
            EventBus.PauseSending();
            
            RegisterItems();
            InitializeItems();

            EventBus.UnpauseSending();
        }

        private void RegisterItems()
        {
            for (int i = 0; i < _monoInstallers.Length; i++)
            {
                _monoInstallers[i].Install(this);
            }
        }

        private void InitializeItems()
        {
            _serviceLocator.Initialize();

            foreach (ContextInitializer initializer in _contextInitializers)
            {
                Context context = initializer.Initialize();
                _childContexts.Add(context);
                context.Initialize(this);
            }
        }

        public IncompleteServiceInitializer<TService> Register<TService>()
        {
            return _serviceLocator.Register<TService>();
        }
        public IncompleteContextInitializer RegisterContext()
        {
            IncompleteContextInitializer initializer = new()
            {
                initializersRef = _contextInitializers
            };
        
            _contextInitializers.Add(initializer);
            return initializer;
        }

        internal TService Resolve<TService>()
        {
            if (_serviceLocator.Resolve(out TService service))
                return service;

            if (_parentContext == null)
                throw new ServiceNotFoundException(typeof(TService));

            return _parentContext.Resolve<TService>();
        }

        private object Resolve(Type type)
        {
            if (_serviceLocator.Resolve(type, out object service))
                return service;

            if (_parentContext == null)
                throw new ServiceNotFoundException(type);
        
            return _parentContext.Resolve(type);
        }

        public void RegisterEvent<TEvent>() where TEvent : Event
        {
            _eventBus.Register<TEvent>();
        }
    
        internal void SendEvent<TEvent>(TEvent payload) where TEvent : Event
        {
            if (_eventBus.HasType<TEvent>())
            {
                _eventBus.TrySend(payload);
                return;
            }

            if (_parentContext == null) 
                throw new EventNotFoundException(typeof(TEvent));
        
            _parentContext.SendEvent(payload);
        }

        internal void FollowEvent<TEvent>(Action<TEvent> action, int invocationOrder = 0) where TEvent : Event
        {
            if (_eventBus.HasType<TEvent>())
            {
                _eventBus.Follow(action, invocationOrder);
                return;
            }
        
            if (_parentContext == null) 
                throw new EventNotFoundException(typeof(TEvent));
        
            _parentContext.FollowEvent(action, invocationOrder);
        }

        internal void FollowEvent<TEvent>(Action action, int invocationOrder = 0) where TEvent : Event
        {
            if (_eventBus.HasType<TEvent>())
            {
                _eventBus.Follow<TEvent>(action, invocationOrder);
                return;
            }
        
            if (_parentContext == null) 
                throw new EventNotFoundException(typeof(TEvent));
        
            _parentContext.FollowEvent<TEvent>(action, invocationOrder);
        }

        private void OnDestroy()
        {
            _serviceLocator?.ResetServices();
            _ActiveContexts.Remove(this);
        }
    
        internal void FulfillDependencies(object obj)
        {
            List<FieldInfo> fieldInfos = obj.GetType().GetResolvingFields();
            
            if(fieldInfos == null) return;
            
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                fieldInfo.SetValue(obj, Resolve(fieldInfo.FieldType));
            }

            if (obj is not IInitializable initializable) return;
            
            _tempDependentObjects.Add(obj, this);
            initializable.Initialize();
            _tempDependentObjects.Remove(obj);
        }

        public void CheckPersistence()
        {
            for (int i = 0; i < _childContexts.Count; i++)
            {
                Transform contextTransform = _childContexts[i].transform;
                if (contextTransform.parent == null) DontDestroyOnLoad(contextTransform);
                else if (!contextTransform.IsChildOf(transform))
                    throw new SubcontextMisplacementException(contextTransform.name);
            }
        }

        public static Context GetContextForObject(object obj)
        {
            foreach (Context activeContext in _ActiveContexts)
                if (activeContext._serviceLocator.ContainsServiceInstance(obj))
                    return activeContext;

            if (_tempDependentObjects.TryGetValue(obj, out Context tempOwnerContext)) 
                return tempOwnerContext;
            
            throw new ServiceInstanceNotFoundException(obj.GetType());
        }
    }
}