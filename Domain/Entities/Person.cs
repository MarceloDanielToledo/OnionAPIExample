using Domain.Common;

namespace Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Count { get; set; }
    }
}
