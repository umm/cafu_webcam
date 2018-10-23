using CAFU.Core;
using CAFU.WebCam.Domain.Structure.Presentation;

namespace CAFU.WebCam.Domain.UseCase
{
    public interface IStoredTextureEventsProvider : IPresenter
    {
        void Provide(StoredTextureEvents events);
    }
}