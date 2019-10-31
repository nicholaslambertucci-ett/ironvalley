using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Sheets.Payloads
{
    [Serializable]
    internal struct SheetListQueryPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int activationId;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string activationType;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int limit;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int offset;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int pathId;
    }
}