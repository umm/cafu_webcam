using CAFU.Core;
using CAFU.WebCam.Domain.Entity;
using CAFU.WebCam.Domain.Structure.Presentation;

namespace CAFU.WebCam.Domain.Factory
{
    public class WebCamEventsTranslator : ITranslator<IWebCamEntity, WebCamEvents>
    {
        public WebCamEvents Translate(IWebCamEntity param1)
        {
            return new WebCamEvents.Factory().Create(
                param1.Initialize,
                param1.Play,
                param1.Stop,
                param1.RenderWebCamTexture,
                param1.ClearWebCamTexture,
                param1.Capture,
                param1.ConfirmTextureSize
            );
        }
    }
}