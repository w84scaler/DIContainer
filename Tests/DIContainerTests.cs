using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIContainer;

namespace Tests
{
    [TestClass]
    public class DIContainerTests
    {
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

    public interface IHuman
    {

    }

    public class Polina : IHuman
    {

    }
}
