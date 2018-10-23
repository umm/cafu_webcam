using System;
using UniRx;
using UnityEngine;
using IPresenter = CAFU.Core.IPresenter;

namespace CAFU.WebCam.Domain.UseCase
{
    public interface IWebCamController : IPresenter
    {
        IObservable<Unit> TriggerPlayAsObservable();
        IObservable<Unit> TriggerStopAsObservable();
        IObservable<Unit> TriggerCaptureAsObservable();
    }
}