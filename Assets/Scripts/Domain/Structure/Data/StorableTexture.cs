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

        public IEnumerable<byte> Data { get; set; }
    }
}