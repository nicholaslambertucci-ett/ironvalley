using System;
using Ett.Vdt.NativeApplication.Data.Types;

namespace Ett.Vdt.NativeApplication.Data.Maps.Payloads.Extensions
{
    internal static class ExtensionsToMapItemPayload
    {
        public static MapItem ToMapItem(this MapItemPayload payload)
        {
            var item = new MapItem
            {
                ActivationId = string.IsNullOrEmpty(payload.activationId)
                    ? (int?)null
                    : int.Parse(payload.activationId),
                ActivationType = null,
                Id = payload.id,
                MediaPath = payload.mediaPath,
                Latitude = payload.latitude,
                Longitude = payload.longitude,
                PoiId = string.IsNullOrEmpty(payload.poiId)
                ? (int?)null
                : int.Parse(payload.poiId),
                X = payload.x,
                Y = payload.y
            };

            if (string.IsNullOrEmpty(payload.activationType))
                return item;

            InternalTypeOut internalType;
            item.ActivationType = Enum.TryParse(payload.activationType, true, out internalType)
                ? (TypeOut)((int)internalType)
                : TypeOut.Unknown;

            return item;
        }
    }
}