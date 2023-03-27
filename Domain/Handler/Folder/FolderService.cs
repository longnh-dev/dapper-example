using DapperExample.Sharedkernel;
using Sharedkernel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handler
{
    public class FolderService : IFolderService
    {
        private readonly IFolderRepository _folderRepo;
        public FolderService(IFolderRepository folderRepo)
        {
            _folderRepo = folderRepo;
        }

        public  IEnumerable<TreeItem<Folder>> GetTreeFolderFromDB()
        {
            var flatFolder =  _folderRepo.GetAll();
            var treeFolder = ConvertFlatToTree(flatFolder.Result);
            return treeFolder;
        }

        public IEnumerable<TreeItem<Folder>> ConvertFlatToTree(IEnumerable<Folder> folderFlat)
        {
            Dictionary<string, Folder> dicSupportFindData = new Dictionary<string, Folder>();
            Dictionary<string, int> dicTotalItemPerLevel = new Dictionary<string, int>();
            foreach (var item in folderFlat)
            {
                string parentId = item.ParentId == null ? "null" : item.ParentId;
                string nextId = item.NextId == null ? "null" : item.NextId;
                dicSupportFindData.Add(parentId + nextId, item);
                if (dicTotalItemPerLevel.ContainsKey(parentId))
                {
                    dicTotalItemPerLevel[parentId] = dicTotalItemPerLevel[parentId] + 1;
                }
                else
                {
                    dicTotalItemPerLevel.Add(parentId, 1);
                }
            }
            var treeRoot = folderFlat.GenerateTree2(dicSupportFindData, dicTotalItemPerLevel, null).Reverse();
            return treeRoot;
        }
    }
}
