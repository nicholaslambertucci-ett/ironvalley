using System;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Galleries.Payloads
{
    [Serializable]
    internal struct GalleryPayload
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] public int id;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public GalleryItemPayload[] galleryItems;
        // ReSharper disable once InconsistentNaming
        [SerializeField] public string tag;
    }
}