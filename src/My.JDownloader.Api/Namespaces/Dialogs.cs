using System.Collections.Generic;
using System.Threading.Tasks;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.Dialog.Response;

namespace My.JDownloader.Api.Namespaces
{
    public class Dialogs : Base
    {
        internal Dialogs(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Answers the dialog by id.
        /// </summary>
        /// <param name="id">The id of the dialog.</param>
        /// <param name="data">The data you want to fill into the dialog.</param>
        public async Task AnswerAsync(long id, Dictionary<object, object> data)
        {
            var param = new object[] { id, data };
            await ApiHandler.CallActionAsync<DefaultResponse<object>>(Device, "/dialogs/answer",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Answers the dialog by id.
        /// </summary>
        /// <param name="id">The id of the dialog.</param>
        /// <param name="data">The data you want to fill into the dialog.</param>
        public void Answer(long id, Dictionary<object, object> data)
        {
            AnswerAsync(id, data).Wait();
        }

        /// <summary>
        /// Gets the informations about a dialog by id.
        /// </summary>
        /// <param name="id">The id of the dialog.</param>
        /// <param name="icon">True if you want the icon of the dialog.</param>
        /// <param name="properties">True if you want the properties of the dialog.</param>
        /// <returns>An object which contains the informations of the dialog.</returns>
        public async Task<DialogInfoResponse> GetAsync(long id, bool icon, bool properties)
        {
            var param = new object[] { id, icon, properties };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<DialogInfoResponse>>(Device, "/dialogs/get",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets the informations about a dialog by id.
        /// </summary>
        /// <param name="id">The id of the dialog.</param>
        /// <param name="icon">True if you want the icon of the dialog.</param>
        /// <param name="properties">True if you want the properties of the dialog.</param>
        /// <returns>An object which contains the informations of the dialog.</returns>
        public DialogInfoResponse Get(long id, bool icon, bool properties)
        {
            return GetAsync(id, icon, properties).Result;
        }

        /// <summary>
        /// Gets the informations about a specific dialog type.
        /// </summary>
        /// <param name="dialogType">Name of the dialog type.</param>
        /// <returns>An object which contains the informations about the dialog type.</returns>
        public async Task<DialogTypeInfoResponse> GetTypeInfoAsync(string dialogType)
        {
            var param = new[] { dialogType };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<DialogTypeInfoResponse>>(Device, "/dialogs/getTypeInfo",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets the informations about a specific dialog type.
        /// </summary>
        /// <param name="dialogType">Name of the dialog type.</param>
        /// <returns>An object which contains the informations about the dialog type.</returns>
        public DialogTypeInfoResponse GetTypeInfo(string dialogType)
        {
            return GetTypeInfoAsync(dialogType).Result;
        }

        /// <summary>
        /// Lists all dialogs.
        /// </summary>
        /// <returns>An array which contains the ids of the dialogs.</returns>
        public async Task<long[]> ListAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<long[]>>(Device,
                "/dialogs/list", null, JDownloaderHandler.LoginObject).ConfigureAwait(false);

            return response?.Data;
        }

        /// <summary>
        /// Lists all dialogs.
        /// </summary>
        /// <returns>An array which contains the ids of the dialogs.</returns>
        public long[] List()
        {
            return ListAsync().Result;
        }
    }
}
