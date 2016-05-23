using Newtonsoft.Json;
using Shared.Models;
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
        public string GetGpsCoordinates(YaLocatorDataModel model)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            var req = (HttpWebRequest)WebRequest.Create("http://api.lbs.yandex.net/geolocation");

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Headers[HttpRequestHeader.AcceptEncoding] = "identity";

            var json = JsonConvert.SerializeObject(model);
            var sentData = System.Text.Encoding.UTF8.GetBytes(string.Format("json={0}", json));
          
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
            }

            return System.Text.Encoding.UTF8.GetString(buf);
        }
    }
}
