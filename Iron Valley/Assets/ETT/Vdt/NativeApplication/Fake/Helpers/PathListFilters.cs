using System;
using System.Collections.Generic;
using Ett.Vdt.NativeApplication.Data.Paths.Payloads;

namespace Ett.Vdt.NativeApplication.Fake.Helpers
{
    internal static class PathListFilters
    {
        public static bool Predicate(PathPayload path, PathListQueryPayload query)
        {
            return IdsFilter(path.id, query.ids);
        }

        private static bool IdsFilter(int pathId, int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return true;

            return Array.IndexOf(ids, pathId) >= 0;
        }
    }
}