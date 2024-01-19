namespace Ett.Vdt.NativeApplication.Data.Paths.Payloads.Extensions
{
    internal static class ExtensionsToPathListQuery
    {
        public static PathListQueryPayload ToPayload(this PathListQuery query)
        {
            while (true)
            {
                if (query.IsValid)
                    return new PathListQueryPayload
                    {
                        categoriesId = query.CategoriesId ?? new int[0],
                        categoriesTag = query.CategoriesTag ?? new string[0],
                        excludeCategoriesId = query.ExcludeCategoriesId ?? new int[0],
                        excludeCategoriesTag = query.ExcludeCategoriesTag ?? new string[0],
                        ids = query.Ids ?? new int[0],
                        limit = query.Limit >= PathListQuery.All
                            ? query.Limit
                            : PathListQuery.All,
                        offset = query.Offset >= PathListQuery.None
                            ? query.Offset
                            : PathListQuery.None
                    };
                query = PathListQuery.Empty;
            }
        }
    }
}