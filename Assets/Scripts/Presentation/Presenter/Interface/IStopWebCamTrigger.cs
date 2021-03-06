using System;
using CAFU.Core;
using UniRx;

namespace CAFU.WebCam.Presentation.Presenter
{
    public interface IStopWebCamTrigger : IView
    {
        IObservable<Unit> TriggerAsObservable();
    }
}