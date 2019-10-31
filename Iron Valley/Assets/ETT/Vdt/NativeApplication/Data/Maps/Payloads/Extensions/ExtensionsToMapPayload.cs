namespace Ett.Vdt.NativeApplication.Data.Maps.Payloads.Extensions
{
    public static class ExtensionsToMapPayload
    {
        public static Map ToMap(this MapPayload payload)
        {
            var map = new Map
            {
                Description = payload.description,
                Id = payload.id,
                Items = new MapItem[payload.mapItems?.Length ?? 0],
                MediaPath = payload.mediaPath,
                Subtitle = payload.subtitle,
                Tag = payload.tag,
                Title = payload.title
            };

            if (payload.mapItems == null)
                return map;

            for (var i = 0; i < map.Items.Length; i++)
                map.Items[i] = payload.mapItems[i].ToMapItem();

            return map;
        }
    }
}