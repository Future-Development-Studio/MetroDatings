using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shared.Yandex.Locator
{
    public class GpsService
    {
        private static dynamic YaLocator = new
        {
            Version = "1.0",
            ApiKey = @"ABcDO1cBAAAA-HrnawIAbO_EtcFggTbPPjqnBPjslF_UngIAAAAAAAAAAACl8RS-FHrb_CxnfwmIqWJTFdR6OQ==",
            CountryCode = 250,
            OperatorId = 99,//Билайн; надо определять оператора
            CellId = 20952,
            Lac = 561,
            SignalStrengthGsm = -45,
            SignalStrengthWiFi = -90,
            Age = 1000,
            Mac = "382C4A8E0204",
            IP = "192.168.1.1"
        };
        public void GetGpsCoordinates()
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            var req = (HttpWebRequest)WebRequest.Create("http://api.lbs.yandex.net/geolocation");

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Headers[HttpRequestHeader.AcceptEncoding] = "identity";

            //выпилить xml и создать json запрос
            var xDoc = new XDocument(
              new XElement("ya_lbs_request",
                new XElement("common",
                  new XElement("version", YaLocator.Version),
                  new XElement("api_key", YaLocator.ApiKey)),
                new XElement("gsm_cells",
                  new XElement("countrycode", YaLocator.CountryCode),
                  new XElement("operatorid", YaLocator.OperatorId),
                  new XElement("cellid", YaLocator.CellId),
                  new XElement("lac", YaLocator.Lac),
                  new XElement("signal_strength", YaLocator.SignalStrengthGsm),
                  new XElement("age", YaLocator.Age)),
                new XElement("wifi_networks",
                  new XElement("network",
                    new XElement("mac", YaLocator.Mac),
                    new XElement("signal_strength", YaLocator.SignalStrengthWiFi))),
                new XElement("ip",
                  new XElement("address_v4", YaLocator.IP))));

            var sentData = System.Text.Encoding.UTF8.GetBytes(string.Format("xml={0}", xDoc.ToString()));
            req.ContentLength = sentData.Length;

            using (var sendStream = req.GetRequestStream())
            {
                sendStream.Write(sentData, 0, sentData.Length);
            }

            var response = (HttpWebResponse)req.GetResponse();
            var buf = new byte[response.ContentLength];

            using (var respStream = response.GetResponseStream())
            {
                respStream.Read(buf, 0, buf.Length);
               // Console.WriteLine(System.Text.Encoding.UTF8.GetString(buf));
            }


        }
    }
}
