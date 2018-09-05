using System;
using CAFU.WebCam.Domain.UseCase;
using UniRx;
using UnityEngine;
using Zenject;

namespace CAFU.WebCam.Presentation.Presenter
{
    public class WebCamPresenter :
        IWebCamInitializer,
        IWebCamController,
        IWebCamEventHandler
    {
        [Inject] private IPlayWebCamTrigger PlayWebCamTrigger { get; }

        [Inject] private IStopWebCamTrigger StopWebCamTrigger { get; }

        [Inject] private ICaptureWebCamTrigger CaptureWebCamTrigger { get; }

        [InjectOptional] private IWebCamTextureRenderer WebCamTextureRenderer { get; }

        [Inject] private IInitializeWebCamTrigger InitializeWebCamTrigger { get; }

        [InjectOptional] private IWebCamPlayEventHandler WebCamPlayEventHandler { get; }
        [InjectOptional] private IWebCamStopEventHandler WebCamStopEventHandler { get; }
        [InjectOptional] private IWebCamConfirmTextureSizeEventHandler WebCamConfirmTextureSizeEventHandler { get; }

        public IObservable<Unit> InitializeAsObservable()
        {
            return InitializeWebCamTrigger.TriggerAsObservable();
        }

        public void RegisterEvents(
            IObservable<Texture> willPlayObservable,
            IObservable<Texture> didPlayObservable,
            IObservable<Texture> willStopObservable,
            IObservable<Texture> didStopObservable,
            IObservable<Texture> willRenderObservable,
            IObservable<Texture> didRenderObservable,
            IObservable<Vector2Int> didConfirmTextureSizeObservable
        )
        {
            // キャプチャイベントのみ購読
            didPlayObservable.Subscribe(x => WebCamPlayEventHandler?.DidPlay(x));
            willStopObservable.Subscribe(x => WebCamStopEventHandler?.WillStop(x));
            willRenderObservable.Subscribe(x => WebCamTextureRenderer?.Render(x));
            didConfirmTextureSizeObservable.Subscribe(x => WebCamConfirmTextureSizeEventHandler?.DidConfirmTextureSize(x));
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