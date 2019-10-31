using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Sheets.Payloads
{
    [Serializable]
    internal struct SheetListPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public SheetPayload[] sheets;
    }
}