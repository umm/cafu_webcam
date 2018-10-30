using System;
using CAFU.Core;
using CAFU.WebCam.Application;
using CAFU.WebCam.Domain.Structure.Data;
using ExtraUniRx;
using UnityEngine;

namespace CAFU.WebCam.Domain.Entity
{
    public interface IWebCamEntity : IEntity
    {
        SubjectProperty<WebCamTexture> WebCamTextureProperty { get; }
        SubjectProperty<WebCamDevice> WebCamDeviceProperty { get; }
        SubjectProperty<StorableTexture> StorableTextureProperty { get; }

        ITenseSubject Initialize { get; }
        ITenseSubject<Texture> Play { get; }
        ITenseSubject<Texture> Stop { get; }
        ITenseSubject<WebCamTexture> RenderWebCamTexture { get; }
        ITenseSubject ClearWebCamTexture { get; }
        ITenseSubject<Texture> Capture { get; }
        ITenseSubject<Vector2Int> ConfirmTextureSize { get; }
        ITenseSubject Save { get; }
        ITenseSubject Load { get; }
        ITenseSubject<Texture> RenderStoredTexture { get; }
        ITenseSubject ClearStoredTexture { get; }
        ITenseSubject<int> ConfirmTextureRotationAngle { get; }

        bool HasResolutionConfirmed();
    }

    [Serializable]
    public class WebCamEntity : IWebCamEntity
    {
        public SubjectProperty<WebCamTexture> WebCamTextureProperty { get; } = new SubjectProperty<WebCamTexture>();
        public SubjectProperty<WebCamDevice> WebCamDeviceProperty { get; } = new SubjectProperty<WebCamDevice>();
        public SubjectProperty<StorableTexture> StorableTextureProperty { get; } = new SubjectProperty<StorableTexture>();

        public ITenseSubject Initialize { get; } = new TenseSubject();
        public ITenseSubject<Texture> Play { get; } = new TenseSubject<Texture>();
        public ITenseSubject<Texture> Stop { get; } = new TenseSubject<Texture>();
        public ITenseSubject<WebCamTexture> RenderWebCamTexture { get; } = new TenseSubject<WebCamTexture>();
        public ITenseSubject ClearWebCamTexture { get; } = new TenseSubject();
        public ITenseSubject<Texture> Capture { get; } = new TenseSubject<Texture>();
        public ITenseSubject<Vector2Int> ConfirmTextureSize { get; } = new TenseSubject<Vector2Int>();
        public ITenseSubject Save { get; } = new TenseSubject();
        public ITenseSubject Load { get; } = new TenseSubject();
        public ITenseSubject<Texture> RenderStoredTexture { get; } = new TenseSubject<Texture>();
        public ITenseSubject ClearStoredTexture { get; } = new TenseSubject();
        public ITenseSubject<int> ConfirmTextureRotationAngle { get; } = new TenseSubject<int>();

        public bool HasResolutionConfirmed()
        {
            return
                WebCamTextureProperty.Value != default(WebCamTexture)
                && WebCamTextureProperty.Value.isPlaying
                && WebCamTextureProperty.Value.width >= Constant.MinimumWebCamTextureSize.x
                && WebCamTextureProperty.Value.height >= Constant.MinimumWebCamTextureSize.y;
        }
    }
}