using System;
using Ett.Vdt.NativeApplication.Data.Types;

namespace Ett.Vdt.NativeApplication.Data.Maps.Payloads.Extensions
{
    internal static class ExtensionsToMapListQuery
    {
        public static MapListQueryPayload ToPayload(this MapListQuery query)
        {
            if (!query.IsValid)
                throw new ArgumentException("Invalid map query");

            return new MapListQueryPayload
            {
                activationId = query.ActivationId,
                activationType = ((InternalTypeIn)((int)query.ActivationType)).ToString(),
                excludedTags = query.ExcludedTags ?? new string[0],
                pathId = query.PathId,
                poiId = query.PoiId,
                tags = query.Tags ?? new string[0],
                limit = query.Limit,
                offset = query.Offset
            };
        }
    }
}