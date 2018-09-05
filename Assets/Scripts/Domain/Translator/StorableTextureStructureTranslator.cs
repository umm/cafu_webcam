using CAFU.Core;
using CAFU.Data.Utility;
using CAFU.WebCam.Domain.Entity;
using CAFU.WebCam.Domain.Structure.Data;
using UnityEngine;

namespace CAFU.WebCam.Domain.Factory
{
    public class StorableTextureStructureTranslator : ITranslator<IWebCamEntity, IStorableTexture>
    {
        public IStorableTexture Translate(IWebCamEntity param)
        {
            return new StorableTexture
            {
                Width = param.WebCamTextureProperty.Value.width,
                Height = param.WebCamTextureProperty.Value.height,
                TextureFormat = TextureFormat.RGBA32,
                MipChain = false,
                Data = ArrayConverter.Color32ArrayToByteArray(param.WebCamTextureProperty.Value.GetPixels32()),
            };
        }
    }
}