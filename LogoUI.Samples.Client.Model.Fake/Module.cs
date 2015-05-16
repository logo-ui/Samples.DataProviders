using System.ComponentModel.Composition;
using LogoFX.UI.Bootstrapping.SimpleContainer;
using LogoUI.Samples.Client.Model.Contracts;
using Solid.Practices.Modularity;

namespace LogoUI.Samples.Client.Model.Fake
{
    [Export(typeof(ICompositionModule))]
    public class Module : ICompositionModule<ExtendedSimpleIocContainer>
    {
        public void RegisterModule(ExtendedSimpleIocContainer container)
        {
            container.RegisterSingleton<ILoginService, FakeLoginService>();
            container.RegisterTransient<IDataService, FakeDataService>();            
        }
    }
}
