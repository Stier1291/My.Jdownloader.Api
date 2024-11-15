using System.Collections.Generic;
using System.Threading.Tasks;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.Extraction.Response;

namespace My.JDownloader.Api.Namespaces
{
    public class Extraction : Base
    {

        internal Extraction(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Adds an archive password to the client.
        /// </summary>
        /// <param name="password">The password to add.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> AddArchivePasswordAsync(string password)
        {
            var param = new[] { password };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<object>>(Device, "/extraction/addArchivePassword",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data != null;
        }

        /// <summary>
        /// Adds an archive password to the client.
        /// </summary>
        /// <param name="password">The password to add.</param>
        /// <returns>True if successful.</returns>
        public bool AddArchivePassword(string password)
        {
            return AddArchivePasswordAsync(password).Result;
        }

        /// <summary>
        /// Cancels an extraction process.
        /// </summary>
        /// <param name="controllerId">The id of the control you want to cancel.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> CancelExtractionAsync(string controllerId)
        {
            var param = new[] { controllerId };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/extraction/cancelExtraction",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Cancels an extraction process.
        /// </summary>
        /// <param name="controllerId">The id of the control you want to cancel.</param>
        /// <returns>True if successful.</returns>
        public bool CancelExtraction(string controllerId)
        {
            return CancelExtractionAsync(controllerId).Result;
        }

        /// <summary>
        /// Gets infos about the archive status.
        /// </summary>
        /// <param name="linkIds">Ids of the links you want to check.</param>
        /// <param name="packageIds">Ids of the packages you want to check.</param>
        /// <returns>An enumerable which contains all the archive statuses.</returns>
        public async Task<IEnumerable<ArchiveStatusResponse>> GetArchiveInfoAsync(long[] linkIds, long[] packageIds)
        {
            var param = new[] { linkIds, packageIds };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<ArchiveStatusResponse>>>(Device, "/extraction/getArchiveInfo",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets infos about the archive status.
        /// </summary>
        /// <param name="linkIds">Ids of the links you want to check.</param>
        /// <param name="packageIds">Ids of the packages you want to check.</param>
        /// <returns>An enumerable which contains all the archive statuses.</returns>
        public IEnumerable<ArchiveStatusResponse> GetArchiveInfo(long[] linkIds, long[] packageIds)
        {
            return GetArchiveInfoAsync(linkIds, packageIds).Result;
        }

        /// <summary>
        /// Gets the settings for the given archives.
        /// </summary>
        /// <param name="archiveIds">The archive ids you want the settings from.</param>
        /// <returns>An enumerable which contains the settings of the given archive ids.</returns>
        public async Task<IEnumerable<ArchiveSettingsResponse>> GetArchiveSettingsAsync(string[] archiveIds)
        {
            var param = new[] { archiveIds };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<ArchiveSettingsResponse>>>(Device, "/extraction/getArchiveSettings",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets the settings for the given archives.
        /// </summary>
        /// <param name="archiveIds">The archive ids you want the settings from.</param>
        /// <returns>An enumerable which contains the settings of the given archive ids.</returns>
        public IEnumerable<ArchiveSettingsResponse> GetArchiveSettings(string[] archiveIds)
        {
            return GetArchiveSettingsAsync(archiveIds).Result;
        }

        /// <summary>
        /// Gets all archives statuses that are currently queued.
        /// </summary>
        /// <returns>An enumerable which contains all archive statuses of the queued archives.</returns>
        public async Task<IEnumerable<ArchiveStatusResponse>> GetQueueAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<ArchiveStatusResponse>>>(Device, "/extraction/getQueue",
                null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets all archives statuses that are currently queued.
        /// </summary>
        /// <returns>An enumerable which contains all archive statuses of the queued archives.</returns>
        public IEnumerable<ArchiveStatusResponse> GetQueue()
        {
            return GetQueueAsync().Result;
        }

        /// <summary>
        /// Sets the settings for an archive.
        /// </summary>
        /// <param name="archiveId">The id of the archive you want to update the settings.</param>
        /// <param name="archiveSettings">The new settings for the archive.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SetArchiveSettingsAsync(string archiveId, ArchiveSettingsResponse archiveSettings)
        {
            var param = new object[] { archiveId, archiveSettings };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/extraction/setArchiveSettings",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Sets the settings for an archive.
        /// </summary>
        /// <param name="archiveId">The id of the archive you want to update the settings.</param>
        /// <param name="archiveSettings">The new settings for the archive.</param>
        /// <returns>True if successful.</returns>
        public bool SetArchiveSettings(string archiveId, ArchiveSettingsResponse archiveSettings)
        {
            return SetArchiveSettingsAsync(archiveId, archiveSettings).Result;
        }

        /// <summary>
        /// Starts the extraction for specific packages and links.
        /// </summary>
        /// <param name="linkIds">The ids of the links you want to start the extraction.</param>
        /// <param name="packageIds">The ids of the packages you want to start the extraction.</param>
        /// <returns>A dictionary which contains the archive id as the key and the extraction status as value.</returns>
        public async Task<Dictionary<string, bool?>> StartExtractionNowAsync(long[] linkIds, long[] packageIds)
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<Dictionary<string, bool?>>>(Device, "/extraction/startExtractionNow",
                null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Starts the extraction for specific packages and links.
        /// </summary>
        /// <param name="linkIds">The ids of the links you want to start the extraction.</param>
        /// <param name="packageIds">The ids of the packages you want to start the extraction.</param>
        /// <returns>A dictionary which contains the archive id as the key and the extraction status as value.</returns>
        public Dictionary<string, bool?> StartExtractionNow(long[] linkIds, long[] packageIds)
        {
            return StartExtractionNowAsync(linkIds, packageIds).Result;
        }
    }
}
