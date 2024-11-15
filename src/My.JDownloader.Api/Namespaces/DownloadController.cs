using System.Threading.Tasks;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public class DownloadController : Base
    {

        internal DownloadController(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Forces JDownloader to start downloading the given links/packages
        /// </summary>
        /// <param name="linkIds">The ids of the links you want to force download.</param>
        /// <param name="packageIds">The ids of the packages you want to force download.</param>
        /// <returns>True if successfull</returns>
        public async Task<bool> ForceDownloadAsync(long[] linkIds, long[] packageIds)
        {
            var param = new[] { linkIds, packageIds };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<object>>(Device, "/downloadcontroller/forceDownload",
              param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response != null;
        }

        /// <summary>
        /// Forces JDownloader to start downloading the given links/packages
        /// </summary>
        /// <param name="linkIds">The ids of the links you want to force download.</param>
        /// <param name="packageIds">The ids of the packages you want to force download.</param>
        /// <returns>True if successfull</returns>
        public bool ForceDownload(long[] linkIds, long[] packageIds)
        {
            return ForceDownloadAsync(linkIds, packageIds).Result;
        }

        /// <summary>
        /// Gets the current state of the device
        /// </summary>
        /// <returns>The current state of the device.</returns>
        public async Task<string> GetCurrentStateAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<string>>(Device, "/downloadcontroller/getCurrentState",
              null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response != null)
                return response.Data;
            return "UNKOWN_STATE";
        }

        /// <summary>
        /// Gets the current state of the device
        /// </summary>
        /// <returns>The current state of the device.</returns>
        public string GetCurrentState()
        {
            return GetCurrentStateAsync().Result;
        }

        /// <summary>
        /// Gets the actual download speed of the client.
        /// </summary>
        /// <returns>The actual download speed.</returns>
        public async Task<long> GetSpeedInBpsAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<long>>(Device, "/downloadcontroller/getSpeedInBps", 
              null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response != null) return response.Data;
            return 0;
        }

        /// <summary>
        /// Gets the actual download speed of the client.
        /// </summary>
        /// <returns>The actual download speed.</returns>
        public long GetSpeedInBps()
        {
            return GetSpeedInBpsAsync().Result;
        }

        /// <summary>
        /// Starts all downloads.
        /// </summary>
        /// <returns>True if successfull.</returns>
        public async Task<bool> StartAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/downloadcontroller/start",
              null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Starts all downloads.
        /// </summary>
        /// <returns>True if successfull.</returns>
        public bool Start()
        {
            return StartAsync().Result;
        }

        /// <summary>
        /// Stops all downloads.
        /// </summary>
        /// <returns>True if successfull.</returns>
        public async Task<bool> StopAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/downloadcontroller/stop",
              null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Stops all downloads.
        /// </summary>
        /// <returns>True if successfull.</returns>
        public bool Stop()
        {
            return StopAsync().Result;
        }

        /// <summary>
        /// Pauses all downloads.
        /// </summary>
        /// <param name="pause">True if you want to pause the download</param>
        /// <returns>True if successfull.</returns>
        public async Task<bool> PauseAsync(bool pause)
        {
            var param = new[] { pause };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/downloadcontroller/pause",
              param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Pauses all downloads.
        /// </summary>
        /// <param name="pause">True if you want to pause the download</param>
        /// <returns>True if successfull.</returns>
        public bool Pause(bool pause)
        {
            return PauseAsync(pause).Result;
        }
    }
}
