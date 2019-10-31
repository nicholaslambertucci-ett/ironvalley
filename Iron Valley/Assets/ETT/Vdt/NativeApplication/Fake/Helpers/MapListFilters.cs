using System;
using Ett.Vdt.NativeApplication.Data.Maps.Payloads;

namespace Ett.Vdt.NativeApplication.Fake.Helpers
{
    internal static class MapListFilters
    {
        public static bool Predicate(MapPayload map, MapListQueryPayload query)
        {           
            return (TagsFilter(map, query.tags)
                || IgnoreTagsFilter(map, query.excludedTags));
        }

        private static bool IgnoreTagsFilter(MapPayload map, string[] tags)
        {
            if (tags == null || tags.Length == 0)
                return true;

            if (string.IsNullOrEmpty(map.tag))
                return true;

            return Array.IndexOf(tags, map.tag) < 0;
        }

        private static bool TagsFilter(MapPayload map, string[] tags)
        {
            if (tags == null || tags.Length == 0)
                return true;

            if (string.IsNullOrEmpty(map.tag))
                return false;

            return Array.IndexOf(tags, map.tag) >= 0;
        }
    }
}