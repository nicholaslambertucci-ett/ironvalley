namespace Ett.Vdt.NativeApplication.Data.Maps.Payloads.Extensions
{
    internal static class ExtensionsToMap
    {
        public static MapPayload ToPayload(this Map map)
        {
            var payload = new MapPayload
            {
                description = map.Description,
                id = map.Id,
                mapItems = new MapItemPayload[map.Items?.Length ?? 0],
                mediaPath= map.MediaPath,
                subtitle =  map.Subtitle,
                tag = map.Tag,
                title = map.Title
            };

            if (map.Items == null)
                return payload;

            for (var i = 0; i < map.Items.Length; i++)
                payload.mapItems[i] = map.Items[i].ToPayload();

            return payload;
        }
    }
}