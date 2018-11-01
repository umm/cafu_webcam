using System;
using System.Collections.Generic;
using System.Linq;
using CAFU.WebCam.Domain.Structure.Presentation;
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
        [InjectOptional] private ILoadCapturedTextureTrigger LoadCapturedTextureTrigger { get; set; }
        [InjectOptional] private ISaveCapturedTextureTrigger SaveCapturedTextureTrigger { get; set; }

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
    }
}