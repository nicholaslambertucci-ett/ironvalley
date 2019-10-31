using System;
using System.Collections;
using System.Collections.Generic;
using Ett.Vdt.Data;
using Ett.Vdt.Fake;
using Ett.Vdt.NativeApplication.Data.Maps.Payloads;
using Ett.Vdt.NativeApplication.Data.Paths.Payloads;
using Ett.Vdt.NativeApplication.Data.Pois.Payloads;
using Ett.Vdt.NativeApplication.Data.Sheets.Payloads;
using Ett.Vdt.NativeApplication.Fake.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Ett.Vdt.NativeApplication.Fake
{
    public partial class VdtFakeLogic : MonoBehaviour
    {
        #region Message from "Unity App"

        #region Vdt

        // ReSharper disable once UnusedMember.Local
        private void OnExitToApp()
        {
            Debug.Log("VdtFakeLogic - OnExitToApp");
        }

        private void OnReady()
        {
            Debug.LogFormat("VdtFakeLogic - OnReady");
            this.StartCoroutine(this.SendConfigureToApp());
        }

#if VDT_V_2
        private void OnReady(string gameObjectName)
        {
            Debug.LogFormat("VdtFakeLogic - OnReady - {0}", gameObjectName);
            this._mainControllerName = gameObjectName;
            this.StartCoroutine(this.SendConfigureToApp());
        }
#endif

        #endregion

        #region Vdt.NativeApp

        //todo: added!
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        private void onBeaconCompleteStartScan()
        {
            Debug.LogWarning("Not yet implemented");
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        private void onBeaconStartScan()
        {
            Debug.LogWarning("Not yet implemented");
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        private void onBeaconStopScan()
        {
            Debug.LogWarning("Not yet implemented");
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        private void onGetMultipleSheetList(string queryPayload)
        {
            this.StartCoroutine(this.SendMultipleSheetList(queryPayload));
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        private void onGetMapList(string queryPayload)
        {
            this.StartCoroutine(this.SendMapList(queryPayload));
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        private void onGetPathList(string queryPayload)
        {
            this.StartCoroutine(this.SendPathList(queryPayload));
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        private void onGetPoiDetails(int poiId)
        {
            Debug.LogWarning("Not yet implemented");
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        private void onGetPoiList(string queryPayload)
        {
            this.StartCoroutine(this.SendPoiList(queryPayload));
        }

        //todo: !added
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        private void onNativeAppReady(string gameObjectName)
        {
            if (!string.IsNullOrEmpty(gameObjectName))
                this._nativeAppControllerName = gameObjectName;

            this.StartCoroutine(this.SendNativeAppReady());
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        private void onGetSheetList(string queryPayload)
        {
            this.StartCoroutine(this.SendSheetList(queryPayload));
        }

        #endregion

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            if (string.IsNullOrEmpty(this._sceneToLoad))
                return;

            SceneManager.LoadScene(this._sceneToLoad);//, "StartScene");
        }

        #endregion


        [SerializeField] private float _delayResponseSeconds = 1f;
        [SerializeField] private string _sceneToLoad = null;
        [SerializeField] private string _sceneToLoadOnReady = null;


        #region Vdt

        private string _mainControllerName = "MainController";

        private IEnumerator SendConfigureToApp()
        {
            yield return new WaitForSeconds(this._delayResponseSeconds);
            yield return new WaitWhile(() => string.IsNullOrEmpty(this._sceneToLoadOnReady));
            if (string.IsNullOrEmpty(this._sceneToLoadOnReady))
            {
                Debug.LogError("NO scene to load specified on VdtFakeLogic");
            }
            FakeNativeApp.Instance.UnitySendMessage(this._mainControllerName, "Configure",
                JsonUtility.ToJson(new Message
                {
                    Type = this._sceneToLoadOnReady
                }));
        }

        #endregion

        #region Vdt.NativeApp

        private string _nativeAppControllerName = "NativeAppController";

        private Coroutine _beaconScannerCoroutine;


        private IEnumerator SendMapList(string queryPayload)
        {
            yield return new WaitForSeconds(this._delayResponseSeconds);

            var query = JsonUtility.FromJson<MapListQueryPayload>(queryPayload);
            var fileList = GetFileContent<MapListPayload>($"path_{query.pathId}_maplist.json");
            if (fileList.maps == null)
            {
                FakeNativeApp.Instance.UnitySendMessage(this._nativeAppControllerName,
                    "ReceiveMapList",
                    JsonUtility.ToJson(new MapListPayload
                    {
                        maps = new MapPayload[0]
                    })
                );
                yield break;
            }

            var mapList = new List<MapPayload>();
            for (var i = 0; i < fileList.maps.Length; i++)
            {
                var map = fileList.maps[i];
                if (!MapListFilters.Predicate(map, query))
                    continue;

                map.mediaPath = ConvertFilePath(map.mediaPath);
                if (map.mapItems != null)
                    for (var y = 0; y < map.mapItems.Length; y++)
                        map.mapItems[y].mediaPath = ConvertFilePath(map.mapItems[y].mediaPath);


                mapList.Add(map);
            }

            var maps = new MapPayload[mapList.Count];
            mapList.CopyTo(maps);
            maps = PaginationHelper.Paginate(maps, query.limit, query.offset);

            FakeNativeApp.Instance.UnitySendMessage(this._nativeAppControllerName,
                "ReceiveMapList",
                JsonUtility.ToJson(new MapListPayload
                {
                    maps = maps
                })
            );
        }

        private IEnumerator SendMultipleSheetList(string queryPayload)
        {
            yield return new WaitForSeconds(this._delayResponseSeconds);

            var query = JsonUtility.FromJson<MultipleSheetListQueryPayload>(queryPayload);

            if (query.activators == null)
            {
                {
                    FakeNativeApp.Instance.UnitySendMessage(this._nativeAppControllerName,
                        "ReceiveMultipleSheetList",
                        JsonUtility.ToJson(new MultipleSheetListPayload
                        {
                            activatedList = new ActivatedSheetListPayload[0]
                        })
                    );
                    yield break;
                }
            }


            var resultList = new List<ActivatedSheetListPayload>();
            foreach (var activator in query.activators)
            {
                var fileList = GetSheets(query.pathId, activator.type, activator.id);
                if (fileList.Length == 0)
                    continue;

                var sheetList = new List<SheetPayload>();
                for (var i = 0; i < fileList.Length; i++)
                {
                    // todo: aggiungere filtro
                    var sheet = fileList[i];
                    sheet.mediaPath = ConvertFilePath(sheet.mediaPath);
                    sheetList.Add(sheet);
                }

                var sheets = new SheetPayload[sheetList.Count];
                sheetList.CopyTo(sheets);
                resultList.Add(new ActivatedSheetListPayload
                {
                    activator = activator,
                    list = new SheetListPayload
                    {
                        sheets = sheets
                    }
                });
            }


            var activated = new ActivatedSheetListPayload[resultList.Count];
            resultList.CopyTo(activated);

            FakeNativeApp.Instance.UnitySendMessage(this._nativeAppControllerName,
                "ReceiveMultipleSheetList",
                JsonUtility.ToJson(new MultipleSheetListPayload
                {
                    activatedList = activated
                })
            );

        }

        private IEnumerator SendNativeAppReady()
        {
            yield return new WaitForSeconds(this._delayResponseSeconds);
            FakeNativeApp.Instance.UnitySendMessage(this._nativeAppControllerName, "ReceiveNativeAppReady");
        }

        private IEnumerator SendPathList(string queryPayload)
        {
            yield return new WaitForSeconds(this._delayResponseSeconds);

            var query = JsonUtility.FromJson<PathListQueryPayload>(queryPayload);

            var fileList = GetFileContent<PathListPayload>("pathlist.json");
            if (fileList.paths == null)
            {
                FakeNativeApp.Instance.UnitySendMessage(this._nativeAppControllerName,
                    "ReceivePathList",
                    JsonUtility.ToJson(new PathListPayload
                    {
                        paths = new PathPayload[0]
                    })
                );
                yield break;
            }


            var pathList = new List<PathPayload>();
            for (var i = 0; i < fileList.paths.Length; i++)
            {
                if (!PathListFilters.Predicate(fileList.paths[i], query))
                    continue;

                var path = fileList.paths[i];
                path.mediaPath = ConvertFilePath(path.mediaPath);
                pathList.Add(path);
            }

            var paths = new PathPayload[pathList.Count];
            pathList.CopyTo(paths);

            paths = PaginationHelper.Paginate(paths, query.limit, query.offset);

            FakeNativeApp.Instance.UnitySendMessage(this._nativeAppControllerName,
                "ReceivePathList",
                JsonUtility.ToJson(new PathListPayload
                {
                    paths = paths

                })
            );
        }

        private IEnumerator SendPoiList(string queryPayload)
        {
            yield return new WaitForSeconds(this._delayResponseSeconds);

            var query = JsonUtility.FromJson<PoiListQueryPayload>(queryPayload);

            var fileList = GetFileContent<PoiListPayload>($"path_{query.pathId}_poilist.json");
            if (fileList.pois == null)
            {
                FakeNativeApp.Instance.UnitySendMessage(this._nativeAppControllerName,
                    "ReceivePoiList",
                    JsonUtility.ToJson(new PoiListPayload
                    {
                        pois = new PoiPayload[0]
                    })
                );
                yield break;
            }

            var poiList = new List<PoiPayload>();
            for (var i = 0; i < fileList.pois.Length; i++)
            {
                if (!PoiListFilters.Predicate(fileList.pois[i], query))
                    continue;
                var poi = fileList.pois[i];
                poi.mediaPath = ConvertFilePath(poi.mediaPath);
                poiList.Add(fileList.pois[i]);
            }

            var pois = new PoiPayload[poiList.Count];
            poiList.CopyTo(pois);

            pois = PaginationHelper.Paginate(pois, query.limit, query.offset);

            FakeNativeApp.Instance.UnitySendMessage(this._nativeAppControllerName,
                "ReceivePoiList",
                new PoiListPayload
                {
                    pois = pois
                }
            );
        }

        private static SheetPayload[] GetSheets(int pathId, string activationType, int? activationId)
        {
            var fileList = GetFileContent<SheetListPayload>(activationId > -1 && !string.IsNullOrEmpty(activationType)
                ? $"path_{pathId}_{activationType}_{activationId}_sheetlist.json"
                : $"path_{pathId}_sheetlist.json");

            return fileList.sheets == null
                ? new SheetPayload[0]
                : fileList.sheets;
        }


        private IEnumerator SendSheetList(string queryPayload)
        {
            yield return new WaitForSeconds(this._delayResponseSeconds);

            var query = JsonUtility.FromJson<SheetListQueryPayload>(queryPayload);

            var fileList = GetSheets(query.pathId, query.activationType, query.activationId);

            //var fileList = GetFileContent<SheetListPayload>(query.activationId > -1 && !string.IsNullOrEmpty(query.activationType)
            //    ? $"path_{query.pathId}_{query.activationType}_{query.activationId}_sheetlist.json"
            //    : $"path_{query.pathId}_sheetlist.json");

            //if (fileList.sheets == null)
            if (fileList.Length == 0)
            {
                FakeNativeApp.Instance.UnitySendMessage(this._nativeAppControllerName,
                    "ReceiveSheetList",
                    JsonUtility.ToJson(new SheetListPayload
                    {
                        //sheets = new SheetPayload[0]
                        sheets = fileList
                    })
                );
                yield break;
            }

            var sheetList = new List<SheetPayload>();
            //for (var i = 0; i < fileList.sheets.Length; i++)
            for (var i = 0; i < fileList.Length; i++)
            {
                // todo: aggiungere filtro
                //var sheet = fileList.sheets[i];
                var sheet = fileList[i];
                sheet.mediaPath = ConvertFilePath(sheet.mediaPath);
                sheetList.Add(sheet);
            }

            var sheets = new SheetPayload[sheetList.Count];
            sheetList.CopyTo(sheets);

            sheets = PaginationHelper.Paginate(sheets, query.limit, query.offset);
            FakeNativeApp.Instance.UnitySendMessage(this._nativeAppControllerName,
                "ReceiveSheetList",
                JsonUtility.ToJson(new SheetListPayload
                {
                    sheets = sheets
                })
            );

        }

        public static string ConvertFilePath(string originalPath)
        {
            if (string.IsNullOrEmpty(originalPath))
                return originalPath;

            var fileName = System.IO.Path.Combine(Application.persistentDataPath,
                "exported",
                "data",
                System.IO.Path.GetFileName(originalPath));
#if UNITY_EDITOR || !UNITY_ANDROID && ! UNITY_IPHONE
            if (!System.IO.Path.GetExtension(fileName).Equals(".mp3", StringComparison.InvariantCultureIgnoreCase))
                return fileName;

            var convertedFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(fileName),
                $"{System.IO.Path.GetFileNameWithoutExtension(fileName)}.wav");

            if (System.IO.File.Exists(convertedFile))
                return convertedFile;

            using (NAudio.Wave.Mp3FileReader mp3 = new NAudio.Wave.Mp3FileReader(fileName))
            {
                using (NAudio.Wave.WaveStream pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    NAudio.Wave.WaveFileWriter.CreateWaveFile(convertedFile, pcm);
                }
            }

            return convertedFile;
#else
            return fileName;
#endif
        }

        private static T GetFileContent<T>(string fileName)
        {
            var fullPath = System.IO.Path.Combine(Application.persistentDataPath, "exported", fileName);
            if (!System.IO.File.Exists(fullPath))
                return default(T);

            var content = System.IO.File.ReadAllText(fullPath);
            return JsonUtility.FromJson<T>(content);
        }

        #endregion
    }
}
