namespace Ett.Vdt.NativeApplication.Data.Vrs.Payloads.Extentions
{
    internal static class ExtensionsToVrHotspotPayload
    {
        public static VrHotspot ToHotspot(this VrHotspotPayload payload)
            => new VrHotspot
            {
                Description = payload.description,
                MediaPath = payload.mediaPath,
                Subtitle = payload.subtitle,
                Title = payload.title,
                X = payload.x,
                Y = payload.y
            };
    }
}