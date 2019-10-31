#if UNITY_EDITOR && !UNITY_ANDROID && !UNITY_IPHONE
using Ett.Vdt.Fake;
using UnityEngine;

namespace Ett.Vdt.NativeApplication
{
    public partial class NativeAppController
    {
        public static NativeAppController Controller { get; private set; }

        public static void Editor_ForwardReceivedFoundBeacon(int beaconId)
        {
            Controller.ReceiveFoundBeacon(beaconId.ToString());
        }

        static partial void SendBeaconStartScanning(int pathId)
        {
            Debug.Log("Starting fake beacon scan");
            FakeNativeApp.Instance.CallFakeNative("onBeaconStartScan", pathId);
        }

        static partial void SendBeaconStopScanning()
        {
            Debug.Log("Stopping fake beacon scan");
            FakeNativeApp.Instance.CallFakeNative("onBeaconStopScan");
        }

        static partial void SendCompleteBeaconStartScanning()
        {
            Debug.Log("Starting fake complete beacon scan");
            FakeNativeApp.Instance.CallFakeNative("onBeaconCompleteStartScan");
        }

        static partial void SendGetMapList(string queryPayload)
        {
            Debug.Log("Calling onGetMapList");
            FakeNativeApp.Instance.CallFakeNative("onGetMapList", queryPayload);
        }

        static partial void SendGetPathList(string queryPayload)
        {
            Debug.Log("Calling onGetPathList");
            FakeNativeApp.Instance.CallFakeNative("onGetPathList", queryPayload);
        }

        static partial void SendGetPoiDetails(int poiId)
        {
            Debug.Log("Calling onGetPoiDetails");
            FakeNativeApp.Instance.CallFakeNative("onGetPoiDetails", poiId);
        }

        static partial void SendGetPoiList(string queryPayload)
        {
            Debug.Log("Calling onGetPoiList");
            FakeNativeApp.Instance.CallFakeNative("onGetPoiList", queryPayload);
        }

        #region Sheets

        static partial void SendGetMultipleSheetList(string queryPayload)
        {
            Debug.Log("Calling onGetMultipleSheetList");
            FakeNativeApp.Instance.CallFakeNative("onGetMultipleSheetList", queryPayload);
        }

        static partial void SendGetSheetList(string queryPayload)
        {
            Debug.Log("Calling onGetSheetList");
            FakeNativeApp.Instance.CallFakeNative("onGetSheetList", queryPayload);
        }

        #endregion

        partial void OnStart()
        {
            Controller = this;
        }
    }
}
#endif
