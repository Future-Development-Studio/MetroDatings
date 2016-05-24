using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Telephony;
using Shared.Models;
using Android.Telephony.Gsm;
using Android.Net;
using Android.Net.Wifi;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Shared.Yandex.Locator;
using Shared.Interfaces;

namespace MetroDatings.Android
{
    [Activity(Label = "CodeApp", MainLauncher = true, Icon = "@drawable/icon")]

    public class MainActivity : Activity, IGpsSentData
    {
        private static string apiKey = @"ABcDO1cBAAAA-HrnawIAbO_EtcFggTbPPjqnBPjslF_UngIAAAAAAAAAAACl8RS-FHrb_CxnfwmIqWJTFdR6OQ==";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            TextView cidLabel = FindViewById<TextView>(Resource.Id.cid);
            TextView lacLabel = FindViewById<TextView>(Resource.Id.lac);
            TextView macLabel = FindViewById<TextView>(Resource.Id.mac);
            TextView ipLabel = FindViewById<TextView>(Resource.Id.ip);
            TextView gpsLabel = FindViewById<TextView>(Resource.Id.gps);

            var gsmData = GetGsmData();
            var mac = GetMacAdress();
            var ip = GetIpAddress();

            var gpsService = new GpsService();
            var initialData = new YaLocatorDataModel()
            {
                Common = new Common()
                {
                    Version = "1.0",
                    ApiKey = apiKey
                },
                GsmCells = gsmData == null ? null : new List<GsmCell>()
                  {
                      new GsmCell()
                      {
                          CountryCode = 250,
                          OperatorId = 01,
                          CellId = gsmData.Cid,
                          Lac = gsmData.Lac,
                          SignalStrengthGsm = -45,
                          Age = 1000
                      }
                  },
                WiFiNetworks = mac == null ? null : new List<WiFiNetwork>()
                {
                    new WiFiNetwork()
                    {
                        SignalStrengthWiFi = -90,
                        Age = 1000,
                        Mac = mac
                    }
                },
                IpAddress = new IpAddress()
                {
                    Ip = ip
                }

                //WORKING TEST DATA FOR PC
                //CellId = 20952,
                //Lac = 561,
                //Mac = "382C4A8E0204",
                //Ip = "10.205.167.167"
            };

            var gpsData = gpsService.GetGpsCoordinates(initialData);

            cidLabel.Text = string.Format("Cid: {0}", gsmData != null ? gsmData.Cid.ToString() : "No Cid");
            lacLabel.Text = string.Format("Lac: {0}", gsmData != null ? gsmData.Lac.ToString() : "No Lac");
            macLabel.Text = string.Format("Mac: {0}", mac ?? "No mac");
            ipLabel.Text = string.Format("Ip: {0}", ip ?? "No Ip");
            gpsLabel.Text = string.Format("GpsData: {0}", gpsData);
        }

        public GsmData GetGsmData()
        {
            var data = new GsmData();
            TelephonyManager telephonyManager = (TelephonyManager)GetSystemService(TelephonyService);
            GsmCellLocation cellLocation = (GsmCellLocation)telephonyManager.CellLocation;
            if (cellLocation == null)
                data = null;
            else
            {
                data.Cid = cellLocation.Cid;
                data.Lac = cellLocation.Lac;
            }

            return data;
        }

        public string GetMacAdress()
        {
            string mac = null;
            ConnectivityManager cm = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo networkInfo = (NetworkInfo)cm.ActiveNetworkInfo;

            if (networkInfo.IsConnected)
            {
                WifiManager wifiManager = (WifiManager)GetSystemService(WifiService);
                WifiInfo connectionInfo = wifiManager.ConnectionInfo;
                if (connectionInfo != null)
                {
                    mac = connectionInfo.BSSID;
                }
            }

            return mac;
        }

        public string GetIpAddress()
        {
            string ipAddress = null;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip.ToString();
                }
            }
            return ipAddress;
        }
    }
}

