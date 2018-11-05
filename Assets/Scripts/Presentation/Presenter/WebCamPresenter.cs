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
        [Inject] private IPlayWebCamTrigger PlayWebCamTrigger { get; set; }

        [Inject] private IStopWebCamTrigger StopWebCamTrigger { get; set; }

        [Inject] private ICaptureWebCamTrigger CaptureWebCamTrigger { get; set; }

        [Inject] private IInitializeWebCamTrigger InitializeWebCamTrigger { get; set; }

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