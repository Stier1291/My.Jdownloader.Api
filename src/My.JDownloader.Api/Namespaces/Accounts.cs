using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Accounts;
using My.JDownloader.Api.Models.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public class Accounts : Base
    {
        internal Accounts(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Adds an premium account to your JDownloader device.
        /// </summary>
        /// <param name="hoster">The hoster e.g. mega.co.nz</param>
        /// <param name="email">Your email</param>
        /// <param name="password">Your password</param>
        /// <returns>True if the account was successfully added.</returns>
        public async Task<bool> AddAccountAsync(string hoster, string email, string password)
        {
            var param = new[] { hoster, email, password };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/accounts/addAccount",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Adds an premium account to your JDownloader device.
        /// </summary>
        /// <param name="hoster">The hoster e.g. mega.co.nz</param>
        /// <param name="email">Your email</param>
        /// <param name="password">Your password</param>
        /// <returns>True if the account was successfully added.</returns>
        public bool AddAccount(string hoster, string email, string password)
        {
            return AddAccountAsync(hoster, email, password).Result;
        }

        /// <summary>
        /// Disables an account to download.
        /// </summary>
        /// <param name="accountIds">The account ids you want to disable.</param>
        /// <returns>True if succesful</returns>
        public async Task<bool> DisableAccountsAsync(long[] accountIds)
        {
            var param = new[] { accountIds };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/accounts/disableAccounts",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Disables an account to download.
        /// </summary>
        /// <param name="accountIds">The account ids you want to disable.</param>
        /// <returns>True if succesful</returns>
        public bool DisableAccounts(long[] accountIds)
        {
            return DisableAccountsAsync(accountIds).Result;
        }

        /// <summary>
        /// Enables an account to download.
        /// </summary>
        /// <param name="accountIds">The account ids you want to enable.</param>
        /// <returns>True if succesful</returns>
        public async Task<bool> EnableAccountsAsync(long[] accountIds)
        {
            var param = new[] { accountIds };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/accounts/enableAccounts",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Enables an account to download.
        /// </summary>
        /// <param name="accountIds">The account ids you want to enable.</param>
        /// <returns>True if succesful</returns>
        public bool EnableAccounts(long[] accountIds)
        {
            return EnableAccountsAsync(accountIds).Result;
        }

        /// <summary>
        /// Gets information about an account
        /// </summary>
        /// <param name="accountId">The id of the account you want to check</param>
        /// <returns>An object which contains the informations about the account.</returns>
        public async Task<Account> GetAccountInfoAsync(long accountId)
        {
            var param = new[] { accountId };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<Account>>(Device, "/accounts/getAccountInfo",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets information about an account
        /// </summary>
        /// <param name="accountId">The id of the account you want to check</param>
        /// <returns>An object which contains the informations about the account.</returns>
        public Account GetAccountInfo(long accountId)
        {
            return GetAccountInfoAsync(accountId).Result;
        }

        /// <summary>
        /// Gets a link of a hoster by the name of it.
        /// </summary>
        /// <param name="hoster">Name of the hoster you want the url from.</param>
        /// <returns>The url of the hoster.</returns>
        public async Task<string> GetPremiumHosterUrlAsync(string hoster)
        {
            var param = new[] { hoster };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<string>>(Device, "/accounts/getPremiumHosterUrl",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data != null)
                return response.Data;
            return string.Empty;
        }

        /// <summary>
        /// Gets a link of a hoster by the name of it.
        /// </summary>
        /// <param name="hoster">Name of the hoster you want the url from.</param>
        /// <returns>The url of the hoster.</returns>
        public string GetPremiumHosterUrl(string hoster)
        {
            return GetPremiumHosterUrlAsync(hoster).Result;
        }

        /// <summary>
        /// Gets all available premium hoster names of the client.
        /// </summary>
        /// <returns>An enumerable of all available premium hoster names.</returns>
        public async Task<IEnumerable<string>> ListPremiumHosterAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<string>>>(Device, "/accounts/listPremiumHoster", null,
                JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets all available premium hoster names of the client.
        /// </summary>
        /// <returns>An enumerable of all available premium hoster names.</returns>
        public IEnumerable<string> ListPremiumHoster()
        {
            return ListPremiumHosterAsync().Result;
        }

        /// <summary>
        /// Gets all premium hoster names + urls that JDownloader supports.
        /// </summary>
        /// <returns>Returns a dictionary containing the hostername as the key and the url as the value.</returns>
        public async Task<Dictionary<string, string>> ListPremiumHosterUrlsAsync()
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<Dictionary<string, string>>>(Device, "/accounts/listPremiumHosterUrls",
                null,
                JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Gets all premium hoster names + urls that JDownloader supports.
        /// </summary>
        /// <returns>Returns a dictionary containing the hostername as the key and the url as the value.</returns>
        public Dictionary<string, string> ListPremiumHosterUrls()
        {
            return ListPremiumHosterUrlsAsync().Result;
        }

        /// <summary>
        /// Gets the icon of the given premium hoster.
        /// </summary>
        /// <returns>Returns an byte array which contains the hoster icon.</returns>
        public async Task<string> PremiumHosterIconAsync(string premiumHoster)
        {
            var response = await ApiHandler.CallActionAsync<string>(Device, "/accounts/premiumHosterIcon",
                new[] { premiumHoster },
                JDownloaderHandler.LoginObject, false, false).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets the icon of the given premium hoster.
        /// </summary>
        /// <returns>Returns an byte array which contains the hoster icon.</returns>
        public string PremiumHosterIcon(string premiumHoster)
        {
            return PremiumHosterIconAsync(premiumHoster).Result;
        }

        /// <summary>
        /// Queries all accounts.
        /// </summary>
        /// <param name="query">The queryRequest settings.</param>
        /// <returns>An enumerable which contains all accounts.</returns>
        public async Task<IEnumerable<Account>> QueryAccountsAsync(ApiQuery query)
        {
            var response = await ApiHandler.CallActionAsync<DefaultResponse<IEnumerable<Account>>>(Device, "/accounts/queryAccounts",
                null, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            return response?.Data;
        }

        /// <summary>
        /// Queries all accounts.
        /// </summary>
        /// <param name="query">The queryRequest settings.</param>
        /// <returns>An enumerable which contains all accounts.</returns>
        public IEnumerable<Account> QueryAccounts(ApiQuery query)
        {
            return QueryAccountsAsync(query).Result;
        }

        /// <summary>
        /// Removes accounts stored on the device.
        /// </summary>
        /// <param name="accountIds">The account ids you want to remove.</param>
        /// <returns>True if successfull.</returns>
        public async Task<bool> RemoveAccountsAsync(long[] accountIds)
        {
            var param = new[] { accountIds };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/accounts/removeAccounts",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Removes accounts stored on the device.
        /// </summary>
        /// <param name="accountIds">The account ids you want to remove.</param>
        /// <returns>True if successfull.</returns>
        public bool RemoveAccounts(long[] accountIds)
        {
            return RemoveAccountsAsync(accountIds).Result;
        }

        /// <summary>
        /// Set the enabled status for the accounts
        /// </summary>
        /// <param name="enabled">True if you want to enable the accounts.</param>
        /// <param name="accountIds">The account ids you want to enabled/disable</param>
        /// <returns>True if successful</returns>
        public async Task<bool> SetEnabledStateAsync(bool enabled, long[] accountIds)
        {
            var param = new[] { accountIds };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/accounts/setEnabledState",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Set the enabled status for the accounts
        /// </summary>
        /// <param name="enabled">True if you want to enable the accounts.</param>
        /// <param name="accountIds">The account ids you want to enabled/disable</param>
        /// <returns>True if successful</returns>
        public bool SetEnabledState(bool enabled, long[] accountIds)
        {
            return SetEnabledStateAsync(enabled, accountIds).Result;
        }

        /// <summary>
        /// Updates the username and password for the given account id.
        /// </summary>
        /// <param name="accountId">The id of the account you want to update.</param>
        /// <param name="username">The new username.</param>
        /// <param name="password">The new password.</param>
        /// <returns>True if successful</returns>
        public async Task<bool> UpdateAccountAsync(long accountId, string username, string password)
        {
            var param = new object[] { accountId, username, password };
            var response = await ApiHandler.CallActionAsync<DefaultResponse<bool>>(Device, "/accounts/updateAccount",
                param, JDownloaderHandler.LoginObject, true).ConfigureAwait(false);
            if (response?.Data == null) return false;
            return response.Data;
        }

        /// <summary>
        /// Updates the username and password for the given account id.
        /// </summary>
        /// <param name="accountId">The id of the account you want to update.</param>
        /// <param name="username">The new username.</param>
        /// <param name="password">The new password.</param>
        /// <returns>True if successful</returns>
        public bool UpdateAccount(long accountId, string username, string password)
        {
            return UpdateAccountAsync(accountId, username, password).Result;
        }
    }
}