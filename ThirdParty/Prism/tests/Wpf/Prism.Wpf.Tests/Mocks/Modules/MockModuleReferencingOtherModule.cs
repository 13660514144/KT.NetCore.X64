using Prism.Ioc;
using Prism.Modularity;
using System;

namespace Prism.Wpf.Tests.Mocks.Modules
{
    public class MockModuleReferencingOtherModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            throw new NotImplementedException();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            throw new NotImplementedException();
        }
    }

    public class MyDummyClass : DummyClass {}
}
