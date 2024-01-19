using Ett.Vdt.NativeApplication.Data.Types;

namespace Ett.Vdt.NativeApplication.Data.Maps.Payloads.Extensions
{
    internal static class ExtensionsToMapItem
    {
        public static MapItemPayload ToPayload(this MapItem item)
            => new MapItemPayload
            {
                activationId = item.ActivationId?.ToString(),
                activationType = item.ActivationType != null
                    ? ((InternalTypeOut)((int)item.ActivationType.Value)).ToString()
                    : null,
                id = item.Id,
                mediaPath = item.MediaPath,
                latitude = item.Latitude,
                longitude = item.Longitude,
                poiId = item.PoiId?.ToString(),
                x = item.X,
                y = item.Y
            };
    }
}