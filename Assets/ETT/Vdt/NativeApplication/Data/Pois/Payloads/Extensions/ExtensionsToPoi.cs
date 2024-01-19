namespace Ett.Vdt.NativeApplication.Data.Pois.Payloads.Extensions
{
    internal static class ExtensionsToPoi
    {
        public static PoiPayload ToPayload(this Poi poi)
            => new PoiPayload
            {
                description = poi.Description,
                id = poi.Id,
                mediaPath = poi.MediaPath,
                subtitle = poi.Subtitle,
                title = poi.Title
            };
    }
}