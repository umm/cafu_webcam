using System;
using CAFU.Core;
using CAFU.WebCam.Application;
using CAFU.WebCam.Domain.Structure.Data;
using ExtraUniRx;
using UniRx;
using UnityEngine;

namespace CAFU.WebCam.Domain.Entity
{
    public interface IWebCamEntity : IEntity
    {
        SubjectProperty<WebCamTexture> WebCamTextureProperty { get; }
        SubjectProperty<WebCamDevice> WebCamDeviceProperty { get; }
        SubjectProperty<IStorableTexture> StorableTextureProperty { get; }
        ISubject<Texture> WillPlaySubject { get; }
        ISubject<Texture> DidPlaySubject { get; }
        ISubject<Texture> WillStopSubject { get; }
        ISubject<Texture> DidStopSubject { get; }
        ISubject<Texture> WillRenderWebCamTextureSubject { get; }
        ISubject<Texture> DidRenderWebCamTextureSubject { get; }
        ISubject<Texture> WillRenderStoredTextureSubject { get; }
        ISubject<Texture> DidRenderStoredTextureSubject { get; }
        ISubject<Texture> WillCaptureSubject { get; }
        ISubject<Texture> DidCaptureSubject { get; }
        ISubject<Vector2Int> DidConfirmTextureSizeSubject { get; }

        bool HasResolutionConfirmed();
    }

    [Serializable]
    public class WebCamEntity : IWebCamEntity
    {
        public SubjectProperty<WebCamTexture> WebCamTextureProperty { get; } = new SubjectProperty<WebCamTexture>();
        public SubjectProperty<WebCamDevice> WebCamDeviceProperty { get; } = new SubjectProperty<WebCamDevice>();
        public SubjectProperty<IStorableTexture> StorableTextureProperty { get; } = new SubjectProperty<IStorableTexture>();
        public ISubject<Texture> WillPlaySubject { get; } = new Subject<Texture>();
        public ISubject<Texture> DidPlaySubject { get; } = new Subject<Texture>();
        public ISubject<Texture> WillStopSubject { get; } = new Subject<Texture>();
        public ISubject<Texture> DidStopSubject { get; } = new Subject<Texture>();
        public ISubject<Texture> WillRenderWebCamTextureSubject { get; } = new Subject<Texture>();
        public ISubject<Texture> DidRenderWebCamTextureSubject { get; } = new Subject<Texture>();
        public ISubject<Texture> WillRenderStoredTextureSubject { get; } = new Subject<Texture>();
        public ISubject<Texture> DidRenderStoredTextureSubject { get; } = new Subject<Texture>();
        public ISubject<Texture> WillCaptureSubject { get; } = new Subject<Texture>();
        public ISubject<Texture> DidCaptureSubject { get; } = new Subject<Texture>();
        public ISubject<Vector2Int> DidConfirmTextureSizeSubject { get; } = new Subject<Vector2Int>();

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