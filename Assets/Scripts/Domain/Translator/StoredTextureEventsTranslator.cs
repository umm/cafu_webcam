using CAFU.Core;
using CAFU.WebCam.Domain.Entity;
using CAFU.WebCam.Domain.Structure.Presentation;

namespace CAFU.WebCam.Domain.Factory
{
    public class StoredTextureEventsTranslator : ITranslator<IWebCamEntity, StoredTextureEvents>
    {
        public StoredTextureEvents Translate(IWebCamEntity param1)
        {
            return new StoredTextureEvents.Factory().Create(
                param1.Load,
                param1.Save,
                param1.RenderStoredTexture,
                param1.ClearStoredTexture
            );
        }
    }
}