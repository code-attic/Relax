namespace Relax.Impl.Model
{
    public interface IHaveDocumentId
    {
        string GetDocumentIdAsJson();
        object GetDocumentId();
    }
}