using System.Collections;
using System.IO;
using Ett.Vdt.NativeApplication.Data.Sheets;
using Ett.Vdt.NativeApplication.Data.Sheets.Extensions;
using Ett.Vdt.NativeApplication.Data.Sheets.Payloads.Extensions;
using Ett.Vdt.NativeApplication.Data.Types;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Fake.Exporters
{
    internal class SheetsExporter
    {
        public SheetsExporter(int pathId)
        {
            this._pathId = pathId;
            this._isEntryPoint = true;
        }

        public SheetsExporter(int pathId, TypeIn activationType, int activationId)
        {
            this._activationId = activationId;
            this._activationType = activationType;
            this._pathId = pathId;
            this._isEntryPoint = false;
        }

        public IEnumerable Run()
            => this._isEntryPoint
                ? this.RunForEntryPoint()
                : this.RunForActivation();

        private readonly int _activationId;
        private readonly TypeIn _activationType;
        private readonly bool _isEntryPoint;
        private readonly int _pathId;

        private SheetList _list;
        private bool _listReceived;

        private IEnumerable RunForActivation()
        {
            Debug.LogFormat("SheetsExporter [path {0}, {1} {2}] - Being", this._pathId, this._activationType, this._activationId);

            NativeAppController.RequestSheetList(
                SheetListQuery.ForActivation(this._pathId, this._activationId, this._activationType),
                this.OnActivatedListReceived);
            yield return null;

            yield return new WaitUntil(() => this._listReceived);

            Debug.LogFormat("SheetsExporter [path {0}, {1} {2}] - End", this._pathId, this._activationType, this._activationId);
        }

        private IEnumerable RunForEntryPoint()
        {
            Debug.LogFormat("SheetsExporter [path {0}] - Being", this._pathId);

            NativeAppController.RequestSheetList(
                SheetListQuery.ForPathEntryPoint(this._pathId),
                this.OnEntryPointListReceived);
            yield return null;

            yield return new WaitUntil(() => this._listReceived);

            Debug.LogFormat("SheetsExporter [path {0}] - End", this._pathId);

            yield break;
        }

        private void OnActivatedListReceived(SheetList list)
        {
            Debug.LogFormat("SheetsExporter [path {0}, {1} {2}] - List received", this._pathId, this._activationType, this._activationId);

            var directory = Path.Combine(Application.persistentDataPath, "export");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var activationType = (InternalTypeIn) this._activationType;

            var file = Path.Combine(directory, $"path_{this._pathId}_{activationType}_{this._activationId}_sheetlist.json");
            Debug.LogFormat("SheetsExporter [path {0}, {1} {2}] - saving list to {3}",
                this._pathId, this._activationType, this._activationId, file);

            File.WriteAllText(file, JsonUtility.ToJson(list.ToPayload()));

            this._list = list;
            this._listReceived = true;
        }

        private void OnEntryPointListReceived(SheetList list)
        {
            Debug.LogFormat("SheetsExporter [path {0}] - List received", this._pathId);

            var directory = Path.Combine(Application.persistentDataPath, "export");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var file = Path.Combine(directory, $"path_{this._pathId}_sheetlist.json");
            Debug.LogFormat("SheetsExporter [path {0}] - saving list to {1}",
                this._pathId, file);

            File.WriteAllText(file, JsonUtility.ToJson(list.ToPayload()));

            this._list = list;
            this._listReceived = true;
        }
    }
}
