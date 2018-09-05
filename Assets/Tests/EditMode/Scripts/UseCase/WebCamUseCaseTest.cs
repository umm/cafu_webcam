using CAFU.WebCam.Domain.Entity;
using Moq;
using NUnit.Framework;
using UniRx;
using Zenject;

namespace CAFU.WebCam.Domain.UseCase
{
    public class WebCamUseCaseTest : ZenjectUnitTestFixture
    {
        [Inject] private IWebCamUseCase UseCase { get; }

        [Inject] private LazyInject<IWebCamController> ControllablePresenter { get; }

        [Inject] private LazyInject<Mock<IWebCamController>> MockPresenter { get; }

        [SetUp]
        public void CommonInstall()
        {
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
        }

        [Test]
        public void CallPlayOrStopTest()
        {
            var mockPresenter = new Mock<IWebCamController>();
            mockPresenter.Setup(x => x.TriggerPlayAsObservable()).Returns(Observable.ReturnUnit());
            mockPresenter.Setup(x => x.TriggerStopAsObservable()).Returns(Observable.ReturnUnit());
            Container.BindInstance(mockPresenter.Object);
            Container.BindInstance(mockPresenter);

            Container.Inject(this);

            // UnitTest の場合自動では呼んでくれないので、手動で呼ぶ
            Container.ResolveAll<IInitializable>().ForEach(x => x.Initialize());

            MockPresenter.Value.Verify(x => x.TriggerPlayAsObservable(), Times.Once);
            MockPresenter.Value.Verify(x => x.TriggerStopAsObservable(), Times.Once);
        }
    }
}