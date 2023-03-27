using DapperExample.Sharedkernel;
using Sharedkernel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handler
{
    public interface IFolderService
    {
        public IEnumerable<TreeItem<Folder>> GetTreeFolderFromDB();
        public IEnumerable<TreeItem<Folder>> ConvertFlatToTree(IEnumerable<Folder> folderFlat);
    }
}
