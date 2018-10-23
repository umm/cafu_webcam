using System;
using UniRx;
using IPresenter = CAFU.Core.IPresenter;

namespace CAFU.WebCam.Domain.UseCase
{
    public interface IWebCamInitializer : IPresenter
    {
        IObservable<Unit> InitializeAsObservable();
    }
}