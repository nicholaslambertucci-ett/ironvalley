using System;
using Ett.Vdt.Extensions;
using Ett.Vdt.NativeApplication.Data;
using Ett.Vdt.NativeApplication.Data.Beacons;
using Ett.Vdt.NativeApplication.Data.Beacons.Payloads;
using Ett.Vdt.NativeApplication.Data.Beacons.Payloads.Extensions;
using Ett.Vdt.NativeApplication.Data.Extensions;
using Ett.Vdt.NativeApplication.Data.Maps;
using Ett.Vdt.NativeApplication.Data.Maps.Payloads;
using Ett.Vdt.NativeApplication.Data.Maps.Payloads.Extensions;
using Ett.Vdt.NativeApplication.Data.Paths;
using Ett.Vdt.NativeApplication.Data.Paths.Payloads;
using Ett.Vdt.NativeApplication.Data.Paths.Payloads.Extensions;
using Ett.Vdt.NativeApplication.Data.Payloads;
using Ett.Vdt.NativeApplication.Data.Pois;
using Ett.Vdt.NativeApplication.Data.Pois.Payloads;
using Ett.Vdt.NativeApplication.Data.Pois.Payloads.Extensions;
using Ett.Vdt.NativeApplication.Data.Sheets;
using Ett.Vdt.NativeApplication.Data.Sheets.Extensions;
using Ett.Vdt.NativeApplication.Data.Sheets.Payloads;
using Ett.Vdt.NativeApplication.Data.Sheets.Payloads.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Ett.Vdt.NativeApplication
{
    public partial class NativeAppController : MonoBehaviour
    {
        public static BasicConfiguration BasicConfiguration { get; private set; }

        public static void RequestBeaconStartScanning(int pathId, UnityAction callback, UnityAction<int> onFoundBeaconIdCallback)
        {
            if (callback != null)
                OnReceiveBeaconScanStarted.AddListener(callback);

            if (onFoundBeaconIdCallback != null)
                OnReceivedFoundBeaconId.AddListener(onFoundBeaconIdCallback);

            SendBeaconStartScanning(pathId);
        }

        public static void RequestBeaconStopScanning(UnityAction callback)
        {
            OnReceivedFoundBeaconId.RemoveAllListeners();

            if (callback != null)
                OnReceiveBeaconScanStopped.AddListener(callback);

            SendBeaconStopScanning();
        }

        public static void RequestCompleteBeaconStartScanning(UnityAction callback, UnityAction<Beacon> onFoundBeaconCallback)
        {
            if (callback != null)
                OnReceiveBeaconScanStarted.AddListener(callback);

            if (onFoundBeaconCallback != null)
                OnReceivedFoundCompleteBeacon.AddListener(onFoundBeaconCallback);

            SendCompleteBeaconStartScanning();
        }

        public static void RequestMapList(MapListQuery query, UnityAction<MapList> callback)
        {
            if (callback != null)
                OnReceivedMapList.AddListener(callback);

            var queryPayload = JsonUtility.ToJson(query.ToPayload());
            SendGetMapList(queryPayload);
        }

        public static void RequestPathList(PathListQuery query, UnityAction<PathList> callback)
        {
            if (callback != null)
                OnReceivedPathList.AddListener(callback);

            var queryPayload = JsonUtility.ToJson(query.ToPayload());
            SendGetPathList(queryPayload);
        }

        public static void RequestPoiDetails(int poiId, UnityAction<PoiDetails> callback)
        {
            if (callback != null)
                OnReceivedPoiDetails.AddListener(callback);

            SendGetPoiDetails(poiId);
        }

        public static void RequestPoiList(PoiListQuery query, UnityAction<PoiList> callback)
        {
            if (callback != null)
                OnReceivedPoiList.AddListener(callback);

            var queryPayload = query.ToPayload();
            var payload = JsonUtility.ToJson(queryPayload);

            SendGetPoiList(payload);
        }

        #region Sheets

        public static void RequestMultipleSheetList(MultipleSheetListQuery query, UnityAction<MultipleSheetList> callback)
        {
            if (callback != null)
                OnReceivedMultipleSheetList.AddListener(callback);

            var payload = query.ToPayload();
            var queryPayload = JsonUtility.ToJson(payload);
            SendGetMultipleSheetList(queryPayload);
        }

        public static void RequestSheetList(SheetListQuery query, UnityAction<SheetList> callback)
        {
            if (callback != null)
                OnReceivedSheetList.AddListener(callback);

            var payload = query.ToPayload();
            var queryPayload = JsonUtility.ToJson(payload);
            SendGetSheetList(queryPayload);
        }


        private static readonly UnityEvent<MultipleSheetList> OnReceivedMultipleSheetList = new MultipleSheetListEvent();
        private static readonly UnityEvent<SheetList> OnReceivedSheetList = new SheetListEvent();

        // ReSharper disable once UnusedMember.Local
        private void ReceiveMultipleSheetList(string payload)
        {
            var multipleSheetListPayload = JsonUtility.FromJson<MultipleSheetListPayload>(payload.CloneWithCharArray());
            var multipleSheetList = multipleSheetListPayload.ToMultipleSheetList();
            OnReceivedMultipleSheetList.Invoke(multipleSheetList);
            OnReceivedMultipleSheetList.RemoveAllListeners();
        }

        // ReSharper disable once UnusedMember.Local
        private void ReceiveSheetList(string payload)
        {
            var sheetListPayload = JsonUtility.FromJson<SheetListPayload>(payload.CloneWithCharArray());
            var sheetList = sheetListPayload.ToSheetList();
            OnReceivedSheetList.Invoke(sheetList);
            OnReceivedSheetList.RemoveAllListeners();
        }

        private class MultipleSheetListEvent : UnityEvent<MultipleSheetList> { }

        private class SheetListEvent : UnityEvent<SheetList> { }

        static partial void SendGetMultipleSheetList(string queryPayload);
        static partial void SendGetSheetList(string queryPayload);

        #endregion


        private static readonly UnityEvent OnReceiveBeaconScanStarted = new UnityEvent();
        private static readonly UnityEvent OnReceiveBeaconScanStopped = new UnityEvent();
        private static readonly UnityEvent<int> OnReceivedFoundBeaconId = new FoundBeaconIdEvent();
        private static readonly UnityEvent<Beacon> OnReceivedFoundCompleteBeacon = new FoundBeaconCompleteEvent();
        private static readonly UnityEvent<MapList> OnReceivedMapList = new MapListEvent();
        private static readonly UnityEvent<PathList> OnReceivedPathList = new PathListEvent();
        private static readonly UnityEvent<PoiDetails> OnReceivedPoiDetails = new PoiDetailsEvent();
        private static readonly UnityEvent<PoiList> OnReceivedPoiList = new PoiListEvent();
        


        #region Native Callback

        /// <summary>
        /// Callback richiamata dal nativo per istruire l'app dell'avvenuto avvio
        /// dello scanner per i beacon.
        /// operazione avviata tramite il metodo <code>RequestBeaconStartScanning</code>
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private void ReceiveBeaconScanStarted()
        {
            OnReceiveBeaconScanStarted.Invoke();
            OnReceiveBeaconScanStarted.RemoveAllListeners();
        }

        /// <summary>
        /// Callback richiamata dal nativo per istruire l'app dell'avvenuta interruzione
        /// dello scanner per i beacon.
        /// operazione avviata tramite il metodo <code>RequestBeaconStopScanning</code>
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private void ReceiveBeaconScanStopped()
        {
            OnReceiveBeaconScanStopped.Invoke();
            OnReceiveBeaconScanStopped.RemoveAllListeners();
        }

        /// <summary>
        /// Callback richiamata dal nativo per istruire l'app sui beacon trovati dallo scanner.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once MemberCanBeMadeStatic.Local
        private void ReceiveFoundBeacon(string payload)
        {
            var beaconId = 0;
            if (!int.TryParse(payload.CloneWithCharArray(), out beaconId))
                return;

            OnReceivedFoundBeaconId.Invoke(beaconId);
        }

        /// <summary>
        /// Callback richiamata dal nativo per istruire l'app sui beacon trovati dallo scanner.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private void ReceiveFoundCompleteBeacon(string payload)
        {
            var beaconPayload = JsonUtility.FromJson<BeaconPayload>(payload.CloneWithCharArray());
            var beacon = beaconPayload.ToBeacon();

            OnReceivedFoundCompleteBeacon.Invoke(beacon);
        }

        // ReSharper disable once UnusedMember.Local
        private void ReceiveMapList(string payload)
        {
            var mapListPayload = JsonUtility.FromJson<MapListPayload>(payload.CloneWithCharArray());
            var mapList = mapListPayload.ToMapLIst();
            OnReceivedMapList.Invoke(mapList);
            OnReceivedMapList.RemoveAllListeners();
        }

        // ReSharper disable once UnusedMember.Local
        private void ReceivePathList(string payload)
        {
            var pathListPayload = JsonUtility.FromJson<PathListPayload>(payload.CloneWithCharArray());
            var pathList = pathListPayload.ToPathList();
            OnReceivedPathList.Invoke(pathList);
            OnReceivedPathList.RemoveAllListeners();
        }


        /// <summary>
        /// Callback richiamata dal nativo quando vengono richiesti i dati di dettaglio
        /// di un poi; 
        /// operazione avviata tramite il metodo <code>RequestPoiDetails</code>
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private void ReceivePoiDetails(string payload)
        {
            var poiDetailsPayload = JsonUtility.FromJson<PoiDetailsPayload>(payload.CloneWithCharArray());
            var poiDetails = poiDetailsPayload.ToPoiDetails();
            OnReceivedPoiDetails.Invoke(poiDetails);
            OnReceivedPoiDetails.RemoveAllListeners();
        }

        /// <summary>
        /// Callback richiamata dal nativo quando viene richiesta la lista di poi;
        /// operazione avviata tramite il metodo <code>RequestPoiList</code>
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private void ReceivePoiList(string payload)
        {
            var poisListPayload = JsonUtility.FromJson<PoiListPayload>(payload.CloneWithCharArray());
            var poisList = poisListPayload.ToGetPoiList();
            OnReceivedPoiList.Invoke(poisList);
            OnReceivedPoiList.RemoveAllListeners();
        }

        

        #endregion


        #region Unity Events

#if !VDT_V_2
        private void Start()
        {
            this.OnStart();
            BasicConfiguration = new BasicConfiguration
            {
                Language = MainController.Language
            };

        }
#endif
        #endregion


        #region Platform Methods

        static partial void SendBeaconStartScanning(int pathId);
        static partial void SendBeaconStopScanning();
        static partial void SendCompleteBeaconStartScanning();
        static partial void SendGetMapList(string queryPayload);
        static partial void SendGetPathList(string queryPayload);
        static partial void SendGetPoiDetails(int poiId);
        
        static partial void SendGetPoiList(string queryPayload);


        partial void OnStart();

        #endregion

        private class FoundBeaconCompleteEvent : UnityEvent<Beacon> { }

        private class FoundBeaconIdEvent : UnityEvent<int> { }

        private class MapListEvent : UnityEvent<MapList> { }

        private class PathListEvent : UnityEvent<PathList> { }

        private class PoiDetailsEvent : UnityEvent<PoiDetails> { }

        private class PoiListEvent : UnityEvent<PoiList> { }

        
    }
}
