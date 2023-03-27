using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharedkernel.Helper
{
  
        public interface IFolder
        {
            string GetId();
            string GetParentId();
        }



        public class Folder : IFolder
        {
            public Folder(string id, string? nextId, string? parentId, string? folderName, Int16? level, bool? isFile)
            {
                Id = id;
                NextId = nextId;
                ParentId = parentId;
                FolderName = folderName;
                Level = level;
                IsFile = isFile;
            }
            public string? Id { get; set; }
            public string? NextId { get; set; }
            public string? ParentId { get; set; }
            public Int16? Level { get; set; }



            public string? FolderName { get; set; }
            public bool? IsFile { get; set; }



            public string GetId()
            {
                return this.Id;
            }



            public string GetParentId()
            {
                return this.ParentId;
            }
        }
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
