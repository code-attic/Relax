using System;
using System.Collections.Generic;
using Relax.Impl.Model;

namespace Relax.Impl.Cache
{
    public interface ICouchCacheProvider
    {
        void AddCrossReference(string key, string cacheKey);

        void InvalidateItem<TModel>(string affectedKey);

        void Delete<TModel>(object key, Action<object> delete);

        void Delete<TModel>(object key, object rev, Action<object, object> delete);

        void DeleteAll<TModel>();

        TModel Get<TModel>(object key, object rev, Func<object, object, TModel> retrieve);

        TModel Get<TModel>(object key, Func<object, TModel> retrieve);

        IList<TModel> GetAll<TModel>(Func<IList<TModel>> retrieve);

        IList<TModel> GetAll<TModel>(int pageNumber, int pageSize, Func<int, int, IList<TModel>> retrieve);

        IList<TModel> GetAll<TModel>(object startingWith, object endingWith, Func<object,object,IList<TModel>> retrieve);

        void Save<TModel>(TModel model, Action<TModel> save);

        void Save<TModel>(IEnumerable<TModel> list, Action<IEnumerable<TModel>> save);
    }
}