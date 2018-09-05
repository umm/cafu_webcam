using System;
using CAFU.Data.Data.UseCase;
using CAFU.WebCam.Domain.Structure.Data;
using UniRx;

namespace CAFU.WebCam.Domain.UseCase
{
    public interface IObservableImageRWHandler : IObservableRWHandler
    {
        new IObservable<IStorableTexture> ReadAsObservable(Uri uri);
        IObservable<Unit> WriteAsObservable(Uri uri, IStorableTexture entity);
    }
}