﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.Devices.Response;
using My.JDownloader.Api.Models.Login;
using My.JDownloader.Api.Namespaces;
using Newtonsoft.Json.Linq;
using Extensions = My.JDownloader.Api.Namespaces.Extensions;

namespace My.JDownloader.Api
{
    public class DeviceHandler
    {
        internal static async Task<DeviceHandler> CreateAsync(Device device, JDownloaderApiHandler apiHandler, 
            LoginObject loginObject, bool useJdownloaderApi)
        {
            var deviceHandler = new DeviceHandler(device, apiHandler, loginObject);
            await deviceHandler.DirectConnectAsync(useJdownloaderApi).ConfigureAwait(false);
            return deviceHandler;
        }

        internal static DeviceHandler Create(Device device, JDownloaderApiHandler apiHandler,
            LoginObject loginObject, bool useJdownloaderApi)
        {
            var deviceHandler = new DeviceHandler(device, apiHandler, loginObject);
            deviceHandler.DirectConnectAsync(useJdownloaderApi).Wait();
            return deviceHandler;
        }

        private readonly Device _device;
        private readonly JDownloaderApiHandler _apiHandler;

        private LoginObject _loginObject;

        private byte[] _loginSecret;
        private byte[] _deviceSecret;

        public bool IsConnected;

        public Accounts Accounts;
        public AccountsV2 AccountsV2;
        public Captcha Captcha;
        public CaptchaForward CaptchaForward;
        public Config Config;
        public Dialogs Dialogs;
        public DownloadController DownloadController;
        public DownloadsV2 DownloadsV2;
        public Extensions Extensions;
        public Extraction Extraction;
        public LinkCrawler LinkCrawler;
        public LinkGrabberV2 LinkgrabberV2;
        public Update Update;
        public Jd Jd;
        public Namespaces.System System;

        private DeviceHandler(Device device, JDownloaderApiHandler apiHandler, LoginObject loginObject)
        {
            _device = device;
            _apiHandler = apiHandler;
            _loginObject = loginObject;

            Accounts = new Accounts(_apiHandler, _device);
            AccountsV2 = new AccountsV2(_apiHandler, _device);
            Captcha = new Captcha(_apiHandler, _device);
            CaptchaForward = new CaptchaForward(_apiHandler, _device);
            Config = new Config(_apiHandler, _device);
            Dialogs = new Dialogs(_apiHandler, _device);
            DownloadController = new DownloadController(_apiHandler, _device);
            DownloadsV2 = new DownloadsV2(_apiHandler, _device);
            Extensions = new Extensions(_apiHandler, _device);
            Extraction = new Extraction(_apiHandler, _device);
            LinkCrawler = new LinkCrawler(_apiHandler, _device);
            LinkgrabberV2 = new LinkGrabberV2(_apiHandler, _device);
            Update = new Update(_apiHandler, _device);
            Jd = new Jd(_apiHandler, _device);
            System = new Namespaces.System(_apiHandler, _device);
        }

        /// <summary>
        /// Tries to directly connect to the JDownloader Client.
        /// </summary>
        private async Task DirectConnectAsync(bool useJdownloaderApi)
        {
            bool connected = false;
            if (useJdownloaderApi)
            {
                await ConnectAsync("http://api.jdownloader.org").ConfigureAwait(false);
                return;
            }
            foreach (var conInfos in await GetDirectConnectionInfos().ConfigureAwait(false))
            {
                if (await ConnectAsync(string.Concat("http://", conInfos.Ip, ":", conInfos.Port)).ConfigureAwait(false))
                {
                    connected = true;
                    break;
                }
            }
            if (connected == false)
                await ConnectAsync("http://api.jdownloader.org").ConfigureAwait(false);
        }


        private async Task<bool> ConnectAsync(string apiUrl)
        {
            //Calculating the Login and Device secret
            _loginSecret = Utils.GetSecret(_loginObject.Email, _loginObject.Password, Utils.ServerDomain);
            _deviceSecret = Utils.GetSecret(_loginObject.Email, _loginObject.Password, Utils.DeviceDomain);

            //Creating the queryRequest for the connection request
            string connectQueryUrl =
                $"/my/connect?email={HttpUtility.UrlEncode(_loginObject.Email)}&appkey={HttpUtility.UrlEncode(Utils.AppKey)}";
            _apiHandler.SetApiUrl(apiUrl);
            //Calling the queryRequest
            var response = await _apiHandler.CallServerAsync<LoginObject>(connectQueryUrl, _loginSecret).ConfigureAwait(false);

            //If the response is null the connection was not successful
            if (response == null)
                return false;

            response.Email = _loginObject.Email;
            response.Password = _loginObject.Password;

            //Else we are saving the response which contains the SessionToken, RegainToken and the RequestId
            _loginObject = response;
            _loginObject.ServerEncryptionToken = Utils.UpdateEncryptionToken(_loginSecret, _loginObject.SessionToken);
            _loginObject.DeviceEncryptionToken = Utils.UpdateEncryptionToken(_deviceSecret, _loginObject.SessionToken);
            IsConnected = true;
            return true;
        }

        private async Task<List<DeviceConnectionInfo>> GetDirectConnectionInfos()
        {
            var tmp = await _apiHandler.CallActionAsync<DefaultResponse<object>>(_device, "/device/getDirectConnectionInfos",
                null, _loginObject, true).ConfigureAwait(false);
            if (tmp.Data == null || string.IsNullOrEmpty(tmp.Data.ToString()))
                return new List<DeviceConnectionInfo>();

            var jobj = (JObject)tmp.Data;
            var deviceConInfos = jobj.ToObject<DeviceConnectionInfoResponse>();

            return deviceConInfos.Infos;
        }
    }
}
