using CAFU.Core;
using UnityEngine;

namespace CAFU.WebCam.Domain.Structure.Presentation
{
    public class RenderableTexture : IStructure
    {
        public Texture2D Texture { get; }
        public int RotationAngle { get; }
        public bool VerticallyMirrored { get; }

        public RenderableTexture(Texture2D texture, int rotationAngle, bool verticallyMirrored)
        {
            Texture = texture;
            RotationAngle = rotationAngle;
            VerticallyMirrored = verticallyMirrored;
        }
    }
}