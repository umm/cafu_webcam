using CAFU.Core;
using CAFU.WebCam.Domain.Structure.Presentation;

namespace CAFU.WebCam.Domain.UseCase
{
    public interface IWebCamEventsProvider : IPresenter
    {
        void Provide(WebCamEvents events);
    }
}