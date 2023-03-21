using System.ComponentModel.DataAnnotations;

namespace DapperExample.Entity
{
    public class Tree
    {
        [Key]
        public Guid Id { get; set; }
        public string?  Name { get; set; }
        public int Level{ get; set; }
        public Guid ParentId{ get; set; }
        public Guid NextId{ get; set; }
    }
}
