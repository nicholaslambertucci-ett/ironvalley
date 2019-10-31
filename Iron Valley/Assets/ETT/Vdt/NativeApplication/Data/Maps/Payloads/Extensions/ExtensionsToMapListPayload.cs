namespace Ett.Vdt.NativeApplication.Data.Maps.Payloads.Extensions
{
    internal static class ExtensionsToMapListPayload
    {
        public static MapList ToMapLIst(this MapListPayload payload)
        {
            var list = new MapList
            {
                Maps = new Map[payload.maps?.Length ?? 0]
            };

            if (payload.maps == null)
                return list;

            for (var i = 0; i < list.Maps.Length; i++)
                list.Maps[i] = payload.maps[i].ToMap();

            return list;
        }
    }
}