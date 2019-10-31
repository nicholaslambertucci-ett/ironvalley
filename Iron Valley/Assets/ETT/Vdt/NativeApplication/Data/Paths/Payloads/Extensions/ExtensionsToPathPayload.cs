namespace Ett.Vdt.NativeApplication.Data.Paths.Payloads.Extensions
{
    internal static class ExtensionsToPathPayload
    {
        public static Path ToPath(this PathPayload payload)
            => new Path
            {
                Description = payload.description,
                Id = payload.id,
                MediaPath = payload.mediaPath,
                Subtitle = payload.subtitle,
                Tag = payload.tag,
                Title = payload.title
            };
    }
}