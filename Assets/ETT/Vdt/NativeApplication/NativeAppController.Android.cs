#if UNITY_ANDROID //&& !UNITY_EDITOR
using System;
using UnityEngine;


namespace Ett.Vdt.NativeApplication
{
    public partial class NativeAppController
    {
        static partial void SendBeaconStartScanning(int pathId)
        {
            Debug.Log("Calling onBeaconStartScan");
            try
            {
                MsgController.Jo.Call("onBeaconStartScan", pathId);
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
                MsgController.Jo.Call("onBeaconStopScan");
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("An error occurred while calling onBeaconStopScan {0}", ex.Message);
            }
        }

        static partial void SendGetMapList(string queryPayload)
        {
            Debug.Log("Calling onGetPathList");
            try
            {
                MsgController.Jo.Call("onGetMapList", queryPayload);
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("An error occurred while calling onBeaconStopScan {0}", ex.Message);
            }
        }

        static partial void SendGetMultipleSheetList(string queryPayload)
        {
            Debug.Log("Calling onGetMultipleSheetList");
            try
            {
                MsgController.Jo.Call("onGetMultipleSheetList", queryPayload);
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("An error occurred while calling onGetMultipleSheetList {0}", ex.Message);
            }
        }

        static partial void SendGetPathList(string queryPayload)
        {
            Debug.Log("Calling onGetPathList");
            try
            {
                MsgController.Jo.Call("onGetPathList", queryPayload);
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
                MsgController.Jo.Call("onGetPoiDetails", poiId);
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("An error occurred while calling onGetPoiDetails ({0}): {1}", poiId, ex.Message);
            }
        }

        static partial void SendGetSheetList(string queryPayload)
        {
            Debug.Log("Calling onGetSheetList");
            try
            {
                MsgController.Jo.Call("onGetSheetList", queryPayload);
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("An error occurred while calling onGetSheetList: {0}", ex.Message);
            }
        }

        static partial void SendGetPoiList(string queryPayload)
        {
            Debug.Log("Calling onGetPoiList");
            try
            {
                MsgController.Jo.Call("onGetPoiList", queryPayload);
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