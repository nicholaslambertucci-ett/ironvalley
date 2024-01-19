namespace Ett.Vdt.NativeApplication.Data.Paths.Payloads.Extensions
{
    internal  static class ExtensionsToPath
    {
        public static PathPayload ToPayload(this Path path)
            => new PathPayload
            {
                description = path.Description,
                id = path.Id,
                mediaPath = path.MediaPath,
                subtitle = path.Subtitle,
                tag = path.Tag,
                title = path.Title
            };
    }
}
