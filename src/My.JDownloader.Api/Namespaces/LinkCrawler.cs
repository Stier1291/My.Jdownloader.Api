using System.Threading.Tasks;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public class LinkCrawler : Base
    {

        internal LinkCrawler(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Asks the client if the linkcrawler is still crawling.
        /// </summary>
        /// <returns>Ture if succesfull</returns>
        public async Task<bool> IsCrawlingAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/linkcrawler/isCrawling", 
              null, JDownloaderHandler.LoginObject).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Asks the client if the linkcrawler is still crawling.
        /// </summary>
        /// <returns>Ture if succesfull</returns>
        public bool IsCrawling()
        {
            return IsCrawlingAsync().Result;
        }
    }
}
