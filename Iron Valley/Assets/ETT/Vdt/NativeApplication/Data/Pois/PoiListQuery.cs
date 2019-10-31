namespace Ett.Vdt.NativeApplication.Data.Pois
{
    public struct PoiListQuery
    {
        public const int All = -1;
        public const int None = -1;

        public static PoiListQuery Default(int pathId) => new PoiListQuery(pathId, limit: All, offset: None);

        public readonly int[] CategoriesId;
        public readonly string[] CategoriesTag;
        public readonly int[] ExcludeCategoriesId;
        public readonly string[] ExcludeCategoriesTag;
        //public readonly int[] ExcludePathCategoriesId;
        //public readonly string[] ExcludePathCategoriesTag;
        //public readonly int[] ExcludePathsId;
        public readonly int[] Ids;
        public readonly int Limit;
        //public readonly int[] PathCategoriesId;
        //public readonly string[] PathCategoriesTag;
        public readonly int PathId;
        //public readonly int[] PathsId;
        public readonly int Offset;

        public PoiListQuery(int pathId, int[] ids = null,
            int[] categoriesId = null, string[] categoriesTag = null, 
            //int[] pathsId = null, int[] pathCategoriesId = null, string[] pathCategoriesTag = null,
            int[] excludeCategoriesId = null, string[] excludeCategoriesTag = null,
            //int[] excludePathsId = null, int[] excludePathCategoriesId = null, string[] excludePathCategoriesTag = null,
            int? limit = null, int? offset = null)
        {
            this.CategoriesId = categoriesId ?? new int[0];
            this.CategoriesTag = categoriesTag ?? new string[0];
            this.ExcludeCategoriesId = excludeCategoriesId ?? new int[0];
            this.ExcludeCategoriesTag = excludeCategoriesTag ?? new string[0];
            //this.ExcludePathCategoriesId = excludePathCategoriesId ?? new int[0];
            //this.ExcludePathCategoriesTag = excludePathCategoriesTag ?? new string[0];
            //this.ExcludePathsId = excludePathsId ?? new int[0];
            this.Ids = ids ?? new int[0];
            //this.PathCategoriesId = pathCategoriesId ?? new int[0];
            //this.PathCategoriesTag = pathCategoriesTag ?? new string[0];
            //this.PathsId = pathsId ?? new int[0];
            this.PathId = pathId;

            this.Limit = limit.HasValue && limit.Value >= All
                ? limit.Value
                : All;

            this.Offset = offset.HasValue && offset.Value >= None
                ? offset.Value
                : None;

            this.IsValid = true;
        }


        internal readonly bool IsValid;
    }
}