using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Vrs.Payloads
{
    [Serializable]
    internal struct VrPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string frameType;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public VrHotspotPayload[] hotspots;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string layout3d;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string mappingLayout;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string mediaType;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string mediaPath;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string mode;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public float rotation;
    }
}