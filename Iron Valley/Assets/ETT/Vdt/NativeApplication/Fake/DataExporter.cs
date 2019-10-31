using System;
using System.Collections.Generic;
using System.IO;
using Ett.Vdt.NativeApplication.Data.Maps;
using Ett.Vdt.NativeApplication.Data.Maps.Payloads.Extensions;
using Ett.Vdt.NativeApplication.Data.Paths;
using Ett.Vdt.NativeApplication.Data.Paths.Payloads.Extensions;
using Ett.Vdt.NativeApplication.Data.Pois;
using Ett.Vdt.NativeApplication.Data.Pois.Payloads.Extensions;
using Ett.Vdt.NativeApplication.Data.Sheets;
using Ett.Vdt.NativeApplication.Data.Sheets.Extensions;
using Ett.Vdt.NativeApplication.Data.Sheets.Payloads.Extensions;
using Ett.Vdt.NativeApplication.Data.Types;
using UnityEngine;
using UnityEngine.Events;

namespace Ett.Vdt.NativeApplication.Fake
{
    public static class DataExporter
    {
        public static void StartExport(UnityAction onExportCompletedCallback)
        {
            PathDataExporter.ExportData(onExportCompletedCallback);
        }


        private static class PathDataExporter
        {

            public static void ExportData(UnityAction completedCallback)
            {
                _completedCallback = completedCallback;
                Debug.Log("Request path list");
                NativeAppController.RequestPathList(PathListQuery.Empty, OnListReceived);
            }


            private static int _pathsIdx;
            private static PathList _list;
            private static UnityAction _completedCallback;


            private static void OnListReceived(PathList list)
            {
                Debug.Log("path list received");
                var directory = System.IO.Path.Combine(Application.persistentDataPath, "export");
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                var file = System.IO.Path.Combine(directory, "pathlist.json");
                Debug.LogFormat("Saving path list to {0}", file);
                File.WriteAllText(file, JsonUtility.ToJson(list.ToPayload()));

                _list = list;
                _pathsIdx = 0;
                ExportPathData();
            }

            private static void ExportPathData()
            {
                Debug.LogFormat("ExportPathData idx: {0}", _pathsIdx);
                if (_list.Paths == null || _pathsIdx >= (_list.Paths?.Length ?? 0))
                {
                    Debug.LogFormat("ExportPathData Completed: path list is null or idx ({0}) is greather or equal to list length ({1})", _pathsIdx, _list.Paths?.Length);
                    _completedCallback?.Invoke();
                    return;
                }

                Debug.LogFormat("Exporting maps entry point for path {0}", _list.Paths[_pathsIdx].Id);

                MapsDataExporter.ExportMapsEntryPointForPath(
                    _list.Paths[_pathsIdx].Id,
                    () =>
                    {
                        Debug.LogFormat("Exporting maps entry point for path {0}", _list.Paths[_pathsIdx].Id);
                        SheetsDataExporter.ExportEntryPointForPath(
                            _list.Paths[_pathsIdx].Id,
                            () => PoisDataExorter.ExportForPath(_list.Paths[_pathsIdx].Id,
                                () =>
                                {
                                    _pathsIdx++;
                                    ExportPathData();
                                })
                        );
                    });
            }
        }

        private static class MapsDataExporter
        {
            public static void ExportMapsEntryPointForPath(int pathId, UnityAction completedCallback)
            {
                _pathId = pathId;
                _completedCallback = completedCallback;
                Debug.LogFormat("Request map list for path:{0}", pathId);
                NativeAppController.RequestMapList(MapListQuery.ForPathEntryPoint(pathId), OnReceivedList);
            }

            private static int _pathId;
            private static UnityAction _completedCallback;
            private static readonly List<MapItem> _mapItems = new List<MapItem>();
            private static int _mapItemIdx;

            private static void ExportMapItemInfo()
            {
                Debug.LogFormat("ExportMapItemInfo idx: {0} for path {1}", _mapItemIdx, _pathId);

                if (_mapItemIdx >= _mapItems.Count)
                {
                    Debug.LogFormat("ExportMapItemInfo for path {0} Completed: idx ({1}) is greather or equal to list length ({2})", _pathId, _mapItemIdx, _mapItems.Count);
                    _completedCallback?.Invoke();
                    return;
                }

                var item = _mapItems[_mapItemIdx];
                switch (item.ActivationType)
                {
                    case TypeOut.Unknown:
                        Debug.LogWarningFormat("ExportMapItemInfo idx: {0} for path {1}: item {2} ActivationType is Unknown", _mapItemIdx, _pathId, item.Id);
                        break;
                    case TypeOut.Galleries:
                        Debug.LogFormat("ExportMapItemInfo idx: {0} for path {1}: item {2} ActivationType is Galleries - nothing to do", _mapItemIdx, _pathId, item.Id);
                        break;
                    case TypeOut.Map:
                        Debug.LogFormat("ExportMapItemInfo idx: {0} for path {1}: item {2} ActivationType is Map - nothing to do", _mapItemIdx, _pathId, item.Id);
                        break;
                    case TypeOut.PikkartArScene:
                        Debug.LogFormat("ExportMapItemInfo idx: {0} for path {1}: item {2} ActivationType is PikkartArScene - nothing to do", _mapItemIdx, _pathId, item.Id);
                        break;
                    case TypeOut.Poi:
                        Debug.LogFormat("ExportMapItemInfo idx: {0} for path {1}: item {2} ActivationType is Poi - nothing to do", _mapItemIdx, _pathId, item.Id);
                        break;
                    case TypeOut.Sheet:
                        Debug.LogFormat("ExportMapItemInfo idx: {0} for path {1}: item {2} ActivationType is Sheet - exporting sheet for poi ({3})", 
                            _mapItemIdx, _pathId, item.Id, item.PoiId.Value);
                        SheetsDataExporter.ExportForPoi(_pathId, item.PoiId.Value, item.Id, TypeIn.MapItem,
                            () =>
                            {
                                Debug.LogFormat("ExportMapItemInfo idx: {0} for path {1}: item {2} ActivationType is Sheet - sheet for poi ({3}) exported, move to next map item",
                                    _mapItemIdx, _pathId, item.Id, item.PoiId.Value);
                                _mapItemIdx++;
                                ExportMapItemInfo();
                            });
                        return;
                    case TypeOut.UnitySceneActivator:
                        Debug.LogFormat("ExportMapItemInfo idx: {0} for path {1}: item {2} ActivationType is UnitySceneActivator - nothing to do", _mapItemIdx, _pathId, item.Id);
                        break;
                    case TypeOut.Vr:
                        Debug.LogFormat("ExportMapItemInfo idx: {0} for path {1}: item {2} ActivationType is Poi - nothing to do", _mapItemIdx, _pathId, item.Id);
                        break;
                    case null:
                        Debug.LogWarningFormat("ExportMapItemInfo idx: {0} for path {1}: item {2} ActivationType is null ?!?", _mapItemIdx, _pathId, item.Id);
                        break;
                    default:
                        Debug.LogWarningFormat("ExportMapItemInfo idx: {0} for path {1}: unexpected item {2} ActivationType {3}", _mapItemIdx, _pathId, item.Id, item.ActivationType);
                        break;
                }

                _mapItemIdx++;
                ExportMapItemInfo();
            }

            private static void OnReceivedList(MapList list)
            {
                Debug.LogFormat("Map list for path {0} received", _pathId);
                var directory = System.IO.Path.Combine(Application.persistentDataPath, "export");
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                var file = System.IO.Path.Combine(directory, $"path_{_pathId}_maplist.json");

                Debug.LogFormat("Saving map list for path {0} to {1}", _pathId, file);

                File.WriteAllText(file, JsonUtility.ToJson(list.ToPayload()));

                _mapItemIdx = 0;
                _mapItems.Clear();
                if(list.Maps == null)
                    Debug.LogFormat("Map list for path {0} is null", _pathId);

                if (list.Maps != null)
                    foreach (var map in list.Maps)
                    {
                        if (map.Items == null)
                        {
                            Debug.LogFormat("Items on map {0} for path {1} is null", map.Id, _pathId);
                            continue;
                        }

                        foreach (var item in map.Items)
                        {
                            if (item.ActivationType == null || item.ActivationId == null || item.PoiId == null)
                            {
                                
                                Debug.LogFormat("Item {0} on map {1} for path {2} has some activation field null: ActivationType ({3}), ActivationId ({4}) PoiId ({5})", 
                                    item.Id, map.Id, _pathId,
                                    item.ActivationType,
                                    item.ActivationId,
                                    item.PoiId);
                                continue;
                            }

                            _mapItems.Add(item);
                        }
                    }


                Debug.LogFormat("Selected {1} map items for path {0}", _pathId, _mapItems.Count);
                ExportMapItemInfo();
            }


        }

        private static class SheetsDataExporter
        {
            public static void ExportEntryPointForPath(int pathId, UnityAction completedCallback)
            {
                _pathId = pathId;
                _completedCallback = completedCallback;
                NativeAppController.RequestSheetList(SheetListQuery.ForPathEntryPoint(pathId), OnReceivedPathEntryPointList);
            }

            public static void ExportForPoi(int pathId, int poiId, int activationId, TypeIn activationType,
                UnityAction completedCallback)
            {
                _pathId = pathId;
                _poiId = poiId;
                _activationId = activationId;
                _activationType = activationType;
                _completedCallback = completedCallback;

                Debug.LogFormat("Request sheet list for path ({0}) poi ({1}) {2} ({3})", _pathId, _poiId, _activationType, _activationId);

                NativeAppController.RequestSheetList(SheetListQuery.ForActivation(pathId, activationId, activationType),
                    OnReceivedPoiList);
            }

            private static int _activationId;
            private static TypeIn _activationType;
            private static int _pathId;
            private static int _poiId;

            private static UnityAction _completedCallback;

            private static void OnReceivedPathEntryPointList(SheetList list)
            {
                var directory = System.IO.Path.Combine(Application.persistentDataPath, "export");
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                var file = System.IO.Path.Combine(directory, $"path_{_pathId}_sheetlist.json");
                File.WriteAllText(file, JsonUtility.ToJson(list.ToPayload()));
                _completedCallback?.Invoke();
            }

            private static void OnReceivedPoiList(SheetList list)
            {
                Debug.LogFormat("Sheet list received for path ({0}) poi ({1}) {2} ({3})", _pathId, _poiId, _activationType, _activationId);

                var directory = System.IO.Path.Combine(Application.persistentDataPath, "export");
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                var file = System.IO.Path.Combine(directory, $"path_{_pathId}_{_activationType}_{_activationId}_sheetlist.json");

                Debug.LogFormat(" Saving sheet list for path ({0}) poi ({1}) {2} ({3}) to {4}", _pathId, _poiId, _activationType, _activationId, file);

                File.WriteAllText(file, JsonUtility.ToJson(list.ToPayload()));
                _completedCallback?.Invoke();
            }


        }

        private static class PoisDataExorter
        {
            public static void ExportForPath(int pathId, UnityAction completedCallback)
            {
                _pathId = pathId;
                _completedCallback = completedCallback;

                NativeAppController.RequestPoiList(PoiListQuery.Default(pathId), OnListReceived);
            }

            private static void OnListReceived(PoiList list)
            {
                var directory = System.IO.Path.Combine(Application.persistentDataPath, "export");
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                var file = System.IO.Path.Combine(directory, $"path_{_pathId}_poilist.json");
                File.WriteAllText(file, JsonUtility.ToJson(list.ToPayload()));
                _completedCallback?.Invoke();
            }

            private static int _pathId;
            private static UnityAction _completedCallback;
        }

    }
}