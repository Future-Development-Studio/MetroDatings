using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class YaLocatorDataModel
    {
        [JsonProperty("common")]
        public Common Common { get; set; }
        [JsonProperty("gsm_cells")]
        public List<GsmCell> GsmCells { get; set; }
        [JsonProperty("wifi_networks")]
        public List<WiFiNetwork> WiFiNetworks { get; set; }
        [JsonProperty("ip")]
        public IpAddress IpAddress { get; set; }
    }
    public class Common
    {
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }
    }
    public class GsmCell
    {
        [JsonProperty("countrycode")]
        public int CountryCode { get; set; }
        [JsonProperty("operatorid")]
        public int OperatorId { get; set; }
        [JsonProperty("cellid")]
        public int CellId { get; set; }
        [JsonProperty("lac")]
        public int Lac { get; set; }
        [JsonProperty("signal_strength")]
        public int SignalStrengthGsm { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
    }
    public class WiFiNetwork
    {
        [JsonProperty("mac")]
        public string Mac { get; set; }
        [JsonProperty("signal_strength")]
        public int SignalStrengthWiFi { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
    }
    public class IpAddress
    {
        [JsonProperty("address_v4")]
        public string Ip { get; set; }
    }
}
