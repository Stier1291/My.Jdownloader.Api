using System.Collections.Generic;
using System.Threading.Tasks;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.LinkgrabberV2;
using My.JDownloader.Api.Models.LinkgrabberV2.Request;
using My.JDownloader.Api.Models.LinkgrabberV2.Response;
using My.JDownloader.Api.Models.LinkgrabberV2.Types;
using My.JDownloader.Api.Models.Types;
using Newtonsoft.Json;

namespace My.JDownloader.Api.Namespaces
{
    public class LinkGrabberV2 : Base
    {

        internal LinkGrabberV2(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Aborts the linkgrabber process for a specific job.
        /// </summary>
        /// <param name="jobId">The jobId you wnat to abort.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> AbortAsync(long jobId = -1)
        {
            var param = new[] { jobId };
            if (jobId == -1)
                param = null;
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/linkgrabberv2/abort",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Aborts the linkgrabber process for a specific job.
        /// </summary>
        /// <param name="jobId">The jobId you wnat to abort.</param>
        /// <returns>True if successful.</returns>
        public bool Abort(long jobId = -1)
        {
            return AbortAsync(jobId).Result;
        }

        /// <summary>
        /// Adds a container to the linkcollector list.
        /// </summary>
        /// <param name="type">The value can be: DLC, RSDF, CCF or CRAWLJOB</param>
        /// <param name="content">File as dataurl. https://de.wikipedia.org/wiki/Data-URL </param>
        public async Task<bool> AddContainerAsync(ContainerType type, string content)
        {
            AddContainer container = new AddContainer
            {
                Type = type.ToString(),
                Content = content
            };

            var json = JsonConvert.SerializeObject(container);
            var param = new[] { json };
            var response = await ApiHandler.CallActionAsync<object>(Device, "/linkgrabberv2/addContainer",
                param, JDownloaderHandler.LoginObject).ConfigureAwait(false);
            return response != null;
        }

        /// <summary>
        /// Adds a container to the linkcollector list.
        /// </summary>
        /// <param name="type">The value can be: DLC, RSDF, CCF or CRAWLJOB</param>
        /// <param name="content">File as dataurl. https://de.wikipedia.org/wiki/Data-URL </param>
        public bool AddContainer(ContainerType type, string content)
        {
            return AddContainerAsync(type, content).Result;
        }

        /// <summary>
        /// Adds a download link to the given device.
        /// </summary>
        /// <param name="request">Contains informations like the link itself or the priority. If you want to use multiple links sperate them with an ';' char.</param>
        public async Task<bool> AddLinksAsync(AddLinkRequest request)
        {
            request.Links = request.Links.Replace(";", "\\r\\n");
            var json = JsonConvert.SerializeObject(request);
            var param = new[] { json };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<object>>(Device, "/linkgrabberv2/addLinks",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response != null;
        }

        /// <summary>
        /// Adds a download link to the given device.
        /// </summary>
        /// <param name="request">Contains informations like the link itself or the priority. If you want to use multiple links sperate them with an ';' char.</param>
        public bool AddLinks(AddLinkRequest request)
        {
            return AddLinksAsync(request).Result;
        }

        /// <summary>
        /// Adds a variant copy of the link.
        /// </summary>
        /// <param name="linkId">The link id you want to copy.</param>
        /// <param name="destinationAfterLinkId"></param>
        /// <param name="destinationPackageId"></param>
        /// <param name="variantId"></param>
        /// <returns>True if successful.</returns>
        public async Task<bool> AddVariantCopyAsync(long linkId, long destinationAfterLinkId, long destinationPackageId, string variantId)
        {
            var param = new[]
                {linkId.ToString(), destinationAfterLinkId.ToString(), destinationPackageId.ToString(), variantId};
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/linkgrabberv2/addVariantCopy",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response != null;
        }

        /// <summary>
        /// Adds a variant copy of the link.
        /// </summary>
        /// <param name="linkId">The link id you want to copy.</param>
        /// <param name="destinationAfterLinkId"></param>
        /// <param name="destinationPackageId"></param>
        /// <param name="variantId"></param>
        /// <returns>True if successful.</returns>
        public bool AddVariantCopy(long linkId, long destinationAfterLinkId, long destinationPackageId, string variantId)
        {
            return AddVariantCopyAsync(linkId, destinationAfterLinkId, destinationPackageId, variantId).Result;
        }

        /// <summary>
        /// Cleans up the linkgrabber list.
        /// </summary>
        /// <param name="linkIds">Ids of the link you may want to clear.</param>
        /// <param name="packageIds">Ids of the packages you may want to clear.</param>
        /// <param name="action">The action type.</param>
        /// <param name="mode">The mode type.</param>
        /// <param name="selection">The selection Type.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> CleanUpAsync(long[] linkIds, long[] packageIds, ActionType action, ModeType mode, SelectionType selection)
        {
            var param = new object[] { linkIds, packageIds, action.ToString(), mode.ToString(), selection.ToString() };
            var response = await ApiHandler.CallActionAsync<object>(Device, "/linkgrabberv2/cleanup",
                param, JDownloaderHandler.LoginObject).ConfigureAwait(false);
            if (response == null) return false;
            return true;
        }

        /// <summary>
        /// Cleans up the linkgrabber list.
        /// </summary>
        /// <param name="linkIds">Ids of the link you may want to clear.</param>
        /// <param name="packageIds">Ids of the packages you may want to clear.</param>
        /// <param name="action">The action type.</param>
        /// <param name="mode">The mode type.</param>
        /// <param name="selection">The selection Type.</param>
        /// <returns>True if successful.</returns>
        public bool CleanUp(long[] linkIds, long[] packageIds, ActionType action, ModeType mode, SelectionType selection)
        {
            return CleanUpAsync(linkIds, packageIds, action, mode, selection).Result;
        }

        /// <summary>
        /// Clears the downloader list.
        /// </summary>
        /// <returns>True if successful</returns>
        public async Task<bool> ClearListAsync()
        {
            var response = await ApiHandler.CallActionAsync<object>(Device, "/linkgrabberv2/clearList",
                null, JDownloaderHandler.LoginObject).ConfigureAwait(false);
            if (response == null) return false;
            return true;
        }

        /// <summary>
        /// Clears the downloader list.
        /// </summary>
        /// <returns>True if successful</returns>
        public bool ClearList()
        {
            return ClearListAsync().Result;
        }

        /// <summary>
        /// Not documented what it really does.
        /// </summary>
        /// <param name="structureWatermark"></param>
        /// <returns></returns>
        public async Task<long> GetChildrenChangedAsync(long structureWatermark)
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<long>>(Device, "/linkgrabberv2/getChildrenChanged",
                structureWatermark, JDownloaderHandler.LoginObject).ConfigureAwait(false);
            if (response?.Data != null) return response.Data;
            return -1;
        }

        /// <summary>
        /// Not documented what it really does.
        /// </summary>
        /// <param name="structureWatermark"></param>
        /// <returns></returns>
        public long GetChildrenChanged(long structureWatermark)
        {
            return GetChildrenChangedAsync(structureWatermark).Result;
        }

        /// <summary>
        /// Gets the selection base of the download folder history.
        /// </summary>
        /// <returns>An array which contains the download folder history.</returns>
        public async Task<string[]> GetDownloadFolderHistorySelectionBaseAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<string[]>>(Device, "/linkgrabberv2/getDownloadFolderHistorySelectionBase",
                null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets the selection base of the download folder history.
        /// </summary>
        /// <returns>An array which contains the download folder history.</returns>
        public string[] GetDownloadFolderHistorySelectionBase()
        {
            return GetDownloadFolderHistorySelectionBaseAsync().Result;
        }

        // TODO: Describe what this function does.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="links"></param>
        /// <param name="afterLinkId"></param>
        /// <param name="destPackageId"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, List<long>>> GetDownloadUrlsAsync(long[] links, long afterLinkId, long destPackageId)
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<Dictionary<string, List<long>>>>(Device, "/linkgrabberv2/getDownloadUrls", 
              null, JDownloaderHandler.LoginObject).ConfigureAwait(false);
            return response?.Data;
        }

        // TODO: Describe what this function does.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="links"></param>
        /// <param name="afterLinkId"></param>
        /// <param name="destPackageId"></param>
        /// <returns></returns>
        public Dictionary<string, List<long>> GetDownloadUrls(long[] links, long afterLinkId, long destPackageId)
        {
            return GetDownloadUrlsAsync(links, afterLinkId, destPackageId).Result;
        }

        /// <summary>
        /// Checks how many packages are inside the linkcollector.
        /// </summary>
        /// <returns>The amount of links which are in the linkcollector.</returns>
        public async Task<int> GetPackageCountAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<int>>(Device, "/linkgrabberv2/getPackageCount",
                null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response == null) return 0;
            return response.Data;
        }

        /// <summary>
        /// Checks how many packages are inside the linkcollector.
        /// </summary>
        /// <returns>The amount of links which are in the linkcollector.</returns>
        public int GetPackageCount()
        {
            return GetPackageCountAsync().Result;
        }

        /// <summary>
        /// Gets the stop mark as long.
        /// </summary>
        /// <returns>The stop mark as long.</returns>
        public async Task<long> GetStopMarkAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<long>>(Device, "/linkgrabberv2/getStopMark",
                null, JDownloaderHandler.LoginObject).ConfigureAwait(false);
            if (response?.Data != null) return response.Data;
            return -1;
        }

        /// <summary>
        /// Gets the stop mark as long.
        /// </summary>
        /// <returns>The stop mark as long.</returns>
        public long GetStopMark()
        {
            return GetStopMarkAsync().Result;
        }

        /// <summary>
        /// Gets informations about a stop marked link.
        /// </summary>
        /// <returns>Returns informations about a stop marked link.</returns>
        public async Task<StopMarkedLinkResponse> GetStopMarkedLinkAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<StopMarkedLinkResponse>>(Device, "/linkgrabberv2/getStopMark",
                null, JDownloaderHandler.LoginObject).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets informations about a stop marked link.
        /// </summary>
        /// <returns>Returns informations about a stop marked link.</returns>
        public StopMarkedLinkResponse GetStopMarkedLink()
        {
            return GetStopMarkedLinkAsync().Result;
        }

        /// <summary>
        /// Gets variants of the link.
        /// </summary>
        /// <param name="linkId">The link id you want to get the variants of.</param>
        /// <returns>An enumerable which contains variants of the given link.</returns>
        public async Task<IEnumerable<GetVariantsResponse>> GetVariantsAsync(long linkId)
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<GetVariantsResponse>>>(Device, "/linkgrabberv2/getVariants",
                null, JDownloaderHandler.LoginObject).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets variants of the link.
        /// </summary>
        /// <param name="linkId">The link id you want to get the variants of.</param>
        /// <returns>An enumerable which contains variants of the given link.</returns>
        public IEnumerable<GetVariantsResponse> GetVariants(long linkId)
        {
            return GetVariantsAsync(linkId).Result;
        }

        /// <summary>
        /// Checks if the JDownloader client is still collecting files from links.
        /// </summary>
        /// <returns>Returns true or false. Depending on if the client is still collecting files.</returns>
        public async Task<bool> IsCollectingAsync()
        {
            var response = await ApiHandler.CallActionAsync<object>(Device, "/linkgrabberv2/isCollecting",
                null, JDownloaderHandler.LoginObject).ConfigureAwait(false);
            if (response == null) return false;
            return true;
        }

        /// <summary>
        /// Checks if the JDownloader client is still collecting files from links.
        /// </summary>
        /// <returns>Returns true or false. Depending on if the client is still collecting files.</returns>
        public bool IsCollecting()
        {
            return IsCollectingAsync().Result;
        }

        /// <summary>
        /// Moves one or multiple links after another link or inside a package.
        /// </summary>
        /// <param name="linkIds">The ids of the links you want to move.</param>
        /// <param name="afterLinkId">The id of the link you want to move the other links to.</param>
        /// <param name="destPackageId">The id of the package where you want to add the links to.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> MoveLinksAsync(long[] linkIds, long afterLinkId, long destPackageId)
        {
            var param = new object[] { linkIds, afterLinkId, destPackageId };
            var response = await ApiHandler.CallActionAsync<object>(Device, "/linkgrabberv2/moveLinks",
                param, JDownloaderHandler.LoginObject).ConfigureAwait(false);
            if (response == null) return false;
            return true;
        }

        /// <summary>
        /// Moves one or multiple links after another link or inside a package.
        /// </summary>
        /// <param name="linkIds">The ids of the links you want to move.</param>
        /// <param name="afterLinkId">The id of the link you want to move the other links to.</param>
        /// <param name="destPackageId">The id of the package where you want to add the links to.</param>
        /// <returns>True if successful.</returns>
        public bool MoveLinks(long[] linkIds, long afterLinkId, long destPackageId)
        {
            return MoveLinksAsync(linkIds, afterLinkId, destPackageId).Result;
        }

        /// <summary>
        /// Moves one or multiple packages after antoher package.
        /// </summary>
        /// <param name="packageIds">The ids of the packages you want to move.</param>
        /// <param name="afterDestPackageId">The id of the package you want to move the others to.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> MovePackagesAsync(long[] packageIds, long afterDestPackageId)
        {
            var param = new object[] { packageIds, afterDestPackageId };
            var response = await ApiHandler.CallActionAsync<object>(Device, "/linkgrabberv2/movePackages",
                param, JDownloaderHandler.LoginObject).ConfigureAwait(false);
            if (response == null) return false;
            return true;
        }

        /// <summary>
        /// Moves one or multiple packages after antoher package.
        /// </summary>
        /// <param name="packageIds">The ids of the packages you want to move.</param>
        /// <param name="afterDestPackageId">The id of the package you want to move the others to.</param>
        /// <returns>True if successful.</returns>
        public bool MovePackages(long[] packageIds, long afterDestPackageId)
        {
            return MovePackagesAsync(packageIds, afterDestPackageId).Result;
        }

        /// <summary>
        /// Moves one or multiple links/packages to the download list.
        /// </summary>
        /// <param name="linkIds">The ids of the links you want to move.</param>
        /// <param name="packageIds">The ids of the packages you want to move.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> MoveToDownloadlistAsync(long[] linkIds, long[] packageIds)
        {
            var param = new[] { linkIds, packageIds };
            var response = await ApiHandler.CallActionAsync<object>(Device, "/linkgrabberv2/moveToDownloadlist",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response == null) return false;
            return true;
        }

        /// <summary>
        /// Moves one or multiple links/packages to the download list.
        /// </summary>
        /// <param name="linkIds">The ids of the links you want to move.</param>
        /// <param name="packageIds">The ids of the packages you want to move.</param>
        /// <returns>True if successful.</returns>
        public bool MoveToDownloadlist(long[] linkIds, long[] packageIds)
        {
            return MoveToDownloadlistAsync(linkIds, packageIds).Result;
        }

        /// <summary>
        /// Move one or multiple links/packages to a new package.
        /// </summary>
        /// <param name="linkIds">The ids of the links you want to move.</param>
        /// <param name="packageIds">The ids of the packages you want to move.</param>
        /// <param name="newPackageName">The name of the new package.</param>
        /// <param name="downloadPath">The download path.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> MoveToNewPackageAsync(long[] linkIds, long[] packageIds, string newPackageName, string downloadPath)
        {
            var param = new object[] { linkIds, packageIds, newPackageName, downloadPath };
            var response = await ApiHandler.CallActionAsync<object>(Device, "/linkgrabberv2/moveToNewPackage",
                param, JDownloaderHandler.LoginObject).ConfigureAwait(false);
            if (response == null) return false;
            return true;
        }

        /// <summary>
        /// Move one or multiple links/packages to a new package.
        /// </summary>
        /// <param name="linkIds">The ids of the links you want to move.</param>
        /// <param name="packageIds">The ids of the packages you want to move.</param>
        /// <param name="newPackageName">The name of the new package.</param>
        /// <param name="downloadPath">The download path.</param>
        /// <returns>True if successful.</returns>
        public bool MoveToNewPackage(long[] linkIds, long[] packageIds, string newPackageName, string downloadPath)
        {
            return MoveToNewPackageAsync(linkIds, packageIds, newPackageName, downloadPath).Result;
        }

        /// <summary>
        /// Gets all links that are currently in the linkcollector.
        /// </summary>
        /// <param name="queryLinksRequest"></param>
        /// <returns>An enumerable of all links that are currently in the linkcollector list.</returns>
        public async Task<IEnumerable<QueryLinksResponse>> QueryLinksAsync(QueryLinksRequest queryLinksRequest)
        {
            string json = JsonConvert.SerializeObject(queryLinksRequest);
            var param = new[] { json };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<QueryLinksResponse>>>(Device, "/linkgrabberv2/queryLinks",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets all links that are currently in the linkcollector.
        /// </summary>
        /// <param name="queryLinksRequest"></param>
        /// <returns>An enumerable of all links that are currently in the linkcollector list.</returns>
        public IEnumerable<QueryLinksResponse> QueryLinks(QueryLinksRequest queryLinksRequest)
        {
            return QueryLinksAsync(queryLinksRequest).Result;
        }

        /// <summary>
        /// Gets a list of available packages that are currently in the linkcollector.
        /// </summary>
        /// <param name="request">The request object which contains properties to define the return properties.</param>
        /// <returns>An enumerable of all available packages.</returns>
        public async Task<IEnumerable<QueryPackagesResponse>> QueryPackagesAsync(QueryPackagesRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
            var param = new[] { json };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<QueryPackagesResponse>>>(Device, "/linkgrabberv2/queryPackages",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets a list of available packages that are currently in the linkcollector.
        /// </summary>
        /// <param name="request">The request object which contains properties to define the return properties.</param>
        /// <returns>An enumerable of all available packages.</returns>
        public IEnumerable<QueryPackagesResponse> QueryPackages(QueryPackagesRequest request)
        {
            return QueryPackagesAsync(request).Result;
        }

        /// <summary>
        /// Allows you to change the download directory of multiple packages.
        /// </summary>
        /// <param name="directory">The new download directory.</param>
        /// <param name="packageIds">The ids of the packages.</param>
        /// <returns>True if successful</returns>
        public async Task<bool> SetDownloadDirectoryAsync(string directory, long[] packageIds)
        {
            var param = new object[] { directory, packageIds };
            var response = await ApiHandler.CallActionAsync<object>(Device, "/linkgrabberv2/setDownloadDirectory",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response != null;
        }

        /// <summary>
        /// Allows you to change the download directory of multiple packages.
        /// </summary>
        /// <param name="directory">The new download directory.</param>
        /// <param name="packageIds">The ids of the packages.</param>
        /// <returns>True if successful</returns>
        public bool SetDownloadDirectory(string directory, long[] packageIds)
        {
            return SetDownloadDirectoryAsync(directory, packageIds).Result;
        }

        /// <summary>
        /// THIS CALL DOES NOT WORK FOR NOW!
        /// Sets the comment for the given links/packages within the downloads tab
        /// </summary>
        /// <param name="linkIds">Array of link ids</param>
        /// <param name="packageIds">Array of package ids</param>
        /// <param name="setPackageChildren">True if also the links within the given packages should get the comment set</param>
        /// <param name="comment">The comment</param>
        /// <returns>Nothing for now since it's undocumented.</returns>
        public async Task<object> SetCommentAsync(long[] linkIds, long[] packageIds, bool setPackageChildren, string comment)
        {
            var param = new object[] { linkIds, packageIds, setPackageChildren, comment };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<object>>(Device, "/linkgrabberv2/setComment",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// THIS CALL DOES NOT WORK FOR NOW!
        /// Sets the comment for the given links/packages within the downloads tab
        /// </summary>
        /// <param name="linkIds">Array of link ids</param>
        /// <param name="packageIds">Array of package ids</param>
        /// <param name="setPackageChildren">True if also the links within the given packages should get the comment set</param>
        /// <param name="comment">The comment</param>
        /// <returns>Nothing for now since it's undocumented.</returns>
        public object SetComment(long[] linkIds, long[] packageIds, bool setPackageChildren, string comment)
        {
            return SetCommentAsync(linkIds, packageIds, setPackageChildren, comment).Result;
        }
    }
}
