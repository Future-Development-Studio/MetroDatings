using NativeWifi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class SentDataService
    {
        //For PC
        public List<string> GetHotspotMacs()
        {
            var macs = new List<string>();
            var wlanClient = new WlanClient();
            foreach (WlanClient.WlanInterface wlanInterface in wlanClient.Interfaces)
            {
                Wlan.WlanBssEntry[] wlanBssEntries = wlanInterface.GetNetworkBssList();
                foreach (Wlan.WlanBssEntry wlanBssEntry in wlanBssEntries)
                {
                    byte[] macAddr = wlanBssEntry.dot11Bssid;
                    var macAddrLen = (uint)macAddr.Length;
                    var str = new string[(int)macAddrLen];
                    for (int i = 0; i < macAddrLen; i++)
                    {
                        str[i] = macAddr[i].ToString("x2");
                    }
                    macs.Add(string.Join("", str));
                  
                }
            }
            return macs;
        }
    }
}
