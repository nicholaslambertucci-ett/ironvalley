namespace Ett.Vdt.NativeApplication.Data.Galleries.Payloads.Extensions
{
    internal static class ExtensionsToGalleryPayload
    {
        public static Gallery ToGallery(this GalleryPayload payload)
        {
            var gallery = new Gallery
            {
                Id = payload.id,
                Items = new GalleryItem[payload.galleryItems?.Length ?? 0],
                Tag = payload.tag
            };

            if (payload.galleryItems == null)
                return gallery;

            for (var i = 0; i < gallery.Items.Length; i++)
                gallery.Items[i] = payload.galleryItems[i].ToGalleryItem();

            return gallery;
        }
           
    }
}