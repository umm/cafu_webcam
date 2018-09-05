using CAFU.WebCam.Domain.Entity;
using Zenject;

namespace CAFU.WebCam.Application.Installer
{
    public class EntityInstaller : Installer<EntityInstaller>
    {
        public override void InstallBindings()
        {
            if (!Container.HasBinding<WebCamEntity>())
            {
                Container.BindInterfacesAndSelfTo<WebCamEntity>().AsCached();
            }
        }
    }
}