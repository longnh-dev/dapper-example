using Sharedkernel.Helper;
using System.Text;


namespace DapperExample.Sharedkernel
{
    public static class TreeHelper
    {
        public static IEnumerable<TreeItem<T>> GenerateTree2<T>(
               this IEnumerable<T> collection,
               Dictionary<string, T> dicSupportFindData,
               Dictionary<string, int> dicTotalItemPerLevel,
               string root_id) where T : IFolder
        {
            List<TreeItem<T>> treeItems = new List<TreeItem<T>>();
            StringBuilder keyStarter = new StringBuilder((root_id == null ? "null" : root_id) + "null");
            IFolder starter = dicSupportFindData.ContainsKey(keyStarter.ToString()) ? dicSupportFindData[keyStarter.ToString()] : null;
            if (starter != null)
            {
                treeItems.Add(new TreeItem<T>
                {
                    Item = (T)starter,
                    Children = collection.GenerateTree2(dicSupportFindData, dicTotalItemPerLevel, starter.GetId()).Reverse()
                });

                var count = dicTotalItemPerLevel[root_id == null ? "null" : root_id]; // bug

                StringBuilder keyCurrent = new StringBuilder();
                while (count > 1)
                {
                    keyCurrent = new StringBuilder((starter.GetParentId() == null ? "null" : starter.GetParentId()) + (starter.GetId() == null ? "null" : starter.GetId()));
                    IFolder currentItem = dicSupportFindData.ContainsKey(keyCurrent.ToString()) ? dicSupportFindData[keyCurrent.ToString()] : null;

                    if (currentItem != null)
                    {
                        treeItems.Add(new TreeItem<T>
                        {
                            Item = (T)currentItem,
                            Children = collection.GenerateTree2(dicSupportFindData, dicTotalItemPerLevel, currentItem.GetId()).Reverse()
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