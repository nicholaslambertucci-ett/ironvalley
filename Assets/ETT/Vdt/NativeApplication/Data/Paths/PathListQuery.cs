namespace Ett.Vdt.NativeApplication.Data.Paths
{
    public struct PathListQuery
    {
        public const int All = -1;
        public const int None = -1;

        public static PathListQuery Empty => new PathListQuery(limit: All, offset: None);

        public readonly int[] CategoriesId;
        public readonly string[] CategoriesTag;
        public readonly int[] ExcludeCategoriesId;
        public readonly string[] ExcludeCategoriesTag;
        public readonly int[] Ids;
        public readonly int Limit;
        public readonly int Offset;



        public PathListQuery(int[] ids = null, int[] categoriesId = null, string[] categoriesTag = null, int[] excludeCategoriesId = null, string[] excludeCategoriesTag = null, int? limit = null, int? offset = null)
        {
            this.CategoriesId = categoriesId ?? new int[0];
            this.CategoriesTag = categoriesTag ?? new string[0];
            this.ExcludeCategoriesId = excludeCategoriesId ?? new int[0];
            this.ExcludeCategoriesTag = excludeCategoriesTag ?? new string[0];
            this.Ids = ids ?? new int[0];
            this.Limit = limit ?? All;
            this.Offset = offset ?? None;

            this.IsValid = true;
        }

        internal readonly bool IsValid;
    }
}