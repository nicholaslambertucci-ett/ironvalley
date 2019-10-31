using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Pois.Payloads
{
    [Serializable]
    internal struct PoiRelPathPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int pathId;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int poiId;
    }
}