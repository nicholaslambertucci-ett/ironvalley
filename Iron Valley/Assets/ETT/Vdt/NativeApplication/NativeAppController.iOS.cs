#if UNITY_IPHONE //&& !UNITY_EDITOR
using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Ett.Vdt.NativeApplication
{
    public partial class NativeAppController
    {
        [DllImport("__Internal")]
        private static extern void onBeaconStartScan();

        [DllImport("__Internal")]
        private static extern void onBeaconStopScan();

        [DllImport("__Internal")]
        private static extern void onGetPathList(string queryPayload);

        [DllImport("__Internal")]
        private static extern void onGetPoiDetails(int poiId);

        [DllImport("__Internal")]
        private static extern void onGetPoiList(string queryPayload);

        static partial void SendBeaconStartScanning()
        {
            Debug.Log("Calling onBeaconStartScan");
            try
            {
                onBeaconStartScan();
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("An error occurred while calling onBeaconStartScan {0}", ex.Message);
            }
        }

        static partial void SendBeaconStopScanning()
        {
            Debug.Log("Calling onBeaconStopScan");
            try
            {
                onBeaconStopScan();
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("An error occurred while calling onBeaconStartScan {0}", ex.Message);
            }
        }

        static partial void SendGetPathList(string queryPayload)
        {
            Debug.Log("Calling onGetPathList");
            try
            {
                onGetPathList(queryPayload);
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("An error occurred while calling onBeaconStopScan {0}", ex.Message);
            }
        }

        static partial void SendGetPoiDetails(int poiId)
        {
            Debug.Log("Calling onGetPoiDetails");
            try
            {
                onGetPoiDetails(poiId);
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("An error occurred while calling onGetPoiDetails ({0}): {1}", poiId, ex.Message);
            }
        }

        static partial void SendGetPoiList(string queryPayload)
        {
            Debug.Log("Calling onGetPoiList");
            try
            {
                onGetPoiList(queryPayload);
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("An error occurred while calling onGetPoiList: {0}", ex.Message);
            }
        }

        partial void OnStart()
        {
            //this.DeserializePayload();
        }
    }
}
#endif