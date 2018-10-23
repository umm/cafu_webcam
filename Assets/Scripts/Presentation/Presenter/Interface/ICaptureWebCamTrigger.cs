using System;
using CAFU.Core;
using UniRx;

namespace CAFU.WebCam.Presentation.Presenter
{
    public interface ICaptureWebCamTrigger : IView
    {
        IObservable<Unit> TriggerAsObservable();
    }
}