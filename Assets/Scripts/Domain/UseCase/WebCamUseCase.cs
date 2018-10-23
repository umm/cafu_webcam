using System;
using System.Linq;
using CAFU.Core;
using CAFU.WebCam.Domain.Entity;
using CAFU.WebCam.Domain.Structure.Data;
using CAFU.WebCam.Domain.Structure.Presentation;
using ExtraUniRx;
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
        [Inject] private ITranslator<IWebCamEntity, StorableTexture> StorableTextureTranslator { get; }
        [Inject] private ITranslator<IWebCamEntity, WebCamEvents> WebCamEventsTranslator { get; }

        // Presenters
        [Inject] private IWebCamController WebCamController { get; }
        [Inject] private IWebCamInitializer WebCamInitializer { get; }
        [Inject] private IWebCamEventsProvider WebCamEventsProvider { get; }

        public void Initialize()
        {
            // WebCam 操作 Presenter
            //   各イベントが叩かれたら対応するメソッドを呼び出し
            WebCamController.TriggerPlayAsObservable().Subscribe(_ => Play());
            WebCamController.TriggerStopAsObservable().Subscribe(_ => Stop());
            WebCamController.TriggerCaptureAsObservable().Subscribe(_ => Capture());

            // WebCam イベント操作 Presenter
            //   WebCam の状態変化イベントを提供
            WebCamEventsProvider.Provide(WebCamEventsTranslator.Translate(WebCamEntity));

            // WebCam 初期化 Presenter
            WebCamInitializer.InitializeAsObservable().Subscribe(_ => InitializeCamera());

            // WebCamTexture サイズ確定
            //   XXX: イベントの伝搬が微妙
            WebCamEntity
                .Play
                .WhenDid()
                .SelectMany(
                    _ =>
                        Observable
                            .EveryUpdate()
                            .Where(__ => WebCamEntity.HasResolutionConfirmed())
                            .Take(1)
                )
                .Select(_ => new Vector2Int(WebCamEntity.WebCamTextureProperty.Value.width, WebCamEntity.WebCamTextureProperty.Value.height))
                .Subscribe(WebCamEntity.ConfirmTextureSize.Did);
            WebCamEntity
                .ConfirmTextureSize
                .WhenDid()
                .Select(_ => WebCamEntity.WebCamTextureProperty.Value)
                .Subscribe(WebCamEntity.RenderWebCamTexture.Will);
        }

        private void Play()
        {
            if (WebCamEntity.WebCamTextureProperty.Value == default(WebCamTexture))
            {
                return;
            }

            WebCamEntity.Play.Will(WebCamEntity.WebCamTextureProperty.Value);
            WebCamEntity.WebCamTextureProperty.Value.Play();
            WebCamEntity.Play.Did(WebCamEntity.WebCamTextureProperty.Value);
        }

        private void Stop()
        {
            if (WebCamEntity.WebCamTextureProperty.Value == default(WebCamTexture))
            {
                return;
            }

            WebCamEntity.Stop.Will(WebCamEntity.WebCamTextureProperty.Value);
            WebCamEntity.WebCamTextureProperty.Value.Stop();
            WebCamEntity.Stop.Did(WebCamEntity.WebCamTextureProperty.Value);
        }

        private void Capture()
        {
            if (WebCamEntity.WebCamTextureProperty.Value == default(WebCamTexture) || !WebCamEntity.WebCamTextureProperty.Value.isPlaying)
            {
                return;
            }

            WebCamEntity.Capture.Will(WebCamEntity.WebCamTextureProperty.Value);
            WebCamEntity.StorableTextureProperty.OnNext(StorableTextureTranslator.Translate(WebCamEntity));
            WebCamEntity.Capture.Did(WebCamEntity.WebCamTextureProperty.Value);
        }

        private void InitializeCamera()
        {
            WebCamEntity.Initialize.Will();
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
            WebCamEntity.Initialize.Did();
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