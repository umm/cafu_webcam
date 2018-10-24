using CAFU.Data.Data.DataStore;
using CAFU.WebCam.Data.Repository;
using CAFU.WebCam.Domain.Structure.Presentation;
using CAFU.WebCam.Domain.UseCase;
using CAFU.WebCam.Presentation.Presenter;
using Zenject;

namespace CAFU.WebCam.Application.Installer
{
    public class CapturedTextureInstaller : Installer<CapturedTextureInstaller>
    {
        public override void InstallBindings()
        {
            EntityInstaller.Install(Container);

            // Structures
            Container.Bind<StoredTextureEvents>().AsCached();

            // UseCases
            Container
                .BindInterfacesAndSelfTo<CapturedTextureUseCase>()
                .AsCached()
                .WithArguments(new CapturedTextureUseCase.InitializeArguments("CapturedTexture"));

            // Repositories
            Container.BindInterfacesAndSelfTo<ImageRepository>().AsSingle();

            // DataStores
            Container.BindInterfacesAndSelfTo<ObservableLocalStorageDataStore>().AsSingle();

            // Presenters
            Container.BindInterfacesAndSelfTo<StoredTexturePresenter>().AsCached();
        }
    }
}
