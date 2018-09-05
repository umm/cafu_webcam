using System;
using UniRx;
using UnityEngine;
using IPresenter = CAFU.Core.IPresenter;

namespace CAFU.WebCam.Domain.UseCase
{
    public interface IStoredTextureHandler : IPresenter
    {
        IObservable<Unit> LoadAsObservable();
        IObservable<Unit> SaveAsObservable();
        void RegisterEvents(
            IObservable<Texture> renderObservable
        );
    }
}