using System.Threading.Tasks;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public class Jd : Base
    {

        internal Jd(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Keep an eye on your JDownloader Client ;)
        /// </summary>
        public async Task DoSomethingCoolAsync()
        {
            await ApiHandler.CallActionAsync<object>(Device, "/jd/doSomethingCool",
                null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Keep an eye on your JDownloader Client ;)
        /// </summary>
        public void DoSomethingCool()
        {
            DoSomethingCoolAsync().Wait();
        }

        /// <summary>
        /// Gets the core revision of the jdownloader client.
        /// </summary>
        /// <returns>Returns the core revision of the jdownloader client.</returns>
        public async Task<int> GetCoreRevisionAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<int>>(Device, "/jd/getCoreRevision",
                null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response != null) return response.Data;
            return -1;
        }

        /// <summary>
        /// Gets the core revision of the jdownloader client.
        /// </summary>
        /// <returns>Returns the core revision of the jdownloader client.</returns>
        public int GetCoreRevision()
        {
            return GetCoreRevisionAsync().Result;
        }

        /// <summary>
        /// Refreshes the plugins.
        /// </summary>
        /// <returns>True if successfull.</returns>
        public async Task<bool> RefreshPluginsAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/jd/refreshPlugins",
                null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Refreshes the plugins.
        /// </summary>
        /// <returns>True if successfull.</returns>
        public bool RefreshPlugins()
        {
            return RefreshPluginsAsync().Result;
        }

        /// <summary>
        /// Creates the sum of two numbers.
        /// </summary>
        /// <param name="a">First number.</param>
        /// <param name="b">Second number.</param>
        /// <returns>Returns the sum of two numbers.</returns>
        public async Task<int> SumAsync(int a, int b)
        {
            var param = new[] { a, b };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<int>>(Device, "/jd/sum",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response != null) return response.Data;
            return -1;
        }

        /// <summary>
        /// Creates the sum of two numbers.
        /// </summary>
        /// <param name="a">First number.</param>
        /// <param name="b">Second number.</param>
        /// <returns>Returns the sum of two numbers.</returns>
        public int Sum(int a, int b)
        {
            return SumAsync(a, b).Result;
        }

        /// <summary>
        /// Gets the current uptime of the JDownloader client.
        /// </summary>
        /// <returns>The current uptime of the JDownloader client as long.</returns>
        public async Task<long> UptimeAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<long>>(Device, "/jd/uptime",
                null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response != null) return response.Data;
            return -1;
        }

        /// <summary>
        /// Gets the current uptime of the JDownloader client.
        /// </summary>
        /// <returns>The current uptime of the JDownloader client as long.</returns>
        public long Uptime()
        {
            return UptimeAsync().Result;
        }

        /// <summary>
        /// Gets the version of the JDownloader client.
        /// </summary>
        /// <returns>The current version of the JDownloader client.</returns>
        public async Task<long> VersionAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<long>>(Device, "/jd/version",
                null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response != null) return response.Data;
            return -1;
        }

        /// <summary>
        /// Gets the version of the JDownloader client.
        /// </summary>
        /// <returns>The current version of the JDownloader client.</returns>
        public long Version()
        {
            return VersionAsync().Result;
        }
    }
}
