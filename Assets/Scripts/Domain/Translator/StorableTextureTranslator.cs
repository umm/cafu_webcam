using CAFU.Core;
using CAFU.Data.Utility;
using CAFU.WebCam.Domain.Entity;
using CAFU.WebCam.Domain.Structure.Data;
using UnityEngine;

namespace CAFU.WebCam.Domain.Factory
{
    public class StorableTextureTranslator : ITranslator<IWebCamEntity, StorableTexture>
    {
        public StorableTexture Translate(IWebCamEntity param)
        {
            return new StorableTexture
            {
                Width = param.WebCamTextureProperty.Value.width,
                Height = param.WebCamTextureProperty.Value.height,
                TextureFormat = TextureFormat.RGBA32,
                MipChain = false,
                RotationAngle = param.WebCamTextureProperty.Value.videoRotationAngle,
                Data = ArrayConverter.Color32ArrayToByteArray(param.WebCamTextureProperty.Value.GetPixels32()),
            };
        }
    }
}