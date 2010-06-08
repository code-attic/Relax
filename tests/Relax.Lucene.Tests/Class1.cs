using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Machine.Specifications;
using Newtonsoft.Json.Linq;
using Relax.Impl.Commands;
using Relax.Impl.Model;
using StructureMap;
using StructureMap.Pipeline;
using Symbiote.Core.Extensions;

namespace Relax.Lucene.Tests
{
    public class Person : CouchDocument
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int Age { get; set; }
        public virtual List<Car> Cars { get; set; }
    }

    public class Car : CouchDocument
    {
        public virtual string Make { get; set; }
        public virtual string Model { get; set; }
        public virtual int Year { get; set; }
    }

    public class DomainHelper
    {
        public static Person Create_Single_Person_With_One_Car()
        {
            return new Person()
                       {
                           FirstName = "Alex",
                           LastName = "Robson",
                           Age = 31,
                           Cars = new List<Car>()
                                      {
                                          new Car() {Make="Chevy",Model="Equinox",Year=2007}
                                      }
                       };
        }

        public static List<Person> Create_Family_With_Two_Cars()
        {
            return new List<Person>()
                       {
                           new Person()
                               {
                                   FirstName = "Alex",
                                   LastName = "Robson",
                                   Age = 31,
                                   Cars = new List<Car>()
                                      {
                                          new Car() {Make="Chevy",Model="Equinox",Year=2007}
                                      }
                               },
                           new Person()
                               {
                                   FirstName = "Rebekah",
                                   LastName = "Robson",
                                   Age = 28,
                                   Cars = new List<Car>()
                                      {
                                          new Car() {Make="Honda",Model="Civic",Year=2008}
                                      }
                               },
                       };
        }
    }

    public class Lucenerator
        : IObserver<Tuple<string,string>>
    {
        protected IndexWriter indexWriter { get; set; }
        protected Document document { get; set; }

        public void OnNext(Tuple<string, string> value)
        {
            document.Add(new Field(value.Item1, value.Item2, Field.Store.COMPRESS, Field.Index.TOKENIZED));
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnCompleted()
        {
            indexWriter.AddDocument(document);
            indexWriter.Optimize();
        }

        public Lucenerator(IndexWriter writer)
        {
            indexWriter = writer;
            document = new Document();
        }
    }

    public class CouchIndexingService        
    {
        protected IRelaxLuceneConfiguration configuration { get; set; }
        protected IndexWriter indexWriter { get; set; }
        protected Directory directory { get; set; }
        protected Analyzer analyzer { get; set; }
        protected ChangeStreamCommand command { get; set; }

        public void Start()
        {
            configuration
                .Databases
                .ForEach(x => /* */);
        }

        public void Stop()
        {
            
        }

        public CouchIndexingService(
            IRelaxLuceneConfiguration configuration,
            CouchCommandFactory commandFactory, 
            DirectoryFactory directoryFactory, 
            AnalyzerFactory analyzerFactory)
        {
            this.configuration = configuration;
            this.command = commandFactory.GetStreamCommand();
            this.directory = directoryFactory.GetDirectory();
            this.analyzer = analyzerFactory.GetAnalyzer();
            this.indexWriter = new IndexWriter(directory, analyzer, true);
        }
    }

    public interface IRelaxLuceneConfiguration
    {
        Type DirectoryType { get; set; }
        Type AnalyzerType { get; set; }
        HashSet<string> Databases { get; set; }
    }

    public class RelaxLuceneConfiguration : IRelaxLuceneConfiguration
    {
        public Type DirectoryType { get; set; }
        public Type AnalyzerType { get; set; }
        public HashSet<string> Databases { get; set; }

        public RelaxLuceneConfiguration()
        {
            DirectoryType = typeof (RAMDirectory);
            AnalyzerType = typeof (StandardAnalyzer);
            Databases = new HashSet<string>();
        }
    }

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

        public RelaxLuceneConfigurator()
        {
            configuration = new RelaxLuceneConfiguration();
        }
    }

    public class DirectoryFactory
    {
        protected IRelaxLuceneConfiguration configuration { get; set; }

        public Directory GetDirectory()
        {
            return ObjectFactory.GetInstance(configuration.DirectoryType) as Directory;
        }

        public DirectoryFactory(IRelaxLuceneConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }

    public class AnalyzerFactory
    {
        protected IRelaxLuceneConfiguration configuration { get; set; }

        public Analyzer GetAnalyzer()
        {
            return ObjectFactory.GetInstance(configuration.AnalyzerType) as Analyzer;
        }

        public AnalyzerFactory(IRelaxLuceneConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }

    public class JsonVisitor
        : IObservable<Tuple<string,string>>
    {
        protected Dictionary<Type, Action<JToken, string>> typeProcessor { get; set; }
        protected ConcurrentBag<IObserver<Tuple<string, string>>> observers { get; set;}
        
        public JsonVisitor()
        {
            typeProcessor = new Dictionary<Type, Action<JToken, string>> {
                    {typeof(JObject),ProcessObject},
                    {typeof(JProperty),ProcessProperty},
                    {typeof(JArray),ProcessArray},
                    {typeof(JValue),ProcessValue}
                };

            observers = new ConcurrentBag<IObserver<Tuple<string, string>>>();
        }

        protected void Process(JToken jObject, string prefix)
        {
            typeProcessor[jObject.GetType()](jObject, prefix);
        }

        public void Traverse(string json)
        {
            var obj = JObject.Parse(json);
            Process(obj, "");
        }

        protected void ProcessValue(JToken token, string prefix)
        {
            var jValue = token as JValue;
            observers
                .ForEach(x => x.OnNext(Tuple.Create(prefix, jValue.Value.ToString())));
        }

        public IDisposable Subscribe(IObserver<Tuple<string, string>> observer)
        {
            var disposable = observer as IDisposable;
            observers.Add(observer);
            return disposable;
        }

        protected void ProcessArray(JToken token, string prefix)
        {
            var array = token as JArray;
            if (array.Children().Count() == 0)
                return;

            array
                .Children()
                .ForEach(x => Process(token, prefix));
        }

        protected void ProcessProperty(JToken token, string prefix)
        {
            var property = token as JProperty;
            var name = string.IsNullOrEmpty(prefix) ? property.Name : "{0}.{1}".AsFormat(prefix, property.Name);
            Process(property.Value, name);
        }

        protected void ProcessObject(JToken token, string prefix)
        {
            var jObject = token as JObject;

            if (jObject.HasValues)
                jObject
                    .Children()
                    .ForEach(x => Process(x, prefix));

            observers
                .ForEach(x => x.OnCompleted());
        }
    }


    public abstract class with_lucene
    {
        protected static RAMDirectory directory;
        protected static Analyzer analyzer;
        protected static IndexWriter indexWriter;
        

        private Establish context = () =>
                                        {
                                            
                                        };
    }

}
