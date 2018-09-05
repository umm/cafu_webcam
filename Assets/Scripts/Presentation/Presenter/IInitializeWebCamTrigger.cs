using System;
using CAFU.Core;
using UniRx;

namespace CAFU.WebCam.Presentation.Presenter
{
    public interface IInitializeWebCamTrigger : IView
    {
        IObservable<Unit> TriggerAsObservable();
    }
}