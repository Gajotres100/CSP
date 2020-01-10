using ComProvis.AV.Core;

namespace ComProvis.AV
{
    public partial class Product : IPrimaryKey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExternalId { get; set; }
    }
}
