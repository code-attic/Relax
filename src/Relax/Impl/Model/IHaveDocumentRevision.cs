namespace Relax.Impl.Model
{
    public interface IHaveDocumentRevision
    {
        string DocumentRevision { get; set; }
        void UpdateRevFromJson(string jsonRev);
    }
}