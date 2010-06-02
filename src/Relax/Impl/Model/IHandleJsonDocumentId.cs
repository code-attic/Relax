namespace Relax.Impl
{
    public interface IHandleJsonDocumentId
    {
        void UpdateKeyFromJson(string jsonKey);
        string GetIdAsJson();
    }
}