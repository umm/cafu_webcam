using System;
using System.Linq;
using CAFU.Core;
using CAFU.WebCam.Domain.Entity;
using CAFU.WebCam.Domain.Structure.Data;
using UniRx;
using UnityEngine;
using Zenject;

namespace CAFU.WebCam.Domain.UseCase
{
    public interface IWebCamUseCase : IUseCase
    {
    }

    public class WebCamUseCase : IWebCamUseCase, IInitializable
    {
        [Inject] private InitializeArguments Arguments { get; }

        // Entities
        // XXX: WebCamEntity の分割は検討しても良いかも
        [Inject] private IWebCamEntity WebCamEntity { get; }

        // Translators
        [Inject] private ITranslator<IWebCamEntity, IStorableTexture> StorableTextureTranslator { get; }

        // Presenters
        [Inject] private IWebCamController WebCamController { get; }
        [Inject] private IWebCamInitializer WebCamInitializer { get; }
        [Inject] private IWebCamEventHandler WebCamEventHandler { get; }

        public void Initialize()
        {
            // WebCam 操作 Presenter
            //   各イベントが叩かれたら対応するメソッドを呼び出し
            WebCamController.TriggerPlayAsObservable().Subscribe(_ => Play());
            WebCamController.TriggerStopAsObservable().Subscribe(_ => Stop());
            WebCamController.TriggerCaptureAsObservable().Subscribe(_ => Capture());

            // WebCam イベント操作 Presenter
            //   WebCam の状態変化イベントを登録
            WebCamEventHandler
                .RegisterEvents(
                    WebCamEntity.WillPlaySubject,
                    WebCamEntity.DidPlaySubject,
                    WebCamEntity.WillStopSubject,
                    WebCamEntity.DidStopSubject,
                    WebCamEntity.WillRenderWebCamTextureSubject,
                    WebCamEntity.DidRenderWebCamTextureSubject,
                    WebCamEntity.DidConfirmTextureSizeSubject
                );

            // WebCam 初期化 Presenter
            WebCamInitializer.InitializeAsObservable().Subscribe(_ => InitializeCamera());

            // WebCamTexture サイズ確定
            //   XXX: イベントの伝搬が微妙
            WebCamEntity
                .DidPlaySubject
                .SelectMany(
                    _ =>
                        Observable
                            .EveryUpdate()
                            .Where(__ => WebCamEntity.HasResolutionConfirmed())
                            .Take(1)
                )
                .Select(_ => new Vector2Int(WebCamEntity.WebCamTextureProperty.Value.width, WebCamEntity.WebCamTextureProperty.Value.height))
                .Subscribe(WebCamEntity.DidConfirmTextureSizeSubject);
            WebCamEntity
                .DidConfirmTextureSizeSubject
                .Select(_ => WebCamEntity.WebCamTextureProperty.Value)
                .Subscribe(WebCamEntity.WillRenderWebCamTextureSubject);
        }

        private void Play()
        {
            if (WebCamEntity.WebCamTextureProperty.Value == default(WebCamTexture))
            {
                return;
            }

            WebCamEntity.WillPlaySubject.OnNext(WebCamEntity.WebCamTextureProperty.Value);
            WebCamEntity.WebCamTextureProperty.Value.Play();
            WebCamEntity.DidPlaySubject.OnNext(WebCamEntity.WebCamTextureProperty.Value);
        }

        private void Stop()
        {
            if (WebCamEntity.WebCamTextureProperty.Value == default(WebCamTexture))
            {
                return;
            }

            WebCamEntity.WillStopSubject.OnNext(WebCamEntity.WebCamTextureProperty.Value);
            WebCamEntity.WebCamTextureProperty.Value.Stop();
            WebCamEntity.DidStopSubject.OnNext(WebCamEntity.WebCamTextureProperty.Value);
        }

        private void Capture()
        {
            if (WebCamEntity.WebCamTextureProperty.Value == default(WebCamTexture) || !WebCamEntity.WebCamTextureProperty.Value.isPlaying)
            {
                return;
            }

            WebCamEntity.WillCaptureSubject.OnNext(WebCamEntity.WebCamTextureProperty.Value);
            WebCamEntity.StorableTextureProperty.OnNext(StorableTextureTranslator.Translate(WebCamEntity));
            WebCamEntity.DidCaptureSubject.OnNext(WebCamEntity.WebCamTextureProperty.Value);
        }

        private void InitializeCamera()
        {
            if (WebCamTexture.devices.Length == 0)
            {
                WebCamEntity.WebCamTextureProperty.OnError(new InvalidOperationException("WebCamTexture.devices length is zero. Please confirm permission for Camera access."));
                return;
            }

            var webCamDevice = WebCamTexture.devices.FirstOrDefault(x => x.isFrontFacing == Arguments.IsFrontFacing && (string.IsNullOrEmpty(Arguments.CameraName) || x.name == Arguments.CameraName));
            if (string.IsNullOrEmpty(webCamDevice.name))
            {
                WebCamEntity.WebCamTextureProperty.OnError(new InvalidOperationException($"Could not find camera that matching condition: [Name: {Arguments.CameraName}, FrontFacing: {Arguments.IsFrontFacing.ToString()}]"));
                return;
            }

            WebCamEntity
                .WebCamDeviceProperty
                .OnNext(webCamDevice);
            WebCamEntity
                .WebCamTextureProperty
                .OnNext(
                    new WebCamTexture(
                        webCamDevice.name,
                        Arguments.RequestedWidth,
                        Arguments.RequestedHeight
                    )
                );
            Observable
                .EveryUpdate()
                .Select(_ => WebCamEntity.WebCamTextureProperty.Value)
                .Where(x => x != null && x.width > 4 && x.height > 4)
                .Take(1)
                .Subscribe(_ => WebCamEventHandler.OnInitialized());
        }

        public struct InitializeArguments
        {
            public int RequestedWidth { get; }

            public int RequestedHeight { get; }

            public string CameraName { get; }

            public bool IsFrontFacing { get; }

            public InitializeArguments(int requestedWidth, int requestedHeight, string cameraName, bool isFrontFacing)
            {
                RequestedWidth = requestedWidth;
                RequestedHeight = requestedHeight;
                CameraName = cameraName;
                IsFrontFacing = isFrontFacing;
            }
        }
    }
}