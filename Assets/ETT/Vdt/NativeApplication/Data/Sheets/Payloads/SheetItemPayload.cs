using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Sheets.Payloads
{
    [Serializable]
    public struct SheetItemPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int id;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string extraData;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string type;
    }
}