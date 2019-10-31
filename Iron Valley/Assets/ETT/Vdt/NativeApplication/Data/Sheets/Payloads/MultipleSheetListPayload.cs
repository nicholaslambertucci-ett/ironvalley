using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Sheets.Payloads
{
    [Serializable]
    internal struct MultipleSheetListPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public ActivatedSheetListPayload[] activatedList;
    }
}