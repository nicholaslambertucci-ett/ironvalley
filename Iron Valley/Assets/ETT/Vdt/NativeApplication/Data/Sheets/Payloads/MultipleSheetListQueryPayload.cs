using System;
using Ett.Vdt.NativeApplication.Data.Activators.Payloads;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Sheets.Payloads
{
    [Serializable]
    internal struct MultipleSheetListQueryPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public ActivatorPayload[] activators;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int limit;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int offset;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int pathId;
    }
}