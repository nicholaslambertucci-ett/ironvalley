using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Pois.Payloads
{
    [Serializable]
    internal struct PoiPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string description;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int id;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string mediaPath;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string title;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string subtitle;
    }
}