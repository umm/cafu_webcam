using CAFU.Core;
using CAFU.WebCam.Domain.Entity;
using ExtraUniRx;
using UnityEngine;
using Zenject;

namespace CAFU.WebCam.Domain.Structure.Presentation
{
    public struct StoredTextureEvents : IStructure
    {
        public ITenseSubject Load { get; }
        public ITenseSubject Save { get; }
        public ITenseSubject<Texture> Render { get; }
        public ITenseSubject Clear { get; }

        public StoredTextureEvents(IWebCamEntity entity)
        {
            Load = entity.Load;
            Save = entity.Save;
            Render = entity.RenderStoredTexture;
            Clear = entity.ClearStoredTexture;
        }
    }
}