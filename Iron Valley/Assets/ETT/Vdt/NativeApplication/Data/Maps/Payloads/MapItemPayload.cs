using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Maps.Payloads
{
    [Serializable]
    public struct MapItemPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField]public string activationId;
        // ReSharper disable once InconsistentNaming
        [SerializeField]public string activationType;
        // ReSharper disable once InconsistentNaming
        [SerializeField]public int id;
        // ReSharper disable once InconsistentNaming
        [SerializeField]public string mediaPath;
        // ReSharper disable once InconsistentNaming
        [SerializeField]public float? latitude;
        // ReSharper disable once InconsistentNaming
        [SerializeField]public float? longitude;
        // ReSharper disable once InconsistentNaming
        [SerializeField]public string poiId;
        // ReSharper disable once InconsistentNaming
        [SerializeField]public int? x;
        // ReSharper disable once InconsistentNaming
        [SerializeField]public int? y;

    }
}