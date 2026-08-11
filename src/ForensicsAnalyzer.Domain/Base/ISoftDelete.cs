namespace ForensicsAnalyzer.Domain.Base
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}