using CAFU.Core;
using CAFU.WebCam.Domain.Entity;
using CAFU.WebCam.Domain.Structure.Presentation;
using ExtraUniRx;
using UnityEngine;
using Zenject;

namespace CAFU.WebCam.Domain.Factory
{
    public class StoredTextureEventsTranslator : ITranslator<IWebCamEntity, StoredTextureEvents>
    {
        [Inject] private IFactory<ITenseSubject, ITenseSubject, StoredTextureEvents> Factory { get; }

        public StoredTextureEvents Translate(IWebCamEntity param1)
        {
            return Factory.Create(param1.Load, param1.Save);
        }
    }
}