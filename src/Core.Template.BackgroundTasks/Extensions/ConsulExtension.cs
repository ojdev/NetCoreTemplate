using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Consul
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConsulExtension
    {
        /// <summary>
        /// 
        /// </summary>
        public static IConsulClient Consul { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="consul"></param>
        /// <param name="serverName"></param>
        public static async Task<string> GetService(this IConsulClient consul, string serverName)
        {
            var response = await consul.Agent.Services();
            var _services = response.Response.Values.Where(t => t.Service.Equals(serverName)).ToList();
            var r = new Random();
            var index = r.Next(_services.Count);
            var service = _services.ElementAt(index);
            return $"{service.Address}:{service.Port}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="kvEndpoint"></param>
        /// <param name="key"></param>
        /// <param name="city"></param>
        /// <param name="company"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static async Task<TResult> GetAsync<TResult>(this IKVEndpoint kvEndpoint, string key, string city = null, string company = null, CancellationToken ct = default(CancellationToken))
        {
            QueryResult<KVPair> queryResult = await kvEndpoint.Get(RequestKey(key, city, company), ct);
            return queryResult.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<TResult>(Encoding.UTF8.GetString(queryResult.Response.Value)) : default(TResult);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="kvEndpoint"></param>
        /// <param name="key"></param>
        /// <param name="city"></param>
        /// <param name="company"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static async Task<TResult> GetOrDefaultAsync<TResult>(this IKVEndpoint kvEndpoint, string key, string city = null, string company = null, CancellationToken ct = default(CancellationToken))
        {
            QueryResult<KVPair> queryResult = await kvEndpoint.Get(RequestKey(key, city, company), ct);
            if (queryResult.StatusCode != HttpStatusCode.OK)
            {
                queryResult = await kvEndpoint.Get(RequestKey(key, !string.IsNullOrWhiteSpace(city) ? "default" : null, !string.IsNullOrWhiteSpace(company) ? "default" : null), ct);
            }
            return queryResult.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<TResult>(Encoding.UTF8.GetString(queryResult.Response.Value)) : default(TResult);
        }
        private static string RequestKey(string key, string city, string company)
        {
            string requestKey = key;
            if (!string.IsNullOrWhiteSpace(city))
            {
                requestKey = $"{city}/{key}";
            }
            if (!string.IsNullOrWhiteSpace(city) && !string.IsNullOrWhiteSpace(company))
            {
                requestKey = $"{city}/{company}/{key}";
            }
            return requestKey;
        }
    }
}
