using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CAFU.Core;
using CAFU.Data.Utility;
using CAFU.WebCam.Application;
using CAFU.WebCam.Domain.Entity;
using CAFU.WebCam.Domain.Structure.Data;
using CAFU.WebCam.Domain.Structure.Presentation;
using ExtraUniRx;
using UniRx;
using UnityEngine;
using Zenject;

namespace CAFU.WebCam.Domain.UseCase
{
    public interface ICapturedTextureUseCase : IUseCase
    {
    }

    public class CapturedTextureUseCase : ICapturedTextureUseCase, IInitializable
    {
        // Primitives
        [Inject] private InitializeArguments Arguments { get; }

        // Entities
        [Inject] private IWebCamEntity WebCamEntity { get; }

        // Presenters
        [Inject] private IStoredTextureHandler StoredTextureHandler { get; }

        // Repositories
        [Inject] private IObservableImageRWHandler ObservableImageRWHandler { get; }

        // Translators
        [Inject] private ITranslator<IWebCamEntity, StorableTexture> StorableTextureTranslator { get; }

        [InjectOptional(Id = Constant.InjectId.UriBuilder)]
        private Func<string, Uri> UriBuilder { get; } =
            (name) =>
                new UriBuilder
                {
                    Scheme = "file",
                    Host = string.Empty,
                    Path = Path.Combine(UnityEngine.Application.persistentDataPath, "Temp", name)
                }.Uri;

        [Inject]
        public void Initialize()
        {
            StoredTextureHandler.SaveAsObservable().Subscribe(_ => Save());
            StoredTextureHandler.LoadAsObservable().Subscribe(_ => Load());
        }

        private void Load()
        {
            WebCamEntity.Load.Will();
            ObservableImageRWHandler
                .ReadAsObservable(
                    UriBuilder(Arguments.Name)
                )
                // MainThread に戻す前に配列の変換を行っておく
                .Select(x => new {StorableTexture = x, Data = ArrayConverter.ByteArrayToColor32Array(x.Data)})
                // CreateTexture2D 内で UnityEngine.Texture2D などの API を触るため、MeinThread に戻す
                .ObserveOnMainThread()
                .Select(x => new {Texture2D = CreateTexture2D(x.StorableTexture, x.Data), x.StorableTexture.RotationAngle})
                // OnCompleted は流さない
                .Subscribe(
                    x =>
                    {
                        WebCamEntity.Load.Did();
                        WebCamEntity.RenderStoredTexture.Did(x.Texture2D);
                        WebCamEntity.ConfirmTextureRotationAngle.Did(x.RotationAngle);
                    }
                );
        }

        private void Save()
        {
            WebCamEntity.Save.Will();
            ObservableImageRWHandler
                .WriteAsObservable(
                    UriBuilder(Arguments.Name),
                    WebCamEntity.StorableTextureProperty.Value
                )
                // WebCamEntity.Save の先で UnityEngine 系の API を触るため、MainThread に戻す
                .ObserveOnMainThread()
                .Subscribe(_ => WebCamEntity.Save.Did());
        }

        private static Texture2D CreateTexture2D(StorableTexture storableTexture, IEnumerable<Color32> colors)
        {
            var texture = new Texture2D(storableTexture.Width, storableTexture.Height, storableTexture.TextureFormat, storableTexture.MipChain);
            texture.SetPixels32(colors.ToArray());
            texture.Apply();
            return texture;
        }

        public struct InitializeArguments
        {
            public string Name { get; }

            public InitializeArguments(string name)
            {
                Name = name;
            }
        }
    }
}