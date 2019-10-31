using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Galleries.Payloads
{
    [Serializable]
    internal struct GalleryItemPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string description;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int id;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string mediaPath;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string mediaType;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string poiId;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string subtitle;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string title;
    }
}