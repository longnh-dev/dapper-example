using DapperExample.Sharedkernel;
using Sharedkernel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handler
{
    public interface IFolderRepository
    {
        public Task<IEnumerable<Folder>> GetAll();
        public Task<Response<Folder>> GetById(string id);
        public Task<Response> Add(AddFolderRequest request);
        public Task<Response> Delete(IEnumerable<Folder> listFolderDelete);
        public Task<Response> Move(MoveFolderRequest request);

    }
}
