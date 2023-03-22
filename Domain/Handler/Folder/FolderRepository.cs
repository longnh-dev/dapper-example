using DapperExample.Sharedkernel;
using Sharedkernel.Helper;

namespace Domain.Handler
{
    public class FolderRepository : IFolderRepository
    {
        public Task<Response> Add(AddFolderRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Delete(IEnumerable<Folder> listFolderDelete)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Folder>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Response> Move(MoveFolderRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
