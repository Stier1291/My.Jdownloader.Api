using System.Threading.Tasks;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public class Update : Base
    {

        internal Update(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Checks if the client has an update available.
        /// </summary>
        /// <returns>True if an update is available.</returns>
        public async Task<bool> IsUpdateAvailableAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/update/isUpdateAvailable",
                null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Checks if the client has an update available.
        /// </summary>
        /// <returns>True if an update is available.</returns>
        public bool IsUpdateAvailable()
        {
            return IsUpdateAvailableAsync().Result;
        }

        /// <summary>
        /// Restarts the client and starts the update.
        /// </summary>
        public async Task RestartAndUpdateAsync()
        {
            await ApiHandler.CallActionAsync<object>(Device, "/update/restartAndUpdate",
                 null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Restarts the client and starts the update.
        /// </summary>
        public void RestartAndUpdate()
        {
            RestartAndUpdateAsync().Wait();
        }

        /// <summary>
        /// Start the update check on the client.
        /// </summary>
        public async Task RunUpdateCheckAsync()
        {
            await ApiHandler.CallActionAsync<object>(Device, "/update/runUpdateCheck",
                null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Start the update check on the client.
        /// </summary>
        public void RunUpdateCheck()
        {
            RunUpdateCheckAsync().Wait();
        }
    }
}
