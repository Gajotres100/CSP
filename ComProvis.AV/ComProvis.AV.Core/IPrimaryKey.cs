namespace ComProvis.AV.Core
{
    public interface IPrimaryKey
    {
        int Id { get; set; }
        string ExternalId { get; set; }
    }
}
