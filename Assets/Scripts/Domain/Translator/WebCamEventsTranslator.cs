using CAFU.Core;
using CAFU.WebCam.Domain.Entity;
using CAFU.WebCam.Domain.Structure.Presentation;
using ExtraUniRx;
using UnityEngine;
using Zenject;

namespace CAFU.WebCam.Domain.Factory
{
    public class WebCamEventsTranslator : ITranslator<IWebCamEntity, WebCamEvents>
    {
        [Inject] private IFactory<
            ITenseSubject,             // Initialize
            ITenseSubject<Texture>,    // Play
            ITenseSubject<Texture>,    // Stop
            ITenseSubject<Texture>,    // RenderWebCamTexture
            ITenseSubject<Texture>,    // Capture
            ITenseSubject<Vector2Int>, // ConfirmTextureSize
            WebCamEvents
        > Factory { get; }

        public WebCamEvents Translate(IWebCamEntity param1)
        {
            return Factory.Create(
                param1.Initialize,
                param1.Play,
                param1.Stop,
                param1.RenderWebCamTexture,
                param1.Capture,
                param1.ConfirmTextureSize
            );
        }
    }
}