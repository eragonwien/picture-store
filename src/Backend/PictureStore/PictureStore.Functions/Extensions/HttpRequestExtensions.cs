using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PictureStore.Functions.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task<TModel> ParseBodyAsync<TModel>(this HttpRequest request)
            where TModel : class
        {
            if (request is null) throw new System.ArgumentNullException(nameof(request));

            try
            {
                string requestBody = string.Empty;
                using var streamReader = new StreamReader(request.Body);
                requestBody = await streamReader.ReadToEndAsync();

                return (TModel)JsonConvert.DeserializeObject(requestBody, typeof(TModel));
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
