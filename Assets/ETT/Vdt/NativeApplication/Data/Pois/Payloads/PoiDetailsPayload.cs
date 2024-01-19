using System;
using Ett.Vdt.NativeApplication.Data.Sheets.Payloads;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Pois.Payloads
{
    [Serializable]
    internal struct PoiDetailsPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string description;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int id;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string mediaPath;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string title;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public SheetPayload sheet;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string subtitle;
    }
}