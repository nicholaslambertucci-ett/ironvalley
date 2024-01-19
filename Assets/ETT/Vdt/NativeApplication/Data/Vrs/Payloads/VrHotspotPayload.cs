using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Vrs.Payloads
{
    [Serializable]
    internal struct VrHotspotPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string description;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string mediaPath;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string subtitle;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string title;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public float x;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public float y;
    }
}