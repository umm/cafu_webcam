﻿using CAFU.WebCam.Domain.Structure.Presentation;
using CAFU.WebCam.Domain.UseCase;
using CAFU.WebCam.Presentation.Presenter;
using Zenject;

namespace CAFU.WebCam.Application.Installer
{
    public class WebCamInstaller : Installer<WebCamInstaller>
    {
        public override void InstallBindings()
        {
            EntityInstaller.Install(Container);
            TranslatorInstaller.Install(Container);

            // Structures
            Container.Bind<WebCamEvents>().AsCached();

            // UseCases
            Container
                .BindInterfacesAndSelfTo<WebCamUseCase>()
                .AsCached()
                .WithArguments(
                    new WebCamUseCase.InitializeArguments(
                        640,
                        480,
                        "",
                        true
                    )
                );

            // Presenters
            Container.BindInterfacesAndSelfTo<WebCamPresenter>().AsCached();
        }
    }
}