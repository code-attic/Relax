using System;
using System.Collections.Generic;
using Relax.Impl.Model;

namespace Relax.Impl.Cache
{
    public interface ICouchCacheProvider
    {
        void AddCrossReference(string key, string cacheKey);

        void InvalidateItem(string affectedKey);

        void Delete<TModel>(object key, Action<object> delete);

        void Delete<TModel>(object key, string rev, Action<object, string> delete);

        TModel Get<TModel>(object key, string rev, Func<object, string, TModel> retrieve);

        TModel Get<TModel>(object key, Func<object, TModel> retrieve);

        IList<TModel> GetAll<TModel>(Func<IList<TModel>> retrieve);

        IList<TModel> GetAllPaged<TModel>(int pageSize, int pageNumber, Func<int, int, IList<TModel>> retrieve);

        IList<TModel> GetAllInRange<TModel>(object startingWith, object endingWith, Func<object,object,IList<TModel>> retrieve);

        void Save<TModel>(TModel model, Action<TModel> save);

        void SaveAll<TModel>(IEnumerable<TModel> list, Action<IEnumerable<TModel>> save);
    }
}