namespace Ett.Vdt.NativeApplication.Data.Pois.Payloads.Extensions
{
    internal static class ExtensionsToPoiCategoryPayload
    {
        public static PoiCategory ToPoiCategory(this PoiCategoryPayload payload)
            => new PoiCategory
            {
                Description = payload.description,
                Id = payload.id,
                MediaPath = payload.mediaPath,
                Tag = payload.tag,
                Title = payload.title,
                Subtitle = payload.subtitle
            };
    }
}