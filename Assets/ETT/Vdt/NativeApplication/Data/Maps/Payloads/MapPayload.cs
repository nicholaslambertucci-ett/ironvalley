using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Maps.Payloads
{
    [Serializable]
    public struct MapPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string description;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int id;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string mediaPath;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public MapItemPayload[] mapItems;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string subtitle;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string tag;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string title;
    }
}