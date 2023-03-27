using Sharedkernel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handler
{
    public class MoveFolderRequest
    {
        public IEnumerable<Folder> ListFolderMove { get; set; }
        public Folder? NextFolder { get; set; }
    }
    public class AddFolderRequest
    {
        public IEnumerable<Folder> ListFolderAdd { get; set; }
        public Folder? NextFolder { get; set; }

    }
}
