using System;
using System.Collections.Generic;
using CAFU.Core;
using UnityEngine;

namespace CAFU.WebCam.Domain.Structure.Data
{
    [Serializable]
    public struct StorableTexture : IStructure
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private TextureFormat textureFormat;
        [SerializeField] private bool mipChain;
        [SerializeField] private int rotationAngle;
        [SerializeField] private bool verticallyMirrored;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public TextureFormat TextureFormat
        {
            get { return textureFormat; }
            set { textureFormat = value; }
        }

        public bool MipChain
        {
            get { return mipChain; }
            set { mipChain = value; }
        }

        public int RotationAngle
        {
            get { return rotationAngle; }
            set { rotationAngle = value; }
        }

        public bool VerticallyMirrored
        {
            get { return verticallyMirrored; }
            set { verticallyMirrored = value; }
        }

        public IEnumerable<byte> Data { get; set; }
    }
}