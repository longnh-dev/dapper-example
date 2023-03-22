using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharedkernel.Helper
{
    public class Folder
    {
        
        public string? Id { get; set; }
        public string? NextId { get; set; }
        public string? ParentId { get; set; }
        public Int16? Level { get; set; }
        public string? FolderName { get; set; }
        public bool? IsFile { get; set; }
        public Folder(string? id, string? nextId, string? parentId, string? folderName, Int16? level, bool? isFile)
        {
            Id = id;
            NextId = nextId;
            ParentId = parentId;
            FolderName = folderName;
            Level = level;
            IsFile = isFile;
        }
    }
}
