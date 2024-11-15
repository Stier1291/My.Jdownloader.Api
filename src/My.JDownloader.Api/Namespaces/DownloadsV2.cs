using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.DownloadsV2.Request;
using My.JDownloader.Api.Models.DownloadsV2.Response;
using My.JDownloader.Api.Models.Types;
using Newtonsoft.Json;

namespace My.JDownloader.Api.Namespaces
{
  public class DownloadsV2 : Base
  {

    internal DownloadsV2(JDownloaderApiHandler apiHandler, Device device)
    {
      ApiHandler = apiHandler;
      Device = device;
    }


    /// <summary>
    /// Cleans up the downloader list.
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
      var response = await ApiHandler.CallActionAsync<object>(Device, "/downloadsV2/cleanup",
          param, JDownloaderHandler.LoginObject).ConfigureAwait(false);
      if (response == null) return false;
      return true;
    }

    /// <summary>
    /// Cleans up the downloader list.
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
    /// Forcing the download on specific links/packages.
    /// </summary>
    /// <param name="linkIds">Ids of the link you may want to force the download.</param>
    /// <param name="packageIds">Ids of the packages you may want to force the download.</param>
    /// <returns></returns>
    public async Task<bool> ForceDownloadAsync(long[] linkIds, long[] packageIds)
    {
      var param = new object[] { linkIds, packageIds };
      var response = await ApiHandler.CallActionAsync<object>(Device, "/downloadsV2/forceDownload",
          param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
      if (response == null) return false;
      return true;
    }

    /// <summary>
    /// Forcing the download on specific links/packages.
    /// </summary>
    /// <param name="linkIds">Ids of the link you may want to force the download.</param>
    /// <param name="packageIds">Ids of the packages you may want to force the download.</param>
    /// <returns></returns>
    public bool ForceDownload(long[] linkIds, long[] packageIds)
    {
      return ForceDownloadAsync(linkIds, packageIds).Result;
    }


    /// <summary>
    /// Gets the 'download url' of the given links/packages.
    /// </summary>
    /// <param name="linkIds">Ids of the link you may want to force the download.</param>
    /// <param name="packageIds">Ids of the packages you may want to force the download.</param>
    /// <param name="urlDisplayType"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, long[]>> GetDownloadUrlsAsync(long[] linkIds, long[] packageIds, UrlDisplayType[] urlDisplayType)
    {
      var param = new object[] { linkIds, packageIds, urlDisplayType.ToString() };
      var response = await ApiHandler.CallActionAsync<DefaultResponse<Dictionary<string, long[]>>>(Device, "/downloadsV2/getDownloadUrls",
          param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
      return response?.Data;
    }

    /// <summary>
    /// Gets the 'download url' of the given links/packages.
    /// </summary>
    /// <param name="linkIds">Ids of the link you may want to force the download.</param>
    /// <param name="packageIds">Ids of the packages you may want to force the download.</param>
    /// <param name="urlDisplayType"></param>
    /// <returns></returns>
    public Dictionary<string, long[]> GetDownloadUrls(long[] linkIds, long[] packageIds, UrlDisplayType[] urlDisplayType)
    {
      return GetDownloadUrlsAsync(linkIds, packageIds, urlDisplayType).Result;
    }


    /// <summary>
    /// Gets all entires that are currently in the download list.
    /// </summary>
    /// <param name="linkQuery">An object which allows you to filter the return object.</param>
    /// <returns>An enumerable of the DownloadLink which contains infos about the entries.</returns>
    public async Task<IEnumerable<DownloadLinkResponse>> QueryLinksAsync(LinkQueryRequest linkQuery)
    {
      string json = JsonConvert.SerializeObject(linkQuery);
      var param = new[] { json };
      var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<DownloadLinkResponse>>>(Device, "/downloadsV2/queryLinks",
          param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
      return response?.Data;
    }

    /// <summary>
    /// Gets all entires that are currently in the download list.
    /// </summary>
    /// <param name="linkQuery">An object which allows you to filter the return object.</param>
    /// <returns>An enumerable of the DownloadLink which contains infos about the entries.</returns>
    public IEnumerable<DownloadLinkResponse> QueryLinks(LinkQueryRequest linkQuery)
    {
      return QueryLinksAsync(linkQuery).Result;
    }

    /// <summary>
    /// Gets all packages that are currently in the download list.
    /// </summary>
    /// <param name="linkQuery">An object which allows you to filter the return object.</param>
    /// <returns>An enumerable of the FilePackageResponse which contains infos about the packages.</returns>
    public async Task<IEnumerable<FilePackageResponse>> QueryPackagesAsync(LinkQueryRequest linkQuery)
    {
      string json = JsonConvert.SerializeObject(linkQuery);
      var param = new[] { json };
      var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<FilePackageResponse>>>(Device, "/downloadsV2/queryPackages",
          param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
      return response?.Data.ToArray();
    }

    /// <summary>
    /// Gets all packages that are currently in the download list.
    /// </summary>
    /// <param name="linkQuery">An object which allows you to filter the return object.</param>
    /// <returns>An enumerable of the FilePackageResponse which contains infos about the packages.</returns>
    public IEnumerable<FilePackageResponse> QueryPackages(LinkQueryRequest linkQuery)
    {
      return QueryPackagesAsync(linkQuery).Result;
    }

    /// <summary>
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
      var response = await ApiHandler.CallActionAsync<DefaultResponse<object>>(Device, "/downloadsV2/setComment",
          param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
      return response?.Data;
    }

    /// <summary>
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
