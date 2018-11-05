using CAFU.Core;
using CAFU.WebCam.Domain.Structure.Presentation;
using UnityEngine;

namespace CAFU.WebCam.Domain.Factory
{
    public class RenderableTextureTranslator : ITranslator<Texture2D, int, bool, RenderableTexture>
    {
        public RenderableTexture Translate(Texture2D param1, int param2, bool param3)
        {
            return new RenderableTexture(param1, param2, param3);
        }
    }
}