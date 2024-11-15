using System.Threading.Tasks;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public class CaptchaForward : Base
    {
        internal CaptchaForward(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Creates a recaptcha job.
        /// For more informations https://my.jdownloader.org/developers/#tag_53.
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <param name="three"></param>
        /// <param name="four"></param>
        /// <returns>Propably the id of the created job.</returns>
        public async Task<long> CreateJobRecaptchaV2Async(string one, string two, string three, string four)
        {
            var param = new[] { one, two, three, four };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<long>>(Device, "/captchaforward/createJobRecaptchaV2",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data != null) return response.Data;
            return -1;
        }

        /// <summary>
        /// Creates a recaptcha job.
        /// For more informations https://my.jdownloader.org/developers/#tag_53.
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <param name="three"></param>
        /// <param name="four"></param>
        /// <returns>Propably the id of the created job.</returns>
        public long CreateJobRecaptchaV2(string one, string two, string three, string four)
        {
            return CreateJobRecaptchaV2Async(one, two, three, four).Result;
        }

        /// <summary>
        /// Gets the result of the captcha by the given id.
        /// </summary>
        /// <param name="id">The id of the job.</param>
        /// <returns>String which contians the result of the captcha.</returns>
        public async Task<string> GetResultAsync(long id)
        {
            var param = new[] { id };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<string>>(Device, "/captchaforward/getResult",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data != null) return response.Data;
            return string.Empty;
        }

        /// <summary>
        /// Gets the result of the captcha by the given id.
        /// </summary>
        /// <param name="id">The id of the job.</param>
        /// <returns>String which contians the result of the captcha.</returns>
        public string GetResult(long id)
        {
            return GetResultAsync(id).Result;
        }
    }
}