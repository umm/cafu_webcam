using CAFU.Core;
using UnityEngine;

namespace CAFU.WebCam.Presentation.Presenter
{
    public interface IWebCamTextureRenderer : IView
    {
        void Render(Texture texture);

        void Clear();
    }
}