using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CAFU.Core;
using CAFU.Data.Utility;
using CAFU.WebCam.Application;
using CAFU.WebCam.Domain.Entity;
using CAFU.WebCam.Domain.Structure.Data;
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
        [Inject] private ITranslator<IWebCamEntity, IStorableTexture> StorableTextureTranslator { get; }

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

            // WebCam イベント操作 Presenter
            //   WebCam の状態変化イベントを登録
            StoredTextureHandler
                .RegisterEvents(
                    WebCamEntity.WillRenderStoredTextureSubject
                );
        }

        private void Save()
        {
            ObservableImageRWHandler
                .WriteAsObservable(
                    UriBuilder(Arguments.Name),
                    WebCamEntity.StorableTextureProperty.Value
                )
                .Subscribe();
        }

        private void Load()
        {
            ObservableImageRWHandler
                .ReadAsObservable(
                    UriBuilder(Arguments.Name)
                )
                // MainThread に戻す前に配列の変換を行っておく
                .Select(x => new Tuple<IStorableTexture, IEnumerable<Color32>>(x, ArrayConverter.ByteArrayToColor32Array(x.Data)))
                // CreateTexture2D 内で UnityEngine.Texture2D などの API を触るため、MeinThread に戻す
                .ObserveOnMainThread()
                .Select(t => CreateTexture2D(t.Item1, t.Item2))
                // OnCompleted は流さない
                .Subscribe(x => WebCamEntity.WillRenderStoredTextureSubject.OnNext(x));
        }

        private static Texture2D CreateTexture2D(IStorableTexture storableTexture, IEnumerable<Color32> colors)
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