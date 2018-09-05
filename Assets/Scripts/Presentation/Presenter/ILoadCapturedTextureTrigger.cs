using System;
using CAFU.Core;
using UniRx;

namespace CAFU.WebCam.Presentation.Presenter
{
    public interface ILoadCapturedTextureTrigger : IView
    {
        IObservable<Unit> TriggerAsObservable();
    }
}