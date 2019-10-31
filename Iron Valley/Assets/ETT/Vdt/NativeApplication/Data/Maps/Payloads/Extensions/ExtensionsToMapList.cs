namespace Ett.Vdt.NativeApplication.Data.Maps.Payloads.Extensions
{
    internal static class ExtensionsToMapList
    {
        public static MapListPayload ToPayload(this MapList list)
        {
            var payload = new MapListPayload
            {
                maps = new MapPayload[list.Maps?.Length ?? 0]
            };

            if (list.Maps == null)
                return payload;

            for (var i = 0; i < list.Maps.Length; i++)
                payload.maps[i] = list.Maps[i].ToPayload();

            return payload;
        }
    }
}