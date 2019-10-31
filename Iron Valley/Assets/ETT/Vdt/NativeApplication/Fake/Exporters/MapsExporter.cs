using System.Collections;
using System.IO;
using Ett.Vdt.NativeApplication.Data.Maps;
using Ett.Vdt.NativeApplication.Data.Maps.Payloads.Extensions;
using Ett.Vdt.NativeApplication.Data.Sheets;
using Ett.Vdt.NativeApplication.Data.Types;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Fake.Exporters
{
    internal class MapsExporter
    {
        public MapsExporter(int pathId)
        {
            this._pathId = pathId;
        }

        public IEnumerable RunForPath()
        {
            this._listReceived = false;
            Debug.LogFormat("MapsExporter [path ({0})] - Begin", this._pathId);

            NativeAppController.RequestMapList(MapListQuery.ForPathEntryPoint(this._pathId), this.OnReceivedList);
            yield return null;

            yield return new WaitUntil(() => this._listReceived);

            if (this._list.Maps == null || this._list.Maps.Length == 0)
            {
                Debug.LogWarningFormat("MapsExporter [path ({0})] - No map found!!! map list {1}",
                    this._pathId,
                    this._list.Maps == null ? "is null" : $"has {this._list.Maps.Length} element(s)");
                yield break;
            }

            Debug.LogFormat("MapsExporter [path ({0})] - list count: {1}", this._pathId, this._list.Maps.Length);

            foreach (var map in this._list.Maps)
            {
                Debug.LogFormat("MapsExporter [path ({0})] - exporting data for map {1} begin", this._pathId, map.Id);
                var mapExporter = new MapExporter(this._pathId, map);
                foreach (var ey in mapExporter.Run())
                    yield return ey;
                Debug.LogFormat("MapsExporter [path ({0})] - exporting data for map {1} end", this._pathId, map.Id);
            }

            Debug.LogFormat("MapsExporter [path ({0})] - End", this._pathId);
        }

        private readonly int _pathId;
        private MapList _list;
        private bool _listReceived;

        private void OnReceivedList(MapList list)
        {
            Debug.LogFormat("MapsExporter [path ({0})] - List received", this._pathId);

            var directory = System.IO.Path.Combine(Application.persistentDataPath, "export");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var file = System.IO.Path.Combine(directory, $"path_{this._pathId}_maplist.json");

            Debug.LogFormat("MapsExporter [path ({0})] - saving list to {1}", this._pathId, file);

            File.WriteAllText(file, JsonUtility.ToJson(list.ToPayload()));

            this._list = list;
            this._listReceived = true;
        }


        private class MapExporter
        {
            public MapExporter(int pathId, Map map)
            {
                this._pathId = pathId;
                this._map = map;
            }

            public IEnumerable Run()
            {
                Debug.LogFormat("MapExporter [path {0}, map {1}] - Being", this._pathId, this._map.Id);

                yield return null;

                if (this._map.Items == null || this._map.Items.Length == 0)
                {
                    Debug.LogWarningFormat("MapExporter [path {0}, map {1}] - No item found!!! item list {2}",
                        this._pathId,
                        this._map.Id,
                        this._map.Items == null ? "is null" : "has 0 element");
                    yield break;
                }

                Debug.LogFormat("MapsExporter [path {0}, map {1}] - items list count: {2}", this._pathId, this._map.Id, this._map.Items.Length);
                foreach (var item in this._map.Items)
                {
                    yield return null;
                    if (item.ActivationId == null || item.ActivationType == null || item.PoiId == null)
                    {
                        Debug.LogWarningFormat("MapsExporter [path {0}, map {1}] - item {2} has invalid activation fields: " +
                                               "ActivationId ({3}) " +
                                               "ActivationType ({4}) " +
                                               "PoiId ({5})",
                            this._pathId, this._map.Id, item.Id, item.ActivationId, item.ActivationType, item.PoiId);
                        continue;
                    }

                    foreach (var ey in this.ExportItem(item))
                        yield return ey;
                }




                Debug.LogFormat("MapExporter [path {0}, map {1}] - End", this._pathId, this._map.Id);
            }

            private readonly Map _map;
            private readonly int _pathId;

            private IEnumerable ExportItem(MapItem item)
            {
                switch (item.ActivationType)
                {
                    case TypeOut.Unknown:
                        Debug.LogWarningFormat("MapsExporter [path {0}, map {1}]: item {2} ActivationType is {3} - nothing to do yet", this._pathId, this._map.Id, item.Id, item.ActivationType);
                        yield break;
                    case TypeOut.Galleries:
                        Debug.LogWarningFormat("MapsExporter [path {0}, map {1}]: item {2} ActivationType is {3} - nothing to do yet", this._pathId, this._map.Id, item.Id, item.ActivationType);
                        yield break;
                    case TypeOut.Map:
                        Debug.LogWarningFormat("MapsExporter [path {0}, map {1}]: item {2} ActivationType is {3} - nothing to do yet", this._pathId, this._map.Id, item.Id, item.ActivationType);
                        yield break;
                    case TypeOut.PikkartArScene:
                        Debug.LogWarningFormat("MapsExporter [path {0}, map {1}]: item {2} ActivationType is {3} - nothing to do yet", this._pathId, this._map.Id, item.Id, item.ActivationType);
                        yield break;
                    case TypeOut.Poi:
                        Debug.LogWarningFormat("MapsExporter [path {0}, map {1}]: item {2} ActivationType is {3} - nothing to do yet", this._pathId, this._map.Id, item.Id, item.ActivationType);
                        yield break;
                    case TypeOut.Sheet:
                        Debug.LogWarningFormat("MapsExporter [path {0}, map {1}]: item {2} ActivationType is {3} - export begin", this._pathId, this._map.Id, item.Id, item.ActivationType);
                        var sheetsExporter = new SheetsExporter(this._pathId, TypeIn.MapItem, item.Id);
                        foreach (var ey in sheetsExporter.Run())
                            yield return ey;
                        Debug.LogWarningFormat("MapsExporter [path {0}, map {1}]: item {2} ActivationType is {3} - export end", this._pathId, this._map.Id, item.Id, item.ActivationType);
                        yield break;
                    case TypeOut.UnitySceneActivator:
                        Debug.LogWarningFormat("MapsExporter [path {0}, map {1}]: item {2} ActivationType is {3} - nothing to do yet", this._pathId, this._map.Id, item.Id, item.ActivationType);
                        yield break;
                    case TypeOut.Vr:
                        Debug.LogWarningFormat("MapsExporter [path {0}, map {1}]: item {2} ActivationType is {3} - nothing to do yet", this._pathId, this._map.Id, item.Id, item.ActivationType);
                        yield break;
                    default:
                        Debug.LogWarningFormat("MapsExporter [path {0}, map {1}]: item {2} ActivationType is {3} - nothing to do yet", this._pathId, this._map.Id, item.Id, item.ActivationType);
                        yield break;
                }
            }
        }
    }
}