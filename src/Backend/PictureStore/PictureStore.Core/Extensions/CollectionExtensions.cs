using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictureStore.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static async Task<ICollection<T>> SelectAsync<T>(this ICollection<T> collection, Func<T, Task<T>> selectAsyncFunc)
        {
            var results = new List<T>();

            if (collection is null ||
                collection.Count == 0 ||
                selectAsyncFunc is null)
                return results;

            foreach (var item in collection)
                results.Add(await selectAsyncFunc.Invoke(item));

            return results;
        }
    }
}
