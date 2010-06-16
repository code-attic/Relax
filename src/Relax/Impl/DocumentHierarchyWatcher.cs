using System;
using System.Collections.Generic;
using Relax.Impl.Model;

namespace Relax.Impl
{
    public class DocumentHierarchyWatcher : IObserver<Tuple<object, string, object>>
    {
        public List<object> Documents { get; set; }
        public bool Done { get; set; }

        public void OnNext(Tuple<object, string, object> value)
        {
            var parent = value.Item1 as BaseDocument;
            var child = value.Item3 as BaseDocument;
            var property = value.Item2;
            
            if(parent != null)
            {
                var childIdArray = new object[] {};
                var childIds = new List<object>() { child.GetDocumentIdAsJson() };
                if(parent.RelatedDocumentIds.TryGetValue(property, out childIdArray))
                {
                    childIds.AddRange(childIdArray);
                }
                parent.RelatedDocumentIds[property] = childIds.ToArray();
                child.ParentId = parent.GetDocumentIdAsJson();
            }
            Documents.Add(child);
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnCompleted()
        {
            Done = true;
        }

        public DocumentHierarchyWatcher()
        {
            Documents = new List<object>();
        }
    }
}