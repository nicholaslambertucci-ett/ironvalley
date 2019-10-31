using System;
using Ett.Vdt.NativeApplication.Data.Activators.Payloads;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Sheets.Payloads
{
    [Serializable]
    internal struct ActivatedSheetListPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public ActivatorPayload activator;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public SheetListPayload list;
    }
}