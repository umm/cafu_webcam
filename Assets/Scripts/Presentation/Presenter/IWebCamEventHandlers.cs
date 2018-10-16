using CAFU.Core;
using UnityEngine;

namespace CAFU.WebCam.Presentation.Presenter
{
    public interface IWebCamPlayEventHandler : IView
    {
        void WillPlay(Texture texture);
        void DidPlay(Texture texture);
    }

    public interface IWebCamStopEventHandler : IView
    {
        void WillStop(Texture texture);
        void DidStop(Texture texture);
    }

    public interface IWebCamInitializeEventHandler : IView
    {
        void OnInitialized();
    }

    public interface IWebCamCaptureEventHandler : IView
    {
        void WillCapture(Texture texture);
        void DidCapture(Texture texture);
    }

    public interface IWebCamConfirmTextureSizeEventHandler : IView
    {
        void DidConfirmTextureSize(Vector2Int size);
    }
}