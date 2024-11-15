using System.Threading.Tasks;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.System.Response;

namespace My.JDownloader.Api.Namespaces
{
    public class System : Base
    {

        internal System(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Closes the JDownloader client.
        /// </summary>
        public async Task ExitJdAsync()
        {
            await ApiHandler.CallActionAsync<object>(Device, "/system/exitJD",
              null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Closes the JDownloader client.
        /// </summary>
        public void ExitJd()
        {
            ExitJdAsync().Wait();
        }

        /// <summary>
        /// Gets storage informations of the given path.
        /// </summary>
        /// <param name="path">The Path you want to check.</param>
        /// <returns>An array with storage informations.</returns>
        public async Task<StorageInfoResponse[]> GetStorageInfosAsync(string path)
        {
            var param = new[] { path };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<StorageInfoResponse[]>>(Device, "/system/getStorageInfos", 
              param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets storage informations of the given path.
        /// </summary>
        /// <param name="path">The Path you want to check.</param>
        /// <returns>An array with storage informations.</returns>
        public StorageInfoResponse[] GetStorageInfos(string path)
        {
            return GetStorageInfosAsync(path).Result;
        }

        /// <summary>
        /// Gets information of the system the JDownloader client is running on.
        /// </summary>
        /// <returns></returns>
        public async Task<SystemInfoResponse> GetSystemInfosAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<SystemInfoResponse>>(Device, "/system/getSystemInfos", 
              null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets information of the system the JDownloader client is running on.
        /// </summary>
        /// <returns></returns>
        public SystemInfoResponse GetSystemInfos()
        {
            return GetSystemInfosAsync().Result;
        }

        /// <summary>
        /// Hibernates the current os the JDownloader client is running on.
        /// </summary>
        public async Task HibernateOsAsync()
        {
            await ApiHandler.CallActionAsync<object>(Device, "/system/hibernateOS",
              null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Hibernates the current os the JDownloader client is running on.
        /// </summary>
        public void HibernateOs()
        {
            HibernateOsAsync().Wait();
        }

        /// <summary>
        /// Restarts the JDownloader client.
        /// </summary>
        public async Task RestartJdAsync()
        {
            await ApiHandler.CallActionAsync<object>(Device, "/system/restartJD", 
              null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Restarts the JDownloader client.
        /// </summary>
        public void RestartJd()
        {
            RestartJdAsync().Wait();
        }

        /// <summary>
        /// Shutsdown the current os the JDownloader client is running on.
        /// </summary>
        /// <param name="force">True if you want to force the shutdown process.</param>
        public async Task ShutdownOsAsync(bool force)
        {
            await ApiHandler.CallActionAsync<object>(Device, "/system/shutdownOS",
              new[] { force }, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Shutsdown the current os the JDownloader client is running on.
        /// </summary>
        /// <param name="force">True if you want to force the shutdown process.</param>
        public void ShutdownOs(bool force)
        {
            ShutdownOsAsync(force).Wait();
        }

        /// <summary>
        /// Sets the current os the JDownloader client is running on in standby.
        /// </summary>
        public async Task StandbyOsAsync()
        {
            await ApiHandler.CallActionAsync<object>(Device, "/system/standbyOS", 
              null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the current os the JDownloader client is running on in standby.
        /// </summary>
        public void StandbyOs()
        {
            StandbyOsAsync().Wait();
        }
    }
}
