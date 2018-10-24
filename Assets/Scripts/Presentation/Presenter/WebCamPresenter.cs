using System;
using System.Collections.Generic;
using System.Linq;
using CAFU.WebCam.Domain.Structure.Presentation;
using CAFU.WebCam.Domain.UseCase;
using UniRx;
using Zenject;

namespace CAFU.WebCam.Presentation.Presenter
{
    public class WebCamPresenter :
        IWebCamInitializer,
        IWebCamController
    {
        [Inject] private IPlayWebCamTrigger PlayWebCamTrigger { get; }

        [Inject] private IStopWebCamTrigger StopWebCamTrigger { get; }

        [Inject] private ICaptureWebCamTrigger CaptureWebCamTrigger { get; }

        [Inject] private IInitializeWebCamTrigger InitializeWebCamTrigger { get; }

        public IObservable<Unit> InitializeAsObservable()
        {
            return InitializeWebCamTrigger.TriggerAsObservable();
        }

        public IObservable<Unit> TriggerPlayAsObservable()
        {
            return PlayWebCamTrigger.TriggerAsObservable();
        }

        public IObservable<Unit> TriggerStopAsObservable()
        {
            return StopWebCamTrigger.TriggerAsObservable();
        }

        public IObservable<Unit> TriggerCaptureAsObservable()
        {
            return CaptureWebCamTrigger.TriggerAsObservable();
        }
    }
}