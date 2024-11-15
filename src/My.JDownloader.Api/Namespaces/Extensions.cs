using System.Collections.Generic;
using System.Threading.Tasks;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.Extensions.Request;
using My.JDownloader.Api.Models.Extensions.Response;
using Newtonsoft.Json;

namespace My.JDownloader.Api.Namespaces
{
    public class Extensions : Base
    {

        internal Extensions(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Installs an extension to the client.
        /// </summary>
        /// <param name="extensionId">The id of the extension you want to install</param>
        /// <returns>True if successfull</returns>
        public async Task<bool> InstallAsync(string extensionId)
        {
            var param = new[] { extensionId };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/extensions/install",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Installs an extension to the client.
        /// </summary>
        /// <param name="extensionId">The id of the extension you want to install</param>
        /// <returns>True if successfull</returns>
        public bool Install(string extensionId)
        {
            return InstallAsync(extensionId).Result;
        }

        /// <summary>
        /// Checks if the extension is enabled.
        /// </summary>
        /// <param name="className">Name/id of the extension.</param>
        /// <returns>True if enabled.</returns>
        public async Task<bool> IsEnabledAsync(string className)
        {
            var param = new[] { className };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/extensions/isEnabled",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Checks if the extension is enabled.
        /// </summary>
        /// <param name="className">Name/id of the extension.</param>
        /// <returns>True if enabled.</returns>
        public bool IsEnabled(string className)
        {
            return IsEnabledAsync(className).Result;
        }

        /// <summary>
        /// Checks if the extension is installed.
        /// </summary>
        /// <param name="extensionId">The id of the extension you want to install.</param>
        /// <returns>True if successfull</returns>
        public async Task<bool> IsInstalledAsync(string extensionId)
        {
            var param = new[] { extensionId };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/extensions/isInstalled",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Checks if the extension is installed.
        /// </summary>
        /// <param name="extensionId">The id of the extension you want to install.</param>
        /// <returns>True if successfull</returns>
        public bool IsInstalled(string extensionId)
        {
            return IsInstalledAsync(extensionId).Result;
        }

        /// <summary>
        /// Gets all extensions that are available.
        /// </summary>
        /// <param name="requestObject">The request object which contains informations about which properties are returned.</param>
        /// <returns>An enumerable of all extensions that are available.</returns>
        public async Task<IEnumerable<ExtensionResponse>> ListAsync(ExtensionRequest requestObject)
        {
            string json = JsonConvert.SerializeObject(requestObject);
            var param = new[] { json };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<ExtensionResponse>>>(Device, "/extensions/list",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets all extensions that are available.
        /// </summary>
        /// <param name="requestObject">The request object which contains informations about which properties are returned.</param>
        /// <returns>An enumerable of all extensions that are available.</returns>
        public IEnumerable<ExtensionResponse> List(ExtensionRequest requestObject)
        {
            return ListAsync(requestObject).Result;
        }

        /// <summary>
        /// Enables or disables an extension
        /// </summary>
        /// <param name="className">Name/id of the extension.</param>
        /// <param name="enabled">If true the extension gets enabled else it disables it.</param>
        /// <returns>True if successful</returns>
        public async Task<bool> SetEnabledAsync(string className, bool enabled)
        {
            var param = new[] { className, enabled.ToString() };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<object>>(Device, "/extensions/setEnabled",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data != null;
        }

        /// <summary>
        /// Enables or disables an extension
        /// </summary>
        /// <param name="className">Name/id of the extension.</param>
        /// <param name="enabled">If true the extension gets enabled else it disables it.</param>
        /// <returns>True if successful</returns>
        public bool SetEnabled(string className, bool enabled)
        {
            return SetEnabledAsync(className, enabled).Result;
        }
    }
}
