using System;
using CAFU.Core;
using UniRx;

namespace CAFU.WebCam.Presentation.Presenter
{
    public interface ISaveCapturedTextureTrigger : IView
    {
        IObservable<Unit> TriggerAsObservable();
    }
}