using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIContainer;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class DIContainerTests
    {
        [TestMethod]
        public void SimpleDependencyTest()
        {
            DependenciesConfiguration config = new DependenciesConfiguration();
            config.Register<ISmth, ClassForISmth>(LifeTime.Singleton);
            DependenciesProvider provider = new DependenciesProvider(config);

            ClassForISmth cl = (ClassForISmth)provider.Resolve<ISmth>();
            Assert.IsNotNull(cl);
        }

        [TestMethod]
        public void LifeTimeTest()
        {
            DependenciesConfiguration config = new DependenciesConfiguration();
            config.Register<ISmth, ClassForISmth>(LifeTime.Singleton);
            config.Register<IService, FirstForIService>();
            DependenciesProvider provider = new DependenciesProvider(config);

            ClassForISmth cl = (ClassForISmth)provider.Resolve<ISmth>();
            ClassForISmth cl2 = (ClassForISmth)provider.Resolve<ISmth>();
            Assert.AreEqual(cl, cl2);
            IService s1 = provider.Resolve<IService>();
            IService s2 = provider.Resolve<IService>();
            Assert.AreNotEqual(s1, s2);
        }

        [TestMethod]
        public void ManyImplementationsResolveTest()
        {
            DependenciesConfiguration config = new DependenciesConfiguration();
            config.Register<IService, FirstForIService>();
            config.Register<IService, SecondClForIService>();
            DependenciesProvider provider = new DependenciesProvider(config);

            IEnumerable<IService> impls = provider.Resolve<IEnumerable<IService>>();
            Assert.IsNotNull(impls);
            Assert.AreEqual(2, (impls as List<IService>).Count);
        }

        [TestMethod]
        public void InnerDependencyTest()
        {
            DependenciesConfiguration config = new DependenciesConfiguration();
            config.Register<ISmth, ClassForISmth>();
            config.Register<IService, FirstForIService>();
            config.Register<IService, SecondClForIService>();
            config.Register<IClient, SecondClassForIClent>();
            DependenciesProvider provider = new DependenciesProvider(config);

            FirstForIService cl = (FirstForIService)provider.Resolve<IService>();
            Assert.IsNotNull(cl.smth);

            SecondClassForIClent cl1 = (SecondClassForIClent)provider.Resolve<IClient>();
            Assert.IsNotNull(cl1.serv);
        }

        [TestMethod]
        public void SimpleRecursionTest()
        {
            DependenciesConfiguration config = new DependenciesConfiguration();
            config.Register<IClient, ClassForIClient>();
            config.Register<IData, ClassForIData>();
            DependenciesProvider provider = new DependenciesProvider(config);

            ClassForIClient cl = (ClassForIClient)provider.Resolve<IClient>();
            Assert.IsNull((cl.data as ClassForIData).cl);
        }

        [TestMethod]
        public void SimpleOpenGenericTest()
        {
            DependenciesConfiguration config = new DependenciesConfiguration();
            config.Register<IAnother<ISmth>, First<ISmth>>();
            config.Register(typeof(IFoo<>), typeof(Second<>));
            DependenciesProvider provider = new DependenciesProvider(config);

            IAnother<ISmth> cl = provider.Resolve<IAnother<ISmth>>();
            Assert.IsNotNull(cl);
            IFoo<IService> cl1 = provider.Resolve<IFoo<IService>>();
            Assert.IsNotNull(cl1);
        }

        [TestMethod]
        public void PolinaTest()
        {
            DependenciesConfiguration conf = new DependenciesConfiguration();
            conf.Register<Polina, Polina>();
            DependenciesProvider prov = new DependenciesProvider(conf);
            Polina polina = prov.Resolve<Polina>();
            Assert.IsNotNull(polina, "polina ne jive(");
        }
    }

    interface IAnother<T>
   where T : ISmth
    {

    }

    class First<T> : IAnother<T>
        where T : ISmth
    {

    }

    interface IFoo<T>
        where T : IService
    {

    }

    class Second<T> : IFoo<T>
        where T : IService
    {

    }

    public interface IHuman
    {

    }

    public class Polina : IHuman
    {

    }
}
