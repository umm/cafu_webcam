using System;
using CAFU.Core;
using UnityEngine;

namespace CAFU.WebCam.Domain.UseCase
{
    public interface IWebCamEventHandler : IPresenter
    {
        void RegisterEvents(
            IObservable<Texture> willPlayObservable,
            IObservable<Texture> didPlayObservable,
            IObservable<Texture> willStopObservable,
            IObservable<Texture> didStopObservable,
            IObservable<Texture> willRenderObservable,
            IObservable<Texture> didRenderObservable,
            IObservable<Vector2Int> didConfirmTextureSizeObservable
        );

        void OnInitialized();
    }
}