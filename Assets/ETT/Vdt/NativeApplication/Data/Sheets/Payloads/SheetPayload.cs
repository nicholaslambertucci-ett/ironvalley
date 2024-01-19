using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Sheets.Payloads
{
    [Serializable]
    public struct SheetPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string description;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int id;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string mediaPath;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int poiId;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public SheetItemPayload[] sheetItems;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string subtitle;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string tag;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string title;
    }
}