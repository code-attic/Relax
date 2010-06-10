using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Symbiote.Core.Extensions;
using Symbiote.Lucene;

namespace Relax.Lucene
{
    public class JsonVisitor
        : IVisit<string>
    {
        protected Dictionary<Type, Action<JToken, string>> typeProcessor { get; set; }
        protected ConcurrentBag<IObserver<Tuple<string, string>>> observers { get; set; }

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

        public void Accept(string root)
        {
            var obj = JObject.Parse(root);
            Process(obj, "");
            observers
                .ForEach(x => x.OnCompleted());
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
                .ForEach(x => Process(x, prefix));
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
        }
    }
}
