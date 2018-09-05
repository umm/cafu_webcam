using System.Collections;
using CAFU.WebCam.Domain.Entity;
using Moq;
using NUnit.Framework;
using UniRx;
using UnityEngine.TestTools;
using Zenject;

namespace CAFU.WebCam.Domain.UseCase
{
    public class WebCamUseCaseTest : ZenjectIntegrationTestFixture
    {
        [Inject] private IWebCamEntity WebCamEntity { get; }

        [SetUp]
        public void CommonInstall()
        {
            PreInstall();

            Container
                .BindInterfacesAndSelfTo<WebCamUseCase>()
                .AsCached()
                .WithArguments(
                    new WebCamUseCase.InitializeArguments(
                        10,
                        10,
                        "",
                        true
                    )
                );
            Container.BindInterfacesAndSelfTo<WebCamEntity>().AsCached();
            var mockPresenter = new Mock<IWebCamController>();
            mockPresenter.Setup(x => x.TriggerPlayAsObservable()).Returns(Observable.Empty<Unit>());
            mockPresenter.Setup(x => x.TriggerStopAsObservable()).Returns(Observable.Empty<Unit>());
            Container.BindInstance(mockPresenter.Object);

            PostInstall();
        }

        [UnityTest]
        public IEnumerator WebCamDeviceTest()
        {
            // 初期化によって WebCamPresenter が1件見付かるコトを期待している。
            // 思いっきりデバイス・マシン依存だが、他に良案浮かばず。
            Assert.IsNotNull(WebCamEntity.WebCamDeviceProperty.Value.name);
            yield break;
        }
    }
}