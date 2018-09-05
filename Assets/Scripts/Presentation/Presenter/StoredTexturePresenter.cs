using System;
using System.Collections.Generic;
using CAFU.WebCam.Domain.UseCase;
using ExtraZenject;
using UniRx;
using UnityEngine;
using Zenject;

namespace CAFU.WebCam.Presentation.Presenter
{
    public class StoredTexturePresenter :
        IStoredTextureHandler
    {
        [Inject] private List<IStoredTextureRenderer> StoredTextureRendererList { get; }
        [InjectOptional] private ILoadCapturedTextureTrigger LoadCapturedTextureTrigger { get; }
        [InjectOptional] private ISaveCapturedTextureTrigger SaveCapturedTextureTrigger { get; }

        public IObservable<Unit> LoadAsObservable()
        {
            ZenjectAssert.IsInjected(LoadCapturedTextureTrigger);
            return LoadCapturedTextureTrigger?.TriggerAsObservable() ?? Observable.Empty<Unit>();
        }

        public IObservable<Unit> SaveAsObservable()
        {
            ZenjectAssert.IsInjected(SaveCapturedTextureTrigger);
            return SaveCapturedTextureTrigger?.TriggerAsObservable() ?? Observable.Empty<Unit>();
        }

        public void RegisterEvents(IObservable<Texture> renderObservable)
        {
            renderObservable.Subscribe(x => StoredTextureRendererList.ForEach(y => y.Render(x)));
        }
    }
}