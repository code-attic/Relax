using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;
using Symbiote.Core;
using Symbiote.Core.Extensions;
using Symbiote.Daemon;
using Symbiote.Log4Net;

namespace Relax.Overflow
{
    class Program
    {
        static void Main(string[] args)
        {
            Assimilate
                .Core()
                .Daemon(x => x
                    .Arguments(args)
                    .Name("SOBulkLoader")
                    .DisplayName("Stack Overflow Bulk Loading Service")
                    .Description("Does what it says"))
                .Relax(x => x.UseDefaults().TimeOut(1000000))
                .AddConsoleLogger<LoadingService>(x => x.Info().MessageLayout(m => m.Date().Message().Newline()))
                .RunDaemon();
        }
    }

    public class LoadingService
        : IDaemon
    {
        protected IDocumentRepository repository { get; set; }
        protected XmlSerializer postSerializer { get; set; }

        public void Start()
        {
            "Loading service starting"
                .ToInfo<LoadingService>();

            Action<IList<XElement>> saveAction = SaveChunk;
            var loader = new BulkPostLoader(@"e:\stackoverflow\062010 so\posts.xml");
            var batches = loader.BufferWithCount(5000);
            var results = batches.Select(x => saveAction.BeginInvoke(x, null, null));
            
            results
                .BufferWithCount(5)
                .Subscribe(x => x.ForEach(y => y.AsyncWaitHandle.WaitOne()));

            loader.Start();
        }

        protected void SaveChunk(IList<XElement> x)
        {
            var list = x.Select(ProcessPost).ToList();
            repository.SaveAll(list);
            "Posts {0} to {1} chunked and saved"
                .ToInfo<LoadingService>(list.First().Id, list.Last().Id);
        }

        public Post ProcessPost(XElement element)
        {
            var content = element.ToString();
            return postSerializer.Deserialize(new StringReader(content)) as Post;
        }

        public void Stop()
        {
            "Loading service stopping"
                .ToInfo<LoadingService>();
        }

        public LoadingService(IDocumentRepository repository)
        {
            this.repository = repository;
            this.postSerializer = new XmlSerializer(typeof (Post));
        }
    }

    public abstract class BaseObservable<TNotification> 
        : IObservable<TNotification>, IDisposable
    {
        protected ConcurrentBag<IObserver<TNotification>> observers { get; set; }

        public virtual void Notify(TNotification notification)
        {
            observers.ForEach(x => x.OnNext(notification));
        }

        public virtual void SendCompletion()
        {
            observers.ForEach(x => x.OnCompleted());
        }

        public virtual IDisposable Subscribe(IObserver<TNotification> observer)
        {
            var disposable = this as IDisposable;
            observers.Add(observer);
            return disposable;
        }

        protected BaseObservable()
        {
            this.observers = new ConcurrentBag<IObserver<TNotification>>();
        }

        public void Dispose()
        {
            while (observers.Count > 0)
            {
                IObserver<TNotification> o;
                observers.TryTake(out o);
            }
        }
    }

    public class BulkPostLoader
        : BaseObservable<XElement>
    {
        protected string xmlExportPath { get; set; }

        public void Start()
        {
            var count = 0;
            using(var stream = new FileStream(xmlExportPath, FileMode.Open, FileAccess.Read, FileShare.None, 2048, true))
            {
                using(var reader = XmlReader.Create(stream))
                {
                    reader.MoveToContent();

                    while(reader.Read())
                    {
                        if(reader.NodeType == XmlNodeType.Element && reader.Name == "row")
                        {
                            count++;
                            var element = XElement.ReadFrom(reader) as XElement;
                            Notify(element);
                        }
                    }
                }
            }
            SendCompletion();
        }

        public BulkPostLoader(string xmlExportPath)
        {
            this.xmlExportPath = xmlExportPath;
        }
    }
}
