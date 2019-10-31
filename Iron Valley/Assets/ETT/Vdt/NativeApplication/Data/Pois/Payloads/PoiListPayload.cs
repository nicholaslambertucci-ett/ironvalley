using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Pois.Payloads
{
    [Serializable]
    internal struct PoiListPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public PoiPayload[] pois;
    }
}
