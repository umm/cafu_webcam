using CAFU.Core;
using CAFU.WebCam.Domain.Structure.Presentation;

namespace CAFU.WebCam.Presentation.Presenter
{
    public interface IWebCamEventsHandler : IView
    {
        void Handle(WebCamEvents events);
    }
}