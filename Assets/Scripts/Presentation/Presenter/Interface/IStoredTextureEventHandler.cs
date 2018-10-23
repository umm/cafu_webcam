using CAFU.Core;
using CAFU.WebCam.Domain.Structure.Presentation;

namespace CAFU.WebCam.Presentation.Presenter
{
    public interface IStoredTextureEventHandler : IView
    {
        void Handle(StoredTextureEvents events);
    }
}