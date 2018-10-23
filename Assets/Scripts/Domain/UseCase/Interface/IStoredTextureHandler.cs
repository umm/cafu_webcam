using System;
using CAFU.WebCam.Domain.Structure.Presentation;
using UniRx;
using IPresenter = CAFU.Core.IPresenter;

namespace CAFU.WebCam.Domain.UseCase
{
    public interface IStoredTextureHandler : IPresenter
    {
        IObservable<Unit> LoadAsObservable();
        IObservable<Unit> SaveAsObservable();
    }
}