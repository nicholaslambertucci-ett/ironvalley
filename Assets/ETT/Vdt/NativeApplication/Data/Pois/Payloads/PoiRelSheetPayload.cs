using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Pois.Payloads
{
    [Serializable]
    public struct PoiRelSheetPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int poiId;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int sheetId;
    }
}