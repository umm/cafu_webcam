using System;
using CAFU.Core;
using UniRx;

namespace CAFU.WebCam.Presentation.Presenter
{
    public interface IPlayWebCamTrigger : IView
    {
        IObservable<Unit> TriggerAsObservable();
    }
}