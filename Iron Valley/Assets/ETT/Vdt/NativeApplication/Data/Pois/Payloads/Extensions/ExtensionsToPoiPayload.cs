namespace Ett.Vdt.NativeApplication.Data.Pois.Payloads.Extensions
{
    internal static class ExtensionsToPoiPayload
    {
        public static Poi ToPoi(this PoiPayload payload)
        {
            return new Poi
            {
                Description = payload.description,
                Id = payload.id,
                MediaPath = payload.mediaPath,
                Subtitle = payload.subtitle,
                Title = payload.title
            };
        }
    }
}