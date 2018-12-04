using System;
using System.Collections.Generic;
using System.Linq;
using CAFU.WebCam.Domain.UseCase;
using UniRx;
using Zenject;

namespace CAFU.WebCam.Presentation.Presenter
{
    public class WebCamPresenter :
        IWebCamInitializer,
        IWebCamController
    {
        [Inject] private IEnumerable<IPlayWebCamTrigger> PlayWebCamTriggerList { get; set; }

        [Inject] private IEnumerable<IStopWebCamTrigger> StopWebCamTriggerList { get; set; }

        [Inject] private IEnumerable<ICaptureWebCamTrigger> CaptureWebCamTriggerList { get; set; }

        [Inject] private IEnumerable<IInitializeWebCamTrigger> InitializeWebCamTriggerList { get; set; }

        public IObservable<Unit> InitializeAsObservable()
        {
            return InitializeWebCamTriggerList.Select(x => x.TriggerAsObservable()).Merge();
        }

        public IObservable<Unit> TriggerPlayAsObservable()
        {
            return PlayWebCamTriggerList.Select(x => x.TriggerAsObservable()).Merge();
        }

        public IObservable<Unit> TriggerStopAsObservable()
        {
            return StopWebCamTriggerList.Select(x => x.TriggerAsObservable()).Merge();
        }

        public IObservable<Unit> TriggerCaptureAsObservable()
        {
            return CaptureWebCamTriggerList.Select(x => x.TriggerAsObservable()).Merge();
        }
    }
}
