using CAFU.Core;
using ExtraUniRx;
using UnityEngine;
using Zenject;

namespace CAFU.WebCam.Domain.Structure.Presentation
{
    public struct WebCamEvents : IStructure
    {
        public ITenseSubject Initialize { get; private set; }
        public ITenseSubject<Texture> Play { get; private set; }
        public ITenseSubject<Texture> Stop { get; private set; }
        public ITenseSubject<Texture> Render { get; private set; }
        public ITenseSubject<Texture> Capture { get; private set; }
        public ITenseSubject<Vector2Int> ConfirmTextureSize { get; private set; }

        public class Factory : IFactory<ITenseSubject, ITenseSubject<Texture>, ITenseSubject<Texture>, ITenseSubject<Texture>, ITenseSubject<Texture>, ITenseSubject<Vector2Int>, WebCamEvents>
        {
            public WebCamEvents Create(ITenseSubject param1, ITenseSubject<Texture> param2, ITenseSubject<Texture> param3, ITenseSubject<Texture> param4, ITenseSubject<Texture> param5, ITenseSubject<Vector2Int> param6)
            {
                return new WebCamEvents()
                {
                    Initialize = param1,
                    Play = param2,
                    Stop = param3,
                    Render = param4,
                    Capture = param5,
                    ConfirmTextureSize = param6,
                };
            }
        }
    }
}