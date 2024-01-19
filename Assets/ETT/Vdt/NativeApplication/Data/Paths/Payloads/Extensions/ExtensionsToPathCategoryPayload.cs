namespace Ett.Vdt.NativeApplication.Data.Paths.Payloads.Extensions
{
    internal static class ExtensionsToPathCategoryPayload
    {
        public static PathCategory ToPathCategory(this PathCategoryPayload payload)
            => new PathCategory
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