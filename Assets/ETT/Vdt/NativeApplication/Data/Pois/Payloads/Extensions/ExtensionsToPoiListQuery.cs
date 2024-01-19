using System;

namespace Ett.Vdt.NativeApplication.Data.Pois.Payloads.Extensions
{
    internal static class ExtensionsToPoiListQuery
    {
        public static PoiListQueryPayload ToPayload(this PoiListQuery query)
        {
            if (query.IsValid)
                return new PoiListQueryPayload
                {
                    categoriesId = query.CategoriesId ?? new int[0],
                    categoriesTag = query.CategoriesTag ?? new string[0],
                    excludeCategoriesId = query.ExcludeCategoriesId ?? new int[0],
                    excludeCategoriesTag = query.ExcludeCategoriesTag ?? new string[0],
                    //excludePathCategoriesId = query.ExcludePathCategoriesId ?? new int[0],
                    //excludePathCategoriesTag = query.ExcludePathCategoriesTag ?? new string[0],
                    //excludePathsId = query.ExcludePathsId ?? new int[0],
                    ids = query.Ids ?? new int[0],
                    //pathCategoriesId = query.PathCategoriesId ?? new int[0],
                    //pathCategoriesTag = query.PathCategoriesTag ?? new string[0],
                    pathId = query.PathId,
                    //pathsId = query.PathsId,
                    limit = query.Limit,
                    offset = query.Offset
                };
            throw new ArgumentException("Query is not valid");
        }
    }
}