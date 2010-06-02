namespace Relax.Impl.Serialization
{
    public class DocumentConventions
    {
        public string IdPropertyName { get; set; }
        public string RevisionPropertyName { get; set; }

        public DocumentConventions()
        {
            IdPropertyName = "Id";
            RevisionPropertyName = "Rev";
        }
    }
}
