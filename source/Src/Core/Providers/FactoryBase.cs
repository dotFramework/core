using System;
using System.Collections.Generic;
using Unity;
using Unity.Extension;
using Unity.Injection;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Interception.Interceptors.TypeInterceptors.VirtualMethodInterception;
using Unity.Lifetime;
using Unity.Registration;

namespace DotFramework.Core
{
    public abstract class FactoryBase<TFactory, TBaseType> : SingletonProvider<TFactory>
        where TFactory : class
    {
        private static readonly object padlock = new object();
        private readonly IList<InterceptionBehavior> _InterceptionBehaviors = new List<InterceptionBehavior>();

        private IUnityContainer _Container;
        protected IUnityContainer Container
        {
            get
            {
                if (_Container == null)
                {
                    lock (padlock)
                    {
                        if (_Container == null)
                        {
                            _Container = new UnityContainer();
                        }
                    }
                }

                return _Container;
            }
        }

        protected void AddNewBehavior<TBehavior>() where TBehavior : IInterceptionBehavior
        {
            _InterceptionBehaviors.Add(new InterceptionBehavior<TBehavior>());
        }

        protected void AddNewExtension<TExtension>() where TExtension : UnityContainerExtension
        {
            Container.AddNewExtension<TExtension>();
        }

        public TType Resolve<TType>() where TType : TBaseType
        {
            try
            {
                lock (padlock)
                {
                    if (!Container.IsRegistered<TType>())
                    {
                        Register<TType>();
                    }

                    return Container.Resolve<TType>();
                }
            }
            catch (Exception ex)
            {
                if (HandleException(ref ex))
                {
                    throw ex;
                }

                return default(TType);
            }
        }

        protected void RegisterType<TType>() where TType : TBaseType
        {
            List<InjectionMember> injectionMembers = new List<InjectionMember>
            {
                GetInterceptor<TType>()
            };

            injectionMembers.AddRange(_InterceptionBehaviors);

            Container.RegisterType<TType>(new ContainerControlledLifetimeManager(),
                                          injectionMembers.ToArray());
        }

        protected void RegisterType<TType>(TType instance) where TType : TBaseType
        {
            List<InjectionMember> injectionMembers = new List<InjectionMember>
            {
                new InjectionFactory((c) => instance),
                GetInterceptor<TType>()
            };

            injectionMembers.AddRange(_InterceptionBehaviors);

            Container.RegisterType<TType>(new ContainerControlledLifetimeManager(),
                                          injectionMembers.ToArray());
        }

        protected void SimpleRegisterType<TType>() where TType : TBaseType
        {
            Container.RegisterType<TType>(new ContainerControlledLifetimeManager());
        }

        protected void SimpleRegisterType<TType>(TType instance) where TType : TBaseType
        {
            Container.RegisterType<TType>(new ContainerControlledLifetimeManager(),
                                          new InjectionFactory((c) => instance));
        }

        protected abstract Interceptor GetInterceptor<TType>();

        protected abstract void Register<TType>() where TType : TBaseType;

        protected abstract bool HandleException(ref Exception ex);
    }

    public abstract class InterfaceFactoryBase<TFactory, TBaseType> : FactoryBase<TFactory, TBaseType>
        where TFactory : class
    {
        protected override Interceptor GetInterceptor<TType>()
        {
            return new Interceptor<InterfaceInterceptor>();
        }

        protected override void Register<TType>()
        {
            TType type = CreateType<TType>();
            RegisterType(type);
        }

        protected abstract TType CreateType<TType>() where TType : TBaseType;
    }

    public abstract class InterfaceFactoryBase<TFactory, TBaseType, TInterceptionBehavior> : InterfaceFactoryBase<TFactory, TBaseType>
    where TFactory : class
    where TInterceptionBehavior : IInterceptionBehavior
    {
        public InterfaceFactoryBase()
        {
            AddNewExtension<Interception>();
            AddNewBehavior<TInterceptionBehavior>();
        }
    }

    public abstract class VirtualMethodFactoryBase<TFactory, TBaseType> : FactoryBase<TFactory, TBaseType>
        where TFactory : class
    {
        protected override Interceptor GetInterceptor<TType>()
        {
            return new Interceptor<VirtualMethodInterceptor>();
        }

        protected override void Register<TType>()
        {
            RegisterType<TType>();
        }
    }

    public abstract class VirtualMethodFactoryBase<TFactory, TBaseType, TInterceptionBehavior> : VirtualMethodFactoryBase<TFactory, TBaseType>
    where TFactory : class
    where TInterceptionBehavior : IInterceptionBehavior
    {
        public VirtualMethodFactoryBase()
        {
            AddNewExtension<Interception>();
            AddNewBehavior<TInterceptionBehavior>();
        }
    }
}
