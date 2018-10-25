using CAFU.WebCam.Domain.Factory;
using Zenject;

namespace CAFU.WebCam.Application.Installer
{
    public class TranslatorInstaller : Installer<TranslatorInstaller>
    {
        public override void InstallBindings()
        {
            if (!Container.HasBinding<StorableTextureTranslator>())
            {
                Container.BindInterfacesAndSelfTo<StorableTextureTranslator>().AsCached();
            }
        }
    }
}