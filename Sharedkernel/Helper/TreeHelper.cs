using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharedkernel.Helper
{
    public static class TreeHelper
    {
        public static IEnumerable<TreeItem<Folder>> GenerateTree2(this IEnumerable<Folder> collection, Dictionary<string, Folder> dicSupportFindData, string root_id)
        {
            List<TreeItem<Folder>> treeItems = new List<TreeItem<Folder>>();
            var listDataSameLevel = collection.AsParallel().Where(c => Equals(c.ParentId, root_id));
            int totalItemSameLevel = listDataSameLevel.Count();
            StringBuilder keyStarter = new StringBuilder((root_id == null ? "null" : root_id) + "null");
            var starter = dicSupportFindData.ContainsKey(keyStarter.ToString()) ? dicSupportFindData[keyStarter.ToString()] : null;

            if(starter != null)
            {
                treeItems.Add(new TreeItem<Folder>
                {
                    Item = starter,
                    Children = collection.GenerateTree2(dicSupportFindData, starter.Id).Reverse()
                });
                var count = totalItemSameLevel;
                StringBuilder keyCurrent = new StringBuilder();

                while(count > 1)
                {
                    keyCurrent = new StringBuilder((starter.ParentId == null ? "null" : starter.ParentId) + (starter.Id == null ? "null" : starter.Id));
                    var currentItem = dicSupportFindData.ContainsKey(keyCurrent.ToString()) ? dicSupportFindData[keyCurrent.ToString()] : null;

                    if(currentItem != null)
                    {
                        treeItems.Add(new TreeItem<Folder>
                        {
                            Item = currentItem,
                            Children = collection.GenerateTree2(dicSupportFindData, currentItem.Id).Reverse()
                        });
                        starter = currentItem;
                    }
                    count--;
                }
            }
            return treeItems;
        }
    }
    public class TreeItem<T>
    {
        public T Item { get; set; }
        public IEnumerable<TreeItem<T>> Children { get; set; }
    }
}
