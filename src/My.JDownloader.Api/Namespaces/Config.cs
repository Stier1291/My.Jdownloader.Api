using System.Collections.Generic;
using System.Threading.Tasks;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Config.Request;
using My.JDownloader.Api.Models.Config.Response;
using My.JDownloader.Api.Models.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public class Config : Base
    {
        internal Config(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Gets the value of the given interface
        /// </summary>
        /// <param name="interfaceName">Name of the interface.</param>
        /// <param name="storage">The storage name.</param>
        /// <param name="key">The key name.</param>
        /// <returns>The value of the given interface.</returns>
        public async Task<object> GetAsync(string interfaceName, string storage, string key)
        {
            var param = new[] { interfaceName, storage, key };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<object>>(Device, "/config/get",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets the value of the given interface
        /// </summary>
        /// <param name="interfaceName">Name of the interface.</param>
        /// <param name="storage">The storage name.</param>
        /// <param name="key">The key name.</param>
        /// <returns>The value of the given interface.</returns>
        public object Get(string interfaceName, string storage, string key)
        {
            return GetAsync(interfaceName, storage, key).Result;
        }

        /// <summary>
        /// Gets the default value of the given interface
        /// </summary>
        /// <param name="interfaceName">Name of the interface.</param>
        /// <param name="storage">The storage name.</param>
        /// <param name="key">The key name.</param>
        /// <returns>The default value of the given interface.</returns>
        public async Task<object> GetDefaultAsync(string interfaceName, string storage, string key)
        {
            var param = new[] { interfaceName, storage, key };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<object>>(Device, "/config/get",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets the default value of the given interface
        /// </summary>
        /// <param name="interfaceName">Name of the interface.</param>
        /// <param name="storage">The storage name.</param>
        /// <param name="key">The key name.</param>
        /// <returns>The default value of the given interface.</returns>
        public object GetDefault(string interfaceName, string storage, string key)
        {
            return GetDefaultAsync(interfaceName, storage, key).Result;
        }

        /// <summary>
        /// Lists all available config entries.
        /// </summary>
        /// <returns>An enumerable with all available config entries.</returns>
        public async Task<IEnumerable<AdvancedConfigApiEntryResponse>> ListAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<AdvancedConfigApiEntryResponse>>>(Device, "/config/list",
                null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Lists all available config entries.
        /// </summary>
        /// <returns>An enumerable with all available config entries.</returns>
        public IEnumerable<AdvancedConfigApiEntryResponse> List()
        {
            return ListAsync().Result;
        }

        /// <summary>
        /// Lists all available config entries based on the pattern.
        /// </summary>
        /// <param name="pattern">A pattern as a regex string.</param>
        /// <param name="returnDescription">True if you want the description.</param>
        /// <param name="returnValues">True if you want the values.</param>
        /// <param name="returnDefaultValues">True if you want the default values.</param>
        /// <param name="returnEnumInfo">True if you want the enum infos</param>
        /// <returns>An enumerable with all available config entries based on the regex pattern.</returns>
        public async Task<IEnumerable<AdvancedConfigApiEntryResponse>> ListAsync(string pattern, bool returnDescription, 
            bool returnValues, bool returnDefaultValues, bool returnEnumInfo)
        {
            var param = new object[] { pattern, returnDescription, returnValues, returnDefaultValues, returnEnumInfo };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<AdvancedConfigApiEntryResponse>>>(Device, "/config/list",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Lists all available config entries based on the pattern.
        /// </summary>
        /// <param name="pattern">A pattern as a regex string.</param>
        /// <param name="returnDescription">True if you want the description.</param>
        /// <param name="returnValues">True if you want the values.</param>
        /// <param name="returnDefaultValues">True if you want the default values.</param>
        /// <param name="returnEnumInfo">True if you want the enum infos</param>
        /// <returns>An enumerable with all available config entries based on the regex pattern.</returns>
        public IEnumerable<AdvancedConfigApiEntryResponse> List(string pattern, bool returnDescription,
            bool returnValues, bool returnDefaultValues, bool returnEnumInfo)
        {
            return ListAsync(pattern, returnDescription, returnValues, returnDefaultValues, returnEnumInfo).Result;
        }

        /// <summary>
        /// Lists all possible enum values.
        /// </summary>
        /// <param name="type">The type of the enum.</param>
        /// <returns>An enumerable with all possible enum values.</returns>
        public async Task<IEnumerable<EnumOptionsResponse>> ListEnumAsync(string type)
        {
            var param = new[] { type };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<EnumOptionsResponse>>>(Device, "/config/listEnum",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Lists all possible enum values.
        /// </summary>
        /// <param name="type">The type of the enum.</param>
        /// <returns>An enumerable with all possible enum values.</returns>
        public IEnumerable<EnumOptionsResponse> ListEnum(string type)
        {
            return ListEnumAsync(type).Result;
        }

        /// <summary>
        /// Lists all available config entries based on the queryRequest object.
        /// </summary>
        /// <param name="queryRequest">The queryRequest object to filter the return.</param>
        /// <returns>An enumerable with all available config entries based on the queryRequest object.</returns>
        public async Task<IEnumerable<AdvancedConfigApiEntryResponse>> QueryAsync(AdvancedConfigQueryRequest queryRequest)
        {
            var param = new[] { queryRequest };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<AdvancedConfigApiEntryResponse>>>(Device, "/config/queryRequest",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Lists all available config entries based on the queryRequest object.
        /// </summary>
        /// <param name="queryRequest">The queryRequest object to filter the return.</param>
        /// <returns>An enumerable with all available config entries based on the queryRequest object.</returns>
        public IEnumerable<AdvancedConfigApiEntryResponse> Query(AdvancedConfigQueryRequest queryRequest)
        {
            return QueryAsync(queryRequest).Result;
        }

        /// <summary>
        /// Resets the interface by the key to its default value.
        /// </summary>
        /// <param name="interfaceName">The name of the interface.</param>
        /// <param name="storage">The storage of the interface.</param>
        /// <param name="key">The key.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> ResetAsync(string interfaceName, string storage, string key)
        {
            var param = new[] { interfaceName, storage, key };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/config/reset",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Resets the interface by the key to its default value.
        /// </summary>
        /// <param name="interfaceName">The name of the interface.</param>
        /// <param name="storage">The storage of the interface.</param>
        /// <param name="key">The key.</param>
        /// <returns>True if successful.</returns>
        public bool Reset(string interfaceName, string storage, string key)
        {
            return ResetAsync(interfaceName, storage, key).Result;
        }

        /// <summary>
        /// Sets the value of the interface by key.
        /// </summary>
        /// <param name="interfaceName">The name of the interface.</param>
        /// <param name="storage">The storage of the interface.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The new value.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SetAsync(string interfaceName, string storage, string key, object value)
        {
            var param = new [] { interfaceName, storage, key, value };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/config/set",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Sets the value of the interface by key.
        /// </summary>
        /// <param name="interfaceName">The name of the interface.</param>
        /// <param name="storage">The storage of the interface.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The new value.</param>
        /// <returns>True if successful.</returns>
        public bool Set(string interfaceName, string storage, string key, object value)
        {
            return SetAsync(interfaceName, storage, key, value).Result;
        }
    }
}