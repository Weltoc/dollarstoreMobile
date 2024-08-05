using DollarStore.Core.Services;
using DollarStore.Core.Services.Interfaces;
using Ninject.Modules;

namespace DollarStore.Core.Ninject
{
    public class CoreService : NinjectModule
    {
        public override void Load()
        {
            Bind<IVendorService>().To<VendorService>().InSingletonScope();
        }
    }
}
