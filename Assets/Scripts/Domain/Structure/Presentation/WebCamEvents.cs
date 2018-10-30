using CAFU.Core;
using CAFU.WebCam.Domain.Entity;
using ExtraUniRx;
using UnityEngine;

namespace CAFU.WebCam.Domain.Structure.Presentation
{
    public struct WebCamEvents : IStructure
    {
        public ITenseSubject Initialize { get; }
        public ITenseSubject<Texture> Play { get; }
        public ITenseSubject<Texture> Stop { get; }
        public ITenseSubject<WebCamTexture> Render { get; }
        public ITenseSubject Clear { get; }
        public ITenseSubject<Texture> Capture { get; }
        public ITenseSubject<Vector2Int> ConfirmTextureSize { get; }

        public WebCamEvents(IWebCamEntity entity)
        {
            Initialize = entity.Initialize;
            Play = entity.Play;
            Stop = entity.Stop;
            Render = entity.RenderWebCamTexture;
            Clear = entity.ClearWebCamTexture;
            Capture = entity.Capture;
            ConfirmTextureSize = entity.ConfirmTextureSize;
        }
    }
}