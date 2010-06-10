namespace Relax.Lucene
{
    public class RelaxLuceneConfigurator
    {
        protected IRelaxLuceneConfiguration configuration { get; set; }

        public RelaxLuceneConfigurator IndexDatabase(string database)
        {
            configuration.Databases.Add(database);
            return this;
        }

        public RelaxLuceneConfigurator UseDefaults()
        {
            return this;
        }

        public IRelaxLuceneConfiguration GetConfiguration()
        {
            return configuration;
        }

        public RelaxLuceneConfigurator()
        {
            configuration = new RelaxLuceneConfiguration();
        }
    }
}