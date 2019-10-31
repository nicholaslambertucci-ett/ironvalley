using System.Collections;
using System.IO;
using Ett.Vdt.NativeApplication.Data.Paths;
using Ett.Vdt.NativeApplication.Data.Paths.Payloads.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Ett.Vdt.NativeApplication.Fake.Exporters
{
    public class PathsExporter
    {
        public PathsExporter(UnityAction completedCallback)
        {
            this._completedCallback = completedCallback;
        }

        public IEnumerator Run()
        {
            this._listReceived = false;
            Debug.LogFormat("PathsExporter - Being");

            NativeAppController.RequestPathList(PathListQuery.Empty, this.OnListReceived);
            yield return null;

            // wait for list received
            yield return new WaitUntil(() => this._listReceived);

            // ciclare su paths

            if (this._list.Paths == null || this._list.Paths.Length == 0)
            {
                Debug.LogWarningFormat("PathsExporter - No path found!!! path list {0}",
                    this._list.Paths == null ? "is null" : $"has {this._list.Paths.Length} element(s)");
                this._completedCallback?.Invoke();
                Debug.LogFormat("PathsExporter - End");
                yield break;
            }

            Debug.LogFormat("PathsExporter - list count: {0}", this._list.Paths?.Length);
            foreach (var path in this._list.Paths)
            {
                Debug.LogFormat("PathsExporter - exporting data for path {0} begin", path.Id);
                var pathExporter = new PathExporter(path.Id);
                foreach (var ey in pathExporter.Run())
                    yield return ey;
                Debug.LogFormat("PathsExporter - exporting data for path {0} end", path.Id);
                yield return null;
            }


            this._completedCallback?.Invoke();
            Debug.LogFormat("PathsExporter - End");
        }

        private readonly UnityAction _completedCallback;

        private PathList _list;
        private bool _listReceived;


        private void OnListReceived(PathList list)
        {
            Debug.LogFormat("PathsExporter - List received");

            var directory = System.IO.Path.Combine(Application.persistentDataPath, "export");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var file = System.IO.Path.Combine(directory, "pathlist.json");
            Debug.LogFormat("PathsExporter - saving list to {0}", file);
            File.WriteAllText(file, JsonUtility.ToJson(list.ToPayload()));

            this._list = list;
            this._listReceived = true;
        }


        private class PathExporter
        {
            public PathExporter(int id)
            {
                this._id = id;
            }

            public IEnumerable Run()
            {
                Debug.LogFormat("PathExporter [path {0}] - Being", this._id);

                #region Maps - Entry Points
                Debug.LogFormat("PathExporter [path {0}] - exporting map entry point begin", this._id);
                var mapsExporter = new MapsExporter(this._id);
                foreach (var ey in mapsExporter.RunForPath())
                    yield return ey;
                Debug.LogFormat("PathExporter [path {0}] - exporting map entry point end", this._id);
                #endregion

                #region Sheets - Entry Points
                Debug.LogFormat("PathExporter [path {0}] - exporting sheet entry point begin", this._id);
                var sheetsExporter = new SheetsExporter(this._id);
                foreach (var ey in sheetsExporter.Run())
                    yield return ey;
                Debug.LogFormat("PathExporter [path {0}] - exporting sheet entry point end", this._id);
                #endregion

                yield return null;
                Debug.LogFormat("PathExporter [path {0}] - End", this._id);
            }



            private readonly int _id;
        }
    }
}