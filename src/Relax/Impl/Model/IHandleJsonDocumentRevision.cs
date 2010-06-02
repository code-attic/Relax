namespace Relax.Impl
{
    public interface IHandleJsonDocumentRevision
    {
        void UpdateRevFromJson(string jsonRev);
        string GetRevAsJson();
    }
}