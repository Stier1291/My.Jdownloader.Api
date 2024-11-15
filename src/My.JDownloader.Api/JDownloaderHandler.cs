using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.Devices.Response;
using My.JDownloader.Api.Models.Login;

namespace My.JDownloader.Api
{
    public class JDownloaderHandler
    {
        public bool IsConnected { get; set; }

        internal static LoginObject LoginObject;

        private byte[] _loginSecret;
        private byte[] _deviceSecret;


        private readonly JDownloaderApiHandler _apiHandler = new JDownloaderApiHandler();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appkey">The name of the app. Should be unique!</param>
        public JDownloaderHandler(string appkey)
        {
            Utils.AppKey = appkey;
        }

        /// <summary>
        /// </summary>
        /// <param name="email">Your email of your my.jdownloader.org account.</param>
        /// <param name="password">Your password of your my.jdownloader.org account.</param>
        /// <param name="appKey">The name of the app. Should be unique!</param>
        public JDownloaderHandler(string email, string password, string appKey)
        {
            Connect(email, password);
            Utils.AppKey = appKey;
        }

        #region "Connection methods"

        /// <summary>
        /// Fires a connection request to the api.
        /// </summary>
        /// <param name="email">Email of the User</param>
        /// <param name="password">Password of the User</param>
        /// <returns>Return if the connection was succesful</returns>
        public async Task<bool> ConnectAsync(string email, string password)
        {
            //Calculating the Login and Device secret
            _loginSecret = Utils.GetSecret(email, password, Utils.ServerDomain);
            _deviceSecret = Utils.GetSecret(email, password, Utils.DeviceDomain);

            //Creating the queryRequest for the connection request
            string connectQueryUrl =
                $"/my/connect?email={HttpUtility.UrlEncode(email)}&appkey={HttpUtility.UrlEncode(Utils.AppKey)}";

            //Calling the queryRequest
            var response = await _apiHandler.CallServerAsync<LoginObject>(connectQueryUrl, _loginSecret).ConfigureAwait(false);

            //If the response is null the connection was not successfull
            if (response == null)
                return false;

            //Else we are saving the response which contains the SessionToken, RegainToken and the RequestId
            LoginObject = response;
            LoginObject.Email = email;
            LoginObject.Password = password;
            LoginObject.ServerEncryptionToken = Utils.UpdateEncryptionToken(_loginSecret, LoginObject.SessionToken);
            LoginObject.DeviceEncryptionToken = Utils.UpdateEncryptionToken(_deviceSecret, LoginObject.SessionToken);
            IsConnected = true;
            return true;
        }

        /// <summary>
        /// Fires a connection request to the api.
        /// </summary>
        /// <param name="email">Email of the User</param>
        /// <param name="password">Password of the User</param>
        /// <returns>Return if the connection was succesful</returns>
        public bool Connect(string email, string password)
        {
            return ConnectAsync(email, password).Result;
        }

        /// <summary>
        /// Tries to reconnect your client to the api.
        /// </summary>
        /// <returns>True if successful else false</returns>
        public async Task<bool> ReconnectAsync()
        {
            string query = $"/my/reconnect?appkey{HttpUtility.UrlEncode(Utils.AppKey)}&sessiontoken={HttpUtility.UrlEncode(LoginObject.SessionToken)}&regaintoken={HttpUtility.UrlEncode(LoginObject.RegainToken)}";
            var response = await _apiHandler.CallServerAsync<LoginObject>(query, LoginObject.ServerEncryptionToken).ConfigureAwait(false);
            if (response == null)
                return false;

            LoginObject = response;
            LoginObject.ServerEncryptionToken = Utils.UpdateEncryptionToken(_loginSecret, LoginObject.SessionToken);
            LoginObject.DeviceEncryptionToken = Utils.UpdateEncryptionToken(_deviceSecret, LoginObject.SessionToken);
            IsConnected = true;
            return IsConnected;
        }

        /// <summary>
        /// Tries to reconnect your client to the api.
        /// </summary>
        /// <returns>True if successful else false</returns>
        public bool Reconnect()
        {
            return ReconnectAsync().Result;
        }

        /// <summary>
        /// Disconnects the your client from the api
        /// </summary>
        /// <returns>True if successful else false</returns>
        public async Task<bool> DisconnectAsync()
        {
            string query = $"/my/disconnect?sessiontoken={HttpUtility.UrlEncode(LoginObject.SessionToken)}";
            var response = await _apiHandler.CallServerAsync<object>(query, LoginObject.ServerEncryptionToken).ConfigureAwait(false);
            if (response == null)
                return false;

            LoginObject = null;
            return true;
        }

        /// <summary>
        /// Disconnects the your client from the api
        /// </summary>
        /// <returns>True if successful else false</returns>
        public bool Disconnect()
        {
            return DisconnectAsync().Result;
        }
        #endregion


        /// <summary>
        /// Lists all Devices which are currently connected to your my.jdownloader.org account.
        /// </summary>
        /// <returns>Returns an enumerable of your currently connected devices.</returns>
        public async Task<IEnumerable<Device>> GetDevicesAsync()
        {
            List<Device> devices = new List<Device>();
            string query = $"/my/listdevices?sessiontoken={HttpUtility.UrlEncode(LoginObject.SessionToken)}";
            var response = await _apiHandler.CallServerAsync<DeviceJsonResponse>(query, LoginObject.ServerEncryptionToken).ConfigureAwait(false);
            if (response == null)
                return devices;

            foreach (Device device in response.Devices)
            {
                devices.Add(device);
            }

            return devices;
        }

        /// <summary>
        /// Lists all Devices which are currently connected to your my.jdownloader.org account.
        /// </summary>
        /// <returns>Returns an enumerable of your currently connected devices.</returns>
        public IEnumerable<Device> GetDevices()
        {
            return GetDevicesAsync().Result;
        }

        /// <summary>
        /// Creates an instance of the DeviceHandler class. 
        /// This is neccessary to call methods!
        /// </summary>
        /// <param name="device">The device you want to call the methods on.</param>
        /// <returns>An deviceHandler instance.</returns>
        public async Task<DeviceHandler> GetDeviceHandlerAsync(Device device, bool useJdownloaderApi = false)
        {
            if (IsConnected)
            {
                //TODO: Make it possible to directly connect to the jdownloader client. If it's not working use the relay server.
                //var tmp = _apiHandler.CallAction<DefaultReturnObject>(device, "/device/getDirectConnectionInfos",
                //    null, LoginObject, true);
                return await DeviceHandler.CreateAsync(device, _apiHandler, LoginObject, useJdownloaderApi).ConfigureAwait(false);
            }
            return null;
        }

        /// <summary>
        /// Creates an instance of the DeviceHandler class. 
        /// This is neccessary to call methods!
        /// </summary>
        /// <param name="device">The device you want to call the methods on.</param>
        /// <returns>An deviceHandler instance.</returns>
        public DeviceHandler GetDeviceHandler(Device device, bool useJdownloaderApi = false)
        {
            if (IsConnected)
            {
                //TODO: Make it possible to directly connect to the jdownloader client. If it's not working use the relay server.
                //var tmp = _apiHandler.CallAction<DefaultReturnObject>(device, "/device/getDirectConnectionInfos",
                //    null, LoginObject, true);
                return DeviceHandler.Create(device, _apiHandler, LoginObject, useJdownloaderApi);
            }
            return null;
        }
    }
}