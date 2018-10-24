using CAFU.Core;
using ExtraUniRx;
using UnityEngine;
using Zenject;

namespace CAFU.WebCam.Domain.Structure.Presentation
{
    public struct StoredTextureEvents : IStructure
    {
        public ITenseSubject Load { get; private set; }
        public ITenseSubject Save { get; private set; }
        public ITenseSubject<Texture> Render { get; set; }
        public ITenseSubject Clear { get; set; }

        public class Factory : IFactory<ITenseSubject, ITenseSubject, ITenseSubject<Texture>, ITenseSubject, StoredTextureEvents>
        {
            public StoredTextureEvents Create(ITenseSubject param1, ITenseSubject param2, ITenseSubject<Texture> param3, ITenseSubject param4)
            {
                return new StoredTextureEvents
                {
                    Load = param1,
                    Save = param2,
                    Render = param3,
                    Clear = param4,
                };
            }
        }
    }
}