using System.Collections.Generic;
using System.Threading.Tasks;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Captcha;
using My.JDownloader.Api.Models.Captcha.Request;
using My.JDownloader.Api.Models.Captcha.Response;
using My.JDownloader.Api.Models.Devices;
using Newtonsoft.Json.Linq;

namespace My.JDownloader.Api.Namespaces
{
    public class Captcha : Base
    {
        internal Captcha(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Gets the captcha by the given id
        /// </summary>
        /// <param name="id">The id of the captcha</param>
        /// <returns>An base64 encoded data url</returns>
        public async Task<string> GetAsync(long id)
        {
            var param = new[] { id };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<string>>(Device, "/captcha/get",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data != null)
                return response.Data;
            return string.Empty;
        }

        /// <summary>
        /// Gets the captcha by the given id
        /// </summary>
        /// <param name="id">The id of the captcha</param>
        /// <returns>An base64 encoded data url</returns>
        public string Get(long id)
        {
            return GetAsync(id).Result;
        }

        /// <summary>
        /// Gets the captcha by the given id.
        /// </summary>
        /// <param name="id">The id of the captcha.</param>
        /// <param name="format">This parameter was not described in the official api documentation.</param>
        /// <returns>An base64 encoded data url.</returns>
        public async Task<string> GetAsync(long id, string format)
        {
            var param = new object[] { id, format };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<string>>(Device, "/captcha/get",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data != null)
                return response.Data;
            return string.Empty;
        }

        /// <summary>
        /// Gets the captcha by the given id.
        /// </summary>
        /// <param name="id">The id of the captcha.</param>
        /// <param name="format">This parameter was not described in the official api documentation.</param>
        /// <returns>An base64 encoded data url.</returns>
        public string Get(long id, string format)
        {
            return GetAsync(id, format).Result;
        }

        /// <summary>
        /// Gets informations about a specific captcha job.
        /// </summary>
        /// <param name="id">The id of the captcha job.</param>
        /// <returns>An object which contains the informations about the captcha job.</returns>
        public async Task<CaptchaJobResponse> GetCaptchaJobAsync(long id)
        {
            var param = new[] { id };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<CaptchaJobResponse>>(Device, "/captcha/getCaptchaJob",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets informations about a specific captcha job.
        /// </summary>
        /// <param name="id">The id of the captcha job.</param>
        /// <returns>An object which contains the informations about the captcha job.</returns>
        public CaptchaJobResponse GetCaptchaJob(long id)
        {
            return GetCaptchaJobAsync(id).Result;
        }

        /// <summary>
        /// Gets all available captcha jobs.
        /// </summary>
        /// <returns>An enumerable which contains informations about all available captcha jobs.</returns>
        public async Task<IEnumerable<CaptchaJobResponse>> ListAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<CaptchaJobResponse>>>(Device, "/captcha/list",
                null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets all available captcha jobs.
        /// </summary>
        /// <returns>An enumerable which contains informations about all available captcha jobs.</returns>
        public IEnumerable<CaptchaJobResponse> List()
        {
            return ListAsync().Result;
        }

        /// <summary>
        /// Skips the captcha job by the given id.
        /// </summary>
        /// <param name="id">The id of the captcha job.</param>
        /// <param name="type">The skip type. Like block all captchas for the package.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SkipAsync(long id, SkipRequest type)
        {
            var param = new[] { id };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/captcha/skip",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Skips the captcha job by the given id.
        /// </summary>
        /// <param name="id">The id of the captcha job.</param>
        /// <param name="type">The skip type. Like block all captchas for the package.</param>
        /// <returns>True if successful.</returns>
        public bool Skip(long id, SkipRequest type)
        {
            return SkipAsync(id, type).Result;
        }

        /// <summary>
        /// Solves the captcha by the given id.
        /// </summary>
        /// <param name="id">The id of the captcha.</param>
        /// <param name="result">The result of the captcha.</param>
        /// <param name="resultFormat">The format of the result.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SolveAsync(long id, string result, string resultFormat)
        {
            var param = new object[] { id, result, resultFormat };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/captcha/solve",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Solves the captcha by the given id.
        /// </summary>
        /// <param name="id">The id of the captcha.</param>
        /// <param name="result">The result of the captcha.</param>
        /// <param name="resultFormat">The format of the result.</param>
        /// <returns>True if successful.</returns>
        public bool Solve(long id, string result, string resultFormat)
        {
            return SolveAsync(id, result, resultFormat).Result;
        }

        /// <summary>
        /// Solves the captcha by the given id.
        /// </summary>
        /// <param name="id">The id of the captcha.</param>
        /// <param name="result">The result of the captcha.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> SolveAsync(long id, string result)
        {
            var param = new object[] { id, result };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/captcha/solve",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Solves the captcha by the given id.
        /// </summary>
        /// <param name="id">The id of the captcha.</param>
        /// <param name="result">The result of the captcha.</param>
        /// <returns>True if successful.</returns>
        public bool Solve(long id, string result)
        {
            return SolveAsync(id, result).Result;
        }
    }
}