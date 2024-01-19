using System;
using Ett.Vdt.NativeApplication.Data.Galleries.Payloads;
using Ett.Vdt.NativeApplication.Data.Galleries.Payloads.Extensions;
using Ett.Vdt.NativeApplication.Data.Sheets;
using Ett.Vdt.NativeApplication.Data.Types;
using UnityEngine;

namespace Ett.Vdt.NativeApplication.Data.Galleries.Extensions
{
    public static class ExtensionsToSheetItem
    {
        public static Gallery? GetGallery(this SheetItem item)
        {
            if (item.Type != TypeOut.Galleries)
                return null;

            if (item.Extra != null)
                return (Gallery)item.Extra;

            var galleryPayload = JsonUtility.FromJson<GalleryPayload>(item.ExtraData);
            var gallery = galleryPayload.ToGallery();
            item.Extra = gallery;
            return gallery;
        }
    }
}