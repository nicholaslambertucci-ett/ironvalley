using System;

namespace Ett.Vdt.NativeApplication.Data.Galleries.Payloads.Extensions
{
    internal static class ExtensionsToGalleryItemPayload
    {
        public static GalleryItem ToGalleryItem(this GalleryItemPayload payload)
        {
            var item = new GalleryItem
            {
                Description = payload.description,
                Id = payload.id,
                MediaPath = payload.mediaPath,
                Subtitle = payload.subtitle,
                Title = payload.title
            };

            Enum.TryParse(payload.mediaType, true, out item.MediaType);
            return item;
        }
    }
}