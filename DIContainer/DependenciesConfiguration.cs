using System;
using System.Collections.Generic;

namespace DIContainer
{
    public enum LifeTime
    {
        Singleton,
        InstancePerDependency
    }

    public class DependenciesConfiguration
    {
        public readonly Dictionary<Type, List<ImplementationInfo>> registedDependencies;

        public DependenciesConfiguration()
        {
            registedDependencies = new Dictionary<Type, List<ImplementationInfo>>();
        }

        public void Register<TDependency, TImplementation>(LifeTime lt = LifeTime.InstancePerDependency)
        {
            Register(typeof(TDependency), typeof(TImplementation), lt);
        }

        public void Register(Type interfaceType, Type classType, LifeTime lt = LifeTime.InstancePerDependency)
        {
            if ((!interfaceType.IsInterface && !Equals(interfaceType, classType)) || classType.IsAbstract || !interfaceType.IsAssignableFrom(classType) && !interfaceType.IsGenericTypeDefinition)
                throw new Exception("Registration bruh");
            if (!registedDependencies.ContainsKey(interfaceType))
            {
                List<ImplementationInfo> impl = new List<ImplementationInfo>();
                impl.Add(new ImplementationInfo(lt, classType));
                registedDependencies.Add(interfaceType, impl);
            }
            else
            {
                registedDependencies[interfaceType].Add(new ImplementationInfo(lt, classType));
            }
        }
    }
}