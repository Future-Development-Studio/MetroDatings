
using Android.App;
using Android.Content;
using Android.Telephony;
using Android.Telephony.Gsm;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroDatings.Android
{
    public class GsmService : Activity
    {
        public GsmData GetGsmData()
        {
            var data = new GsmData();
            TelephonyManager telephonyManager = (TelephonyManager)GetSystemService(Context.TelephonyService);
            GsmCellLocation cellLocation = (GsmCellLocation)telephonyManager.CellLocation;

            if (cellLocation != null)
            {
                data.Cid = cellLocation.Cid;
                data.Lac = cellLocation.Lac;
            }

            return data;         
        }
    }
}
